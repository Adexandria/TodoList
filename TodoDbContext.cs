using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.Data.Entity;
using Tasks.Model;

namespace Tasks.Data
{
    public class TodoDbContext :DbContext
    {
        private readonly IConfiguration config;

        public TodoDbContext(DbContextOptions<TodoDbContext>  options,IConfiguration configure) :base(options)
        {
            this.config = configure;
        }
        public DbSet<Todo> Todos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(config.GetConnectionString("Todo"));
        }
        protected override void OnModelCreating(ModelBuilder bldr)
        {
            bldr.Entity<Todo>().HasData(new 
            { 
                Id = 1,
                Event ="Clean the house",
                Notes ="Remember to clean the house beauty",
                DateTime = new DateTime(2020, 04, 22)
            }
            );
            bldr.Entity<Todo>().HasData(new
            {
                Id = 2,
                Event = "Listen to music",
                Notes = "Listen to Lana Del Rey",
                DateTime = new DateTime(2020, 04, 24)
            }
           );
        }
    }
}
