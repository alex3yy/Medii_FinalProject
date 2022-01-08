using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItemShopModel.Models;
using Microsoft.EntityFrameworkCore;

namespace ItemShopModel.Data
{
    public class ItemShopContext : DbContext
    {

        public ItemShopContext(DbContextOptions<ItemShopContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<ListedItem> ListedItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Item>().ToTable("Item");
            modelBuilder.Entity<Manufacturer>().ToTable("Manufacturer");

            modelBuilder.Entity<Seller>().ToTable("Seller");
            modelBuilder.Entity<ListedItem>().ToTable("ListedItem");
            modelBuilder.Entity<ListedItem>()
                .HasKey(c => new { c.ItemID, c.SellerID }); // configureaza cheia primara compusa
        }
    }
}
