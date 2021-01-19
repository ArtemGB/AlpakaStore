using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Model.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Core.DbControl
{
    class UserDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }

        public UserDbContext()
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
