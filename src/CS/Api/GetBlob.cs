using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Api
{
    public class GetBlob
    {
        private readonly IConfiguration _configuration;

        public GetBlob(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [FunctionName("GetBlob")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Get Blobs function processed a request.");
            int count = 0;

            try {
                BlobServiceClient blobServiceClient = new BlobServiceClient(_configuration.GetValue<string>("BLOB_CONNECTIONSTRING"));
                BlobContainerClient container = blobServiceClient.GetBlobContainerClient("output");
                await container.CreateIfNotExistsAsync();

                await foreach (BlobItem blobItem in container.GetBlobsAsync())
                {
                    count++;
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }

            return new OkObjectResult($"{count} items found.");
        }
    }
}
