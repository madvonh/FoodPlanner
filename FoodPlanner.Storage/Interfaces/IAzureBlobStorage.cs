using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FoodPlanner.Storage.Interfaces
{
    public interface IAzureBlobStorage
    {
        Task UploadAsync(string blobName, string filePath);

        Task UploadAsync(string blobName, Stream stream);

        Task<MemoryStream> DownloadAsync(string blobName);

        Task DownloadAsync(string blobName, string path);

        Task<Uri> UriFor(string id);

        Task<List<AzureBlobItem>> ListAsync();

        Task<List<string>> ListFoldersAsync();
    }
}
