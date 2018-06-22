using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeerStore.Models;
using Microsoft.AspNetCore.Identity;

namespace BeerStore.Controllers
{
    public class AccountController : Controller
    {

        SignInManager<IdentityUser> _signInManager;


        //this is a constructor
        public AccountController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        //responds on GET /Account/Register
        public IActionResult Register()
        {
            return View();
        }
        
        //responds on POST /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel myModel)
        {
            if (ModelState.IsValid)
            {
                //creating a new user in the identity framework
                IdentityUser newUser = new IdentityUser(myModel.UserName);


                IdentityResult creationResult = _signInManager.UserManager.CreateAsync(newUser).Result;


            }

            return View();
        }



    }
}