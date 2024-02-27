using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.EFs
{
    internal class CPtsDbContextFactory : IDesignTimeDbContextFactory<CPtsDbContext>
    {
        public CPtsDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json")
                  .Build();

            var connectionString = configurationRoot.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<CPtsDbContext>();

            optionsBuilder.UseSqlServer(connectionString);
            return new CPtsDbContext(optionsBuilder.Options);
        }
    }
}