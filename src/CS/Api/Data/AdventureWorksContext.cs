using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.Data
{
    public class AdventureWorksContext : DbContext
    {
        public AdventureWorksContext(DbContextOptions<AdventureWorksContext> options)
            : base(options)
        {
        }
    }
}