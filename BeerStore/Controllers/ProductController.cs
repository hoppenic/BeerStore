using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeerStore.Models;

namespace BeerStore.Controllers
{
    public class ProductController : Controller
    {

        //this is my list called _products, using the Product Class
        private List<Product> _products;

        public ProductController()
        {
            //for now use list to mock up data
            _products = new List<Product>();
            _products.Add(new Product
            {
                ID = 1,
                Name = "California IPA",
                Description = "IPA with a southern California soul",
                Image="/images/beer4.jpg",
                Price = 5m

            });

            _products.Add(new Product
            {
                ID = 2,
                Name = "Session IPA",
                Description = "A low-ABV IPA for daytime sipping",
                Image = "/images/beer4.jpg",
                Price = 4m
            });

        }


        //public method called details
        public IActionResult Details(int? id)
        {
            if (id.HasValue)
            {   

                Product p = _products.Single(x => x.ID == id.Value);
                return View(p);
            }
            return NotFound();

        }

        public IActionResult Index()
        {
            return View(_products);
        }
    }
}