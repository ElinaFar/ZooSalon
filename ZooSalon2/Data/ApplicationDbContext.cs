using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ZooSalon2.Models;

namespace ZooSalon2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<ServiceType> ServiceTypes { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceDetails> ServiceDetails { get; set; }
        public DbSet<ServiceShoppingCart> ServiceShoppingCarts { get; set; }
    }
}
