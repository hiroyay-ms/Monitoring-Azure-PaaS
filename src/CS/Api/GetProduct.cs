using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Api.Data;
using Api.Models;

namespace Api
{
    public class GetProduct
    {
        private readonly AdventureWorksContext _context;

        public GetProduct(AdventureWorksContext context)
        {
            _context = context;
        }

        [FunctionName("GetProduct")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("GetProduct function processed a request.");

            if (!req.Query.ContainsKey("id"))
            {
                return new BadRequestObjectResult("Please pass a product category id on the query string.");
            }

            var query = from p in _context.Products 
                        join pc in _context.ProductCategories on p.ProductCategoryID equals pc.ProductCategoryID 
                        join pm in _context.ProductModels on p.ProductModelID equals pm.ProductModelID 
                        join pmd in _context.ProductModelProductDescriptions on pm.ProductModelID equals pmd.ProductModelID 
                        join pd in _context.ProductDescriptions on pmd.ProductDescriptionID equals pd.ProductDescriptionID 
                        where pmd.Culture == "en" && pc.ProductCategoryID == Convert.ToInt32(req.Query["id"])
                        select new 
                        {
                            p.ProductID,
                            ProductName = p.Name,
                            p.ProductNumber,
                            p.Color,
                            p.StandardCost,
                            p.ListPrice,
                            p.Size,
                            p.Weight,
                            p.ProductCategoryID,
                            CategoryName = pc.Name,
                            ModelName = pm.Name,
                            Description = pd.Description,
                            p.SellStartDate,
                            p.SellEndDate,
                            p.ThumbnailPhotoFileName
                        };
            
            var products = await query.ToListAsync();

            log.LogInformation($"{products.Count} products found.");

            string jsonStr = JsonSerializer.Serialize(products);

            return new OkObjectResult(jsonStr);
        }
    }
}
