using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data
{
    public class LoanContext : DbContext
    {
        //private readonly string conn = "Data Source=OSI-L-0399;Initial Catalog=ParallelProgramming;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        public LoanContext(DbContextOptions<LoanContext> options) : base(options)
        {
        }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<LoanMessages> Messages { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=OSI-L-0399;Initial Catalog=ParallelProgramming;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
