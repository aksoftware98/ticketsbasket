using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TicketsBasket.Infrastructure.Options;

namespace TicketsBasket.Services.Storage
{
    public interface IStorageService
    {

        // Save a blob 
        Task<string> SaveBlobAsync(string containerName, IFormFile file);

        // Delete a blob 
        Task<string> RemoveBlobAsync(string containerName, string blobName);

        // Get protected URL 
        string GetProtectedUrl(string container, string blob, DateTime expireDate); 

    }

    public class AzureBlobStorgeService : IStorageService
    {

        private readonly AzureStorageOptions _options;
        private readonly BlobServiceClient _blobServiceClient; 
        public AzureBlobStorgeService(AzureStorageOptions options)
        {
            _options = options;
            _blobServiceClient = new BlobServiceClient(options.StorageConnectionString);
        }

        public string GetProtectedUrl(string container, string blob, DateTime expireDate)
        {
            throw new NotImplementedException();
        }

        public Task<string> RemoveBlobAsync(string containerName, string blobName)
        {
            throw new NotImplementedException();
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
