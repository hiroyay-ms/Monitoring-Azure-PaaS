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
    public class GetProductCategory
    {
        private readonly AdventureWorksContext _context;

        public GetProductCategory(AdventureWorksContext context)
        {
            _context = context;
        }
                [FunctionName("GetProductCategory")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("GetProductCategory function processed a request.");

            var query = from a in _context.ProductCategories 
                        join b in _context.ProductCategories on a.ProductCategoryID equals b.ParentProductCategoryID 
                        join p in _context.Products on b.ProductCategoryID equals p.ProductCategoryID 
                        group new { a, b, p } by new { b.ProductCategoryID, Category = a.Name, SubCategory = b.Name } into g 
                        orderby g.Key.ProductCategoryID  
                        select new 
                        {
                            ProductCategoryID = g.Key.ProductCategoryID, 
                            Category = g.Key.Category,
                            SubCategory = g.Key.SubCategory,
                            ProductCount = g.Count()
                        };
            if (req.Query.ContainsKey("id"))
            {
                switch(Convert.ToInt32(req.Query["id"]))
                {
                    case 1:
                        query = query.Where(x => x.Category == "Bikes");
                        break;
                    case 2:
                        query = query.Where(x => x.Category == "Components");
                        break;
                    case 3:
                        query = query.Where(x => x.Category == "Clothing");
                        break;
                    case 4:
                        query = query.Where(x => x.Category == "Accessories");
                        break;
                }
            }

            var productCategories = await query.ToListAsync();

            log.LogInformation($"{productCategories.Count} product categories found.");

            var jsonString = JsonSerializer.Serialize(productCategories);

            return new OkObjectResult(jsonString);
        }
    }
}
