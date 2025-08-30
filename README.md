📦 ImgCompressor.Function
ImgCompressor.Function is a blob-triggered Azure Function designed to automatically compress image files uploaded to Azure Blob Storage. It supports both local development using Azurite and production deployment to Azure Blob Storage, making it ideal for scalable, event-driven media workflows.

🚀 Features
- 🔁 Blob Triggered: Automatically fires when a new image is uploaded to the input container
- 🖼️ Image Compression: Reduces file size using configurable compression logic
- 🧪 Local Development Support: Fully testable with Azurite and Azure Storage Explorer
- 🔐 Secure Configuration: Uses environment variables or Azure Key Vault for secrets
- 📂 Output Routing: Saves compressed images to a separate container with the same blob name

🧰 Technologies Used
- .NET 8
- Azure Functions
- Azure.Storage.Blobs SDK
- Azurite (local emulator)
- Azure Storage Explorer
- Optional: ImageSharp or System.Drawing for compression

🧪 Local Development Setup
- Install Azurite via VS Code extension or npm
- Start Azurite and create input/output containers
- Set AzureWebJobsStorage to "UseDevelopmentStorage=true"
- Upload test images using Azure Storage Explorer
- Run the function locally and verify compressed output

🌐 Production Deployment
- Update AzureWebJobsStorage with your Azure Blob Storage connection string
- Deploy the function to Azure
- Monitor logs and blob activity via Application Insights or Azure Portal
