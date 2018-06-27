using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeerStore.Models;

namespace BeerStore.Controllers
{
    public class CartController : Controller
    {

        private readonly BeerStoreDbContext _BeerStoreDbContext;

        //constructor
        public CartController(BeerStoreDbContext BeerStoreDbContext)
        {
            _BeerStoreDbContext = BeerStoreDbContext;


        }

        public IActionResult Index()
        {
            Guid cartId;
            Cart cart = null;



            return View();
        }
    }
}