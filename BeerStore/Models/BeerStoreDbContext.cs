using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BeerStore.Models;

namespace BeerStore.Models
{
    public class BeerStoreDbContext : IdentityDbContext<BeerStoreUser>
    {
        //Constructor
        public BeerStoreDbContext() : base()
        {

        }

        public BeerStoreDbContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

    }

    public class BeerStoreUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
