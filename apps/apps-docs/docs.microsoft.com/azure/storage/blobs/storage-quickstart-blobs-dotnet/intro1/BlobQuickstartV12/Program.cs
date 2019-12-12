using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BlobQuickstartV12
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-dotnet
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = File.ReadAllText("D:\\apikeys\\readendless\\azure-blob-connection-string.txt");
            string accountName = File.ReadAllText("D:\\apikeys\\readendless\\azure-blob-accountName.txt");
            string accountKey = File.ReadAllText("D:\\apikeys\\readendless\\azure-blob-accountKey.txt");
            // Create a BlobServiceClient object which will be used to create a container client
            string containerName = "readendless-documents";
            // await CreateContainer(connectionString, containerName);
            // await UploadFile(connectionString, containerName);
            // await ListFiles(connectionString, containerName);
            // await DownloadFile(connectionString, containerName);
            // await DeleteContainer(connectionString, containerName);
            await GetSharedAccessSignature(accountName, accountKey, containerName);
        }

        private static async Task GetSharedAccessSignature(string accountName, string accountKey, string containerName)
        {
            StorageCredentials storageCredentials = new StorageCredentials(accountName, accountKey);
            CloudStorageAccount storageAccount = new CloudStorageAccount(storageCredentials, true);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            BlobContinuationToken continuationToken = null;
            List<string> thumbnailUrls = new List<string>();
            do
            {
                BlobResultSegment resultSegment = await container.ListBlobsSegmentedAsync("", true, BlobListingDetails.All, 10, continuationToken, null, null);
                foreach (IListBlobItem blobItem in resultSegment.Results)
                {
                    CloudBlockBlob blob = blobItem as CloudBlockBlob;
                    SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();
                    sasConstraints.SharedAccessStartTime = DateTimeOffset.UtcNow.AddMinutes(-5);
                    sasConstraints.SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddHours(24);
                    sasConstraints.Permissions = SharedAccessBlobPermissions.Read;
                    string sasBlobToken = blob.GetSharedAccessSignature(sasConstraints);
                    thumbnailUrls.Add(blob.Uri + sasBlobToken);
                }
                continuationToken = resultSegment.ContinuationToken;

            } while (continuationToken != null);
            Console.WriteLine(JsonConvert.SerializeObject(thumbnailUrls, Formatting.Indented));

        }

        private static async Task DeleteContainer(string connectionString, string containerName)
        {
            // Clean up
            Console.Write("Press any key to begin clean up");
            Console.ReadLine();

            Console.WriteLine("Deleting blob container...");
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.DeleteAsync();

            Console.WriteLine("Deleting the local source and downloaded files...");


            Console.WriteLine("Done");
        }

        private static async Task DownloadFile(string connectionString, string containerName)
        {
            // Download the blob to a local file
            // Append the string "DOWNLOAD" before the .txt extension so you can see both files in MyDocuments
            string downloadFilePath = "D:\\temp\\downloads\\intro.pdf";

            Console.WriteLine("\nDownloading blob to\n\t{0}\n", downloadFilePath);
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            BlobItem blobItem2 = null;
            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                blobItem2 = blobItem;
                break;
            }

            if (blobItem2 != null)
            {
                BlobClient blobClient = containerClient.GetBlobClient(blobItem2.Name);
                // Download the blob's contents and save it to a file
                BlobDownloadInfo download = await blobClient.DownloadAsync();
                await using FileStream downloadFileStream = File.OpenWrite(downloadFilePath);
                await download.Content.CopyToAsync(downloadFileStream);
                downloadFileStream.Close();
            }
        }

        private static async Task ListFiles(string connectionString, string containerName)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                Console.WriteLine(blobItem.Name);
            }
        }

        private static async Task UploadFile(string connectionString, string containerName)
        {
            // Create a local file in the ./data/ directory for uploading and downloading
            FileInfo fileInfo = new FileInfo("C:\\Users\\dj_re\\Downloads\\pdf-samples\\NET DevOps for Azure.pdf");
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            // Get a reference to a blob
            BlobClient blobClient = containerClient.GetBlobClient(fileInfo.Name);

            Console.WriteLine("Uploading to Blob storage as blob:\n{0}\n", blobClient.Uri);

            // Open the file and upload its data
            using FileStream uploadFileStream = File.OpenRead(fileInfo.FullName);
            Response<BlobContentInfo> uploadAsync = await blobClient.UploadAsync(uploadFileStream, true);
            BlobContentInfo uploadAsyncValue = uploadAsync.Value;
            Console.Write(JsonConvert.SerializeObject(new
            {
                uploadAsyncValue.BlobSequenceNumber,
                uploadAsyncValue.ContentHash,
                uploadAsyncValue.ETag,
                uploadAsyncValue.EncryptionKeySha256,
                uploadAsyncValue.LastModified,
            }, Formatting.Indented));
            uploadFileStream.Close();

        }

        private static async Task<BlobContainerClient> CreateContainer(string connectionString, string containerName)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            // Create a unique name for the container


            // Create the container and return a container client object
            BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
            return containerClient;
        }
    }
}
