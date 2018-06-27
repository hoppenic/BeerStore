using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeerStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BeerStore
{
    internal static class DbInitializer
    {


        //method
       internal static void Initialize(BeerStoreDbContext db)
        {

            db.Database.Migrate();

            if (db.Products.Count() == 0)
            {
                db.Products.Add(new Product
                {
                    Name = "IPA",
                    Image = "/Images/beer1.jpg",
                    Description = "California IPA",
                    Price = 3.99m


                });

                db.Products.Add(new Product
                {
                    Name = "IPA",
                    Image = "/Images/beer2.jpg",
                    Description = "Low Alcohol Session IPA",
                    Price = 4.99m

                });

                db.SaveChanges();
            }
           



        }









    }



}
