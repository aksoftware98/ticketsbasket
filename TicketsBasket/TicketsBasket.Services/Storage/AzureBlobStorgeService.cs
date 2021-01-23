using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
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

        public async Task<string> SaveBlobAsync(string containerName, IFormFile file)
        {
            string fileName = file.FileName;
            string extension = Path.GetExtension(fileName);
            // Validate the extension 
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
    }
}
