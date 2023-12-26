using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Data;

namespace Repository
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<LoanContext>
    {
        public LoanContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<LoanContext>();
            var connectionString = configuration.GetConnectionString("conn");

            builder.UseSqlServer(connectionString);

            return new LoanContext(builder.Options);
        }
    }
}
