using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
   public class LinksDb:DbContext 
    {
        public LinksDb(DbContextOptions<LinksDb> options):base(options)
        {

        }
        public DbSet<LinkCheckResult> Links { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var conneciton = @"Server=localhost;Database=Links;User id=sa;Password=Siya_2010";
        //    optionsBuilder.UseSqlServer(conneciton);
        //}

    }
}
