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
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }


    }

    public class BeerStoreUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }

    public class Cart
    {

        //Constructor
        public Cart()
        {
            CartItems = new HashSet<CartItem>();
        }

        public int ID { get; set; }
        public Guid CookieIdentifier { get; set; }
        public DateTime LastModified { get; set; }
        public ICollection<CartItem> CartItems { get; set; }

    }



    public class CartItem
    {
        public int ID { get; set; }
        public Cart Cart { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }


    }

}
