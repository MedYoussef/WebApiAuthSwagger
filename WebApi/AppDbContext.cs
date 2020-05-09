using System;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }
    }
}
