using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.Data
{
    public class AdventureWorksContext : DbContext
    {
        public AdventureWorksContext(DbContextOptions<AdventureWorksContext> options) : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
        public DbSet<ProductModel> ProductModels => Set<ProductModel>();
        public DbSet<ProductDescription> ProductDescriptions => Set<ProductDescription>();
        public DbSet<ProductModelProductDescription> ProductModelProductDescriptions => Set<ProductModelProductDescription>();
        public DbSet<SalesOrderHeader> SalesOrderHeaders => Set<SalesOrderHeader>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Address> Addresses => Set<Address>();
        public DbSet<SalesOrderDetail> SalesOrderDetails => Set<SalesOrderDetail>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product", schema: "SalesLT");
            modelBuilder.Entity<ProductCategory>().ToTable("ProductCategory", schema: "SalesLT");
            modelBuilder.Entity<ProductModel>().ToTable("ProductModel", schema: "SalesLT");
            modelBuilder.Entity<ProductDescription>().ToTable("ProductDescription", schema: "SalesLT");
            modelBuilder.Entity<ProductModelProductDescription>().ToTable("ProductModelProductDescription", schema: "SalesLT").HasNoKey();
            modelBuilder.Entity<SalesOrderHeader>().ToTable("SalesOrderHeader", schema: "SalesLT").HasNoKey();
            modelBuilder.Entity<Customer>().ToTable("Customer", schema: "SalesLT");
            modelBuilder.Entity<Address>().ToTable("Address", schema: "SalesLT");
            modelBuilder.Entity<SalesOrderDetail>().ToTable("SalesOrderDetail", schema: "SalesLT").HasNoKey();
        }
    }
}