using System;
using System.IO;
using Azure.Storage.Blobs;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Threading.Tasks;

namespace trainInQueue
{
    public class trainInQueue
    {
        [FunctionName("trainInQueue")]
        public static async Task<IActionResult> Run(string myQueueItem, ILogger log)
        {
            //Save base64 Picture in Blob
            string Connection = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            string containerName = Environment.GetEnvironmentVariable("ContainerName");
            byte[] bytes = Convert.FromBase64String(myQueueItem);
            Stream myBlob = new MemoryStream(bytes);
            var blobClient = new BlobContainerClient(Connection, containerName);
            var myUniqueFileName = string.Format(@"{0}.txt", Guid.NewGuid());
            var blob = blobClient.GetBlobClient(myUniqueFileName);
            await blob.UploadAsync(myBlob);
            log.LogDebug("file uploaded successfylly");
            return new OkObjectResult("file uploaded successfylly");
        }
    }
}

