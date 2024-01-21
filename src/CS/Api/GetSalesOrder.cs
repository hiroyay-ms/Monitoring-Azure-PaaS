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
    public class GetSalesOrder
    {
        private readonly AdventureWorksContext _context;

        public GetSalesOrder(AdventureWorksContext context)
        {
            _context = context;
        }

        [FunctionName("GetSalesOrder")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("GetSalesOrder function processed a request.");

            var query = from soh in _context.SalesOrderHeaders 
                        join c in _context.Customers on soh.CustomerID equals c.CustomerID 
                        join a in _context.Addresses on soh.ShipToAddressID equals a.AddressID 
                        select new 
                        {
                            soh.SalesOrderID,
                            soh.ShipDate,
                            soh.SalesOrderNumber,
                            soh.AccountNumber,
                            soh.CustomerID,
                            CustomerName = c.FirstName + " " + c.LastName,
                            c.CompanyName, 
                            soh.ShipToAddressID,
                            soh.ShipMethod,
                            a.AddressLine1,
                            a.City,
                            a.StateProvince,
                            a.PostalCode,
                            soh.SubTotal,
                            soh.TaxAmt,
                            soh.Freight,
                            soh.TotalDue
                        };
            
            var orders = await query.ToListAsync();

            log.LogInformation($"{orders.Count} orders found.");

            string jsonString = JsonSerializer.Serialize(orders);

            return new OkObjectResult(jsonString);
        }
    }
}
