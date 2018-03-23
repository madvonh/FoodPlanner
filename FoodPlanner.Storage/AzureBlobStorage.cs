using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FoodPlanner.Storage.Interfaces;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace FoodPlanner.Storage
{
    public class AzureBlobStorage: IAzureBlobStorage
    {
        private readonly AzureBlobSettings _settings;
        public AzureBlobStorage(AzureBlobSettings azureBlobSettings)
        {
            _settings = azureBlobSettings;
        }

        public async Task UploadAsync(string blobName, string filePath)
        {
            //Blob
            CloudBlockBlob blockBlob = await GetBlockBlobAsync(blobName);

            //Upload
            using (var fileStream = System.IO.File.Open(filePath, FileMode.Open))
            {
                fileStream.Position = 0;
                await blockBlob.UploadFromStreamAsync(fileStream);
            }
        }

        public async Task UploadAsync(string blobName, Stream stream)
        {
            //Blob
            CloudBlockBlob blockBlob = await GetBlockBlobAsync(blobName);

            //Upload
            stream.Position = 0;
            await blockBlob.UploadFromStreamAsync(stream);
        }

        public async Task<Uri> UriFor(string imageId)
        {
            //to get an image with a sas shared access signature permission:
            //
            var sasPolicy = new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessStartTime = DateTime.Now.AddMinutes(-15),
                SharedAccessExpiryTime = DateTime.Now.AddMinutes(30)
            };

            //Container
            CloudBlobContainer blobContainer = await GetContainerAsync();

            var blob = blobContainer.GetBlockBlobReference(imageId);
            var sasToken = blob.GetSharedAccessSignature(sasPolicy);

            return new Uri(_settings.BlobServiceEndpoint + $"/{_settings.ContainerName}/{imageId}{sasToken}");
        }

        public async Task<MemoryStream> DownloadAsync(string blobName)
        {
            //Blob
            CloudBlockBlob blockBlob = await GetBlockBlobAsync(blobName);

            //Download
            using (var stream = new MemoryStream())
            {
                await blockBlob.DownloadToStreamAsync(stream);
                return stream;
            }
        }

        public async Task DownloadAsync(string blobName, string path)
        {
            //Blob
            CloudBlockBlob blockBlob = await GetBlockBlobAsync(blobName);

            //Download
            await blockBlob.DownloadToFileAsync(path, FileMode.Create);
        }

        public async Task<List<AzureBlobItem>> ListAsync()
        {
            return await GetBlobListAsync();
        }
         
        public async Task<List<string>> ListFoldersAsync()
        {
            var list = await GetBlobListAsync();
            return list.Where(i => !string.IsNullOrEmpty(i.Folder))
                .Select(i => i.Folder)
                .Distinct()
                .OrderBy(i => i)
                .ToList();
        }

        private async Task<CloudBlobContainer> GetContainerAsync()
        {
            //Account
            CloudStorageAccount storageAccount = new CloudStorageAccount(
                new StorageCredentials(_settings.StorageAccount, _settings.StorageKey), false);

            //Client
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            //Container
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(_settings.ContainerName);
            bool exits = await blobContainer.CreateIfNotExistsAsync();

            //// Set the permissions so the blobs are public. 
            if (exits) return blobContainer;

            var permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };
            await blobContainer.SetPermissionsAsync(permissions);

            return blobContainer;
        }

        //var id = Guid.NewGuid().ToString();
        //var blob = container.GetBlockBlobReference(id);
        //await blob.UploadFromStreamAsync(stream);

        //to get an image with a sas shared access signature permission:
        //
        //var sasPolicy = new SharedAcessBlobPolicy
        //{
        //    Permissions = SharedAccessBlobPermissions.Read,
        //    SharedAccessStartTime = DateTime.Now.AddMinutes(-15),
        //    SharedAccessExpiryTime = DateTime.Now.AddMinutes(30)
        //}       
        //var container = _client.GetContainerReference("images");
        //var blob = container.GetBlockBlobReference(imageId);
        //var sasToken = blob.GetSharedAccessSignature(sasPolicy);

        // new Uri(_baseUri, $"/images/{imageId}{sasToken}");


        private async Task<CloudBlockBlob> GetBlockBlobAsync(string blobName)
        {
            //Container
            CloudBlobContainer blobContainer = await GetContainerAsync();

            //Blob
            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(blobName);

            return blockBlob;
        }

        private async Task<List<AzureBlobItem>> GetBlobListAsync(bool useFlatListing = true)
        {
            //Container
            CloudBlobContainer blobContainer = await GetContainerAsync();

            //List
            var list = new List<AzureBlobItem>();
            BlobContinuationToken token = null;
            do
            {
                BlobResultSegment resultSegment =
                    await blobContainer.ListBlobsSegmentedAsync("", useFlatListing,
                        new BlobListingDetails(), null, token, null, null);
                token = resultSegment.ContinuationToken;

                foreach (IListBlobItem item in resultSegment.Results)
                {
                    list.Add(new AzureBlobItem(item));
                }
            } while (token != null);

            return list.OrderBy(i => i.Folder).ThenBy(i => i.Name).ToList();
        }
    }
}
