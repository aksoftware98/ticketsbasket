using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TicketsBasket.Infrastructure.Options;

namespace TicketsBasket.Services.Storage
{
    public class AzureBlobStorgeService : IStorageService
    {

        private readonly AzureStorageOptions _options;
        private readonly BlobServiceClient _blobServiceClient;
        public AzureBlobStorgeService(AzureStorageOptions options)
        {
            _options = options;
            _blobServiceClient = new BlobServiceClient(options.StorageConnectionString);
        }

        public string GetProtectedUrl(string containerName, string blob, DateTimeOffset expireDate)
        {
            var container = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = container.GetBlobClient(Path.GetFileName(blob));
            var token = blobClient.GenerateSasUri(Azure.Storage.Sas.BlobSasPermissions.Read, expireDate);

            return token.AbsoluteUri;
        }

        public async Task RemoveBlobAsync(string containerName, string blobName)
        {
            var container = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = container.GetBlobClient(Path.GetFileName(blobName));
            await blobClient.DeleteIfExistsAsync();
        }

        public async Task<string> SaveBlobAsync(string containerName, IFormFile file, BlobType blobType)
        {
            if (file == null)
                return null;

            string fileName = file.FileName;
            string extension = Path.GetExtension(fileName);
            ValidateExtension(extension, blobType);
            string newFileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}-{Guid.NewGuid()}{extension}";

            using (var stream = file.OpenReadStream())
            {
                var container = _blobServiceClient.GetBlobContainerClient(containerName);
                await container.CreateIfNotExistsAsync();
                var blob = container.GetBlobClient(newFileName);

                await blob.UploadAsync(stream);

                return $"{_options.AccountUrl}/{containerName}/{newFileName}";
            }
        }

        private void ValidateExtension(string extension, BlobType blobType)
        {
            var allowedImageExtensions = new[] { ".jpg", ".png", ".bmp", ".svg" };
            var allowedDocumentsExtensions = new[] { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".txt" };

            switch (blobType)
            {
                case BlobType.Image:
                    if (!allowedImageExtensions.Contains(extension))
                        throw new NotSupportedException($"Image file is not supported for the extesion {extension}");
                    break;
                case BlobType.Document:
                    if (!allowedDocumentsExtensions.Contains(extension))
                        throw new NotSupportedException($"Document file is not supported for the extension {extension}");
                    break;
                default:
                    break;
            }
        }
    }
}
