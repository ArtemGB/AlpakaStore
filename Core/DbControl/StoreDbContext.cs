using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Core.DbControl
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {

        }

        private string GetConnectionStringFromJSON()
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
