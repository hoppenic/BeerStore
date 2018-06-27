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

        private readonly BeerStoreDbContext _beerStoreDbContext;

        //constructor
        public CartController(BeerStoreDbContext BeerStoreDbContext)
        {
            _beerStoreDbContext = BeerStoreDbContext;


        }

        public IActionResult Index()
        {
            Guid cartId;
            Cart cart = null;
            if (Request.Cookies.ContainsKey("cartId"))
            {
                if(Guid.TryParse(Request.Cookies["cartId"], out cartId))
                {
                    cart = _beerStoreDbContext.Carts
                    .Include(Carts => Carts.CartItems)
                    .ThenInclude(CartItems => CartItems.Product)
                    .FirstOrDefault(x => x.CookieIdentifier == cartId);
                }
            }

            if (cart == null)
            {
                cart = new Cart();
            }
            return View(cart);
        }

        public IActionResult Remove(int id)
        {
            Guid cartId;
            Cart cart = null;
            if (Request.Cookies.ContainsKey("cartId"))
            {
                if(Guid.TryParse(Request.Cookies["cartId"], out cartId))
                {
                    cart = _beerStoreDbContext.Carts
                        .Include(Carts => Carts.CartItems)
                        .ThenInclude(CartItems => CartItems.Product)
                        .FirstOrDefault(x => x.CookieIdentifier == cartId);
                }
            }

            CartItem item = cart.CartItems.FirstOrDefault(x => x.ID == id);

            cart.LastModified = DateTime.Now;

            _beerStoreDbContext.CartItems.Remove(item);

            _beerStoreDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
       

    }
}