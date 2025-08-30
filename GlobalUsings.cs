// ✅ Logging & Configuration
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;

// ✅ Azure SDKs
global using Azure.Identity;
global using Azure.Storage.Blobs;
global using Azure.Messaging.ServiceBus;

// ✅ Azure Functions
global using Microsoft.Azure.Functions.Worker;

// ✅ Entity Framework Core (if needed in Functions)

// ✅ ImageSharp (image compression / processing)
global using SixLabors.ImageSharp;
global using SixLabors.ImageSharp.Formats;
global using SixLabors.ImageSharp.Formats.Jpeg;
global using SixLabors.ImageSharp.Formats.Png;
global using SixLabors.ImageSharp.PixelFormats;
global using SixLabors.ImageSharp.Processing;

// ✅ Local project
global using ImgCompressor.Function.Helper;
