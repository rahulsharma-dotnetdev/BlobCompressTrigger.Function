namespace ImgCompressor.Function.Function;

public class ImageCompressorFunction(ILogger<ImageCompressorFunction> logger, BlobServiceClient blobServiceClient)
{
    [Function("ImageCompressorFunction")]
    public async Task Run([BlobTrigger("Images/Original_Images/{name}", Connection = "AzureWebJobsStorage")] Stream inputBlob, string name, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Triggered compression for image: {name}", name);

        var container = blobServiceClient.GetBlobContainerClient("linkedinpostmate");
        await container.CreateIfNotExistsAsync(cancellationToken: cancellationToken);

        var compressedBlobName = $"Images/Compressed_Images/{name}";
        var blobClient = container.GetBlobClient(compressedBlobName);

        if (await blobClient.ExistsAsync(cancellationToken))
        {
            logger.LogInformation("Compressed image already exists: {BlobName}", compressedBlobName);
            return;
        }

        try
        {
            await ImageHelper.CompressAsync(inputBlob, blobClient, logger, cancellationToken);
            logger.LogInformation("Successfully compressed and saved image: {BlobName} and URI is {URI}", name, blobClient.Uri);

            var originalBlobClient = container.GetBlobClient($"Images/Original_Images/{name}");
            var properties = await originalBlobClient.GetPropertiesAsync(cancellationToken: cancellationToken);
            var metadata = properties.Value.Metadata;


            var userId = metadata.TryGetValue("uploadedBy", out var u) ? u : throw new InvalidOperationException("UserId metadata missing.");
            var mediaUrnId = metadata.TryGetValue("mediaurn", out var m) ? m : throw new InvalidOperationException("MediaUrn metadata missing.");


            logger.LogInformation("UserPostMediaCompressedEvent added to Outbox for processing.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Compression failed for image: {ImageName}", name);
            throw;
        }
    }
    private static string ExtractUrnFromName(string name) => $"urn:media:{Path.GetFileNameWithoutExtension(name)}";
}
