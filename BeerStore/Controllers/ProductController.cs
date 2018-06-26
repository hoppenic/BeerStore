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
        private readonly BeerStoreDbContext _beerStoreDbContext;

        public ProductController(BeerStoreDbContext beerStoreDbContext)
        {
            _beerStoreDbContext = beerStoreDbContext;

        }

        public IActionResult Index()
        {
            List<Product> products = _beerStoreDbContext.Products.ToList();
            return View(products);
        }



        //public method called details
        public IActionResult Details(int? id)
        {
            if (id.HasValue)
            {

                Product p = _beerStoreDbContext.Products.Find(id.Value);
                return View(p);
            }
            return NotFound();

        }

      
    }
}