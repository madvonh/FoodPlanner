using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace FoodPlanner.Storage
{
    public class AzureBlobSettings
    {
        public string StorageAccount { get; }
        public string StorageKey { get;}
        public string ContainerName { get;}

        public AzureBlobSettings(string storageAccount, string storageKey, string containerName)
        {
            if (string.IsNullOrEmpty(storageAccount)) throw new ArgumentNullException("StorageAccount");

            if (string.IsNullOrEmpty(storageKey)) throw new ArgumentNullException("StorageKey");

            if (string.IsNullOrEmpty(storageAccount)) throw new ArgumentNullException("ContainerName");

            this.StorageAccount = storageAccount;
            this.StorageKey = storageKey;
            this.ContainerName = containerName;
        }
    }
}
