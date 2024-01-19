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
using Azure.Storage.Blobs.Specialized;

using Api.Data;


namespace Api
{
    public class DataArchiving
    {
        private readonly IConfiguration _configuration;
        private readonly AdventureWorksContext _context;

        public DataArchiving(IConfiguration configuration, AdventureWorksContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [FunctionName("DataArchiving")]
        public async Task Run([TimerTrigger("0 0 1 * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"Data Archiving function executed at: {DateTime.Now}");
            DateTime dt = DateTime.Now.AddYears(-1);

            var query = from soh in _context.SalesOrderHeaders 
                        where soh.ModifiedDate < dt 
                        select soh;

            var orders = await query.ToListAsync();

            log.LogInformation($"{orders.Count} data before {dt} was found.");

            string jsonString = JsonSerializer.Serialize(orders);

            BlobServiceClient blobServiceClient = new BlobServiceClient(_configuration.GetValue<string>("BLOB_CONNECTIONSTRING"));
            BlobContainerClient container = blobServiceClient.GetBlobContainerClient("output");
            await container.CreateIfNotExistsAsync();

            BlobClient blobClient = container.GetBlobClient($"{DateTime.Now.ToString("yyyyMMdd")}.json");

            Encoding encoding = Encoding.UTF8;
            byte[] bytes = encoding.GetBytes(jsonString);

            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;

                await blobClient.UploadAsync(ms, true);
            }

            log.LogInformation($"Output {DateTime.Now.ToString("yyyyMMdd")}.json to container.");
        }
    }
}
