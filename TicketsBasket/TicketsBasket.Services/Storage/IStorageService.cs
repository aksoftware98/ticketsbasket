using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TicketsBasket.Services.Storage
{
    public interface IStorageService
    {

        // Save a blob 
        Task<string> SaveBlobAsync(string containerName, IFormFile file);

        // Delete a blob 
        Task RemoveBlobAsync(string containerName, string blobName);

        // Get protected URL 
        string GetProtectedUrl(string container, string blob, DateTimeOffset expireDate); 

    }
}
