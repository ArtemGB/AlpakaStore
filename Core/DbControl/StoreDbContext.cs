using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.Extensions.Configuration;
using Core.Model.Ordering;
using Core.Model.PriceCalculation;
using Core.Model.Users;

namespace Core.DbControl
{
    /// <summary>
    /// Предоставляет доступ к базе данных.
    /// </summary>
    public class StoreDbContext : DbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Filter> Filters { get; set; }
        public DbSet<Client> Clients { get; set; }

        public StoreDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer(GetConnectionStringFromJson());
        }

        private string GetConnectionStringFromJson()
        {
            var builder = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            builder.SetBasePath($"../{Directory.GetCurrentDirectory()}");
            // получаем конфигурацию из файла appsettings.json
            builder.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = builder.Build();
            // получаем строку подключения
            return config.GetConnectionString("DefaultConnection");
        }
    }
}
