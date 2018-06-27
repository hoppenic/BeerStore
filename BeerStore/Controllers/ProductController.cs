using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeerStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

        [HttpPost]
        public IActionResult Details(int id, int quantity = 1)
        {
            Guid cartId;
            Cart cart = null;
            if (Request.Cookies.ContainsKey("cartId"))
            {
                if(Guid.TryParse(Request.Cookies["cartId"],out cartId))
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
                cartId = Guid.NewGuid();
                cart.CookieIdentifier = cartId;

                _beerStoreDbContext.Carts.Add(cart);
                Response.Cookies.Append("cartId", cartId.ToString(), new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.UtcNow.AddYears(100) });

            }
            CartItem item = cart.CartItems.FirstOrDefault(x => x.Product.ID == id);
            if (item == null)
            {
                item = new CartItem();
                item.Product = _beerStoreDbContext.Products.Find(id);
                cart.CartItems.Add(item);
            }
            item.Quantity += quantity;
            cart.LastModified = DateTime.Now;

            _beerStoreDbContext.SaveChanges();
            return RedirectToAction("Index", "Cart");
        }

      
    }
}