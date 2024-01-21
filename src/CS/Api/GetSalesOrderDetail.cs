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

namespace Api
{
    public class GetSalesOrderDetail
    {
        private readonly AdventureWorksContext _context;

        public GetSalesOrderDetail(AdventureWorksContext context)
        {
            _context = context;
        }

        [FunctionName("GetSalesOrderDetail")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("GetSalesOrderDetail function processed a request.");

            if (!req.Query.ContainsKey("id"))
            {
                return new BadRequestObjectResult("Please pass a product category id on the query string.");
            }

            var query = from sod in _context.SalesOrderDetails 
                        join p in _context.Products on sod.ProductID equals p.ProductID 
                        join pc in _context.ProductCategories on p.ProductCategoryID equals pc.ProductCategoryID 
                        where sod.SalesOrderID == Convert.ToInt32(req.Query["id"]) 
                        select new 
                        {
                            sod.SalesOrderID,
                            sod.SalesOrderDetailID,
                            sod.ProductID,
                            ProductName = p.Name,
                            CategoryName = pc.Name,
                            sod.OrderQty,
                            sod.UnitPrice,
                            sod.UnitPriceDiscount,
                            sod.LineTotal
                        };
            
            var details = await query.ToListAsync();

            log.LogInformation($"{details.Count} sales order details found.");

            string jsonString = JsonSerializer.Serialize(details);

            return new OkObjectResult(jsonString);
        }
    }
}
