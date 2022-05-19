using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace trainInBlob
{
    public class trainInBlob
    {
        [FunctionName("trainInBlob")]
        public void Run([BlobTrigger("mytrains/{name}", Connection = "http://127.0.0.1:10000/devstoreaccount1/mytrains")]Stream myBlob, string name, ILogger log)
        {
            //Notify if New Train added!
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        }
    }
}

