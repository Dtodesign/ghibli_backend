using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GhibliWebAPI.Models;


namespace GhibliWebAPI.Context
{
    public class ghDbContext : DbContext
    {
        
        public DbSet<GhibliWebAPI.Models.Film> Films { get; set; }

        public ghDbContext(DbContextOptions<ghDbContext> options) : base(options)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

        }

       
    }
}
