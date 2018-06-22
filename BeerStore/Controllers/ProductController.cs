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


        }



        public IActionResult Index()
        {
            return View();
        }
    }
}