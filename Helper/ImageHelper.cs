namespace ImgCompressor.Function.Helper;

public class ImageHelper
{
    public static async Task CompressAsync(Stream inputStream, BlobClient blobClient, ILogger log, CancellationToken cancellationToken)
    {
        inputStream.Position = 0;

        try
        {
            using var image = await Image.LoadAsync<Rgba32>(inputStream, cancellationToken);
            log.LogInformation("Original image size: {Width}x{Height}", image.Width, image.Height);

            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Mode = ResizeMode.Max,
                Size = new Size(300,400)
            }));

            var outputStream = new MemoryStream();

            var extension = Path.GetExtension(blobClient.Name).ToLowerInvariant();

            IImageEncoder encoder = extension switch
            {
                ".jpg" or ".jpeg" => new JpegEncoder { Quality = 50 },
                ".png" => new PngEncoder
                {
                    CompressionLevel = PngCompressionLevel.DefaultCompression,
                    TransparentColorMode = PngTransparentColorMode.Preserve
                },
                _ => new JpegEncoder { Quality = 50 }
            };

            await image.SaveAsync(outputStream, encoder, cancellationToken);
            outputStream.Position = 0;

            log.LogInformation("Compressed image stream length: {outputStream.Length}", outputStream.Length);

            await blobClient.UploadAsync(outputStream, overwrite: true, cancellationToken);
            log.LogInformation("Image uploaded to blob: {blobClient.Name} ({extension})", blobClient.Name, extension);
        }
        catch (Exception ex)
        {
            log.LogError(ex, "Image compression or upload failed.");
            throw;
        }
    }
}
