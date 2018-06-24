using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeerStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace BeerStore.Controllers
{
    public class AccountController : Controller
    {

        SignInManager<BeerStoreUser> _signInManager;


        //this is a constructor
        public AccountController(SignInManager<BeerStoreUser> signInManager)
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
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                BeerStoreUser newUser = new BeerStoreUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber
                };

                IdentityResult creationResult = _signInManager.UserManager.CreateAsync(newUser).Result;

                if (creationResult.Succeeded)
                {
                    IdentityResult passwordResult = _signInManager.UserManager.AddPasswordAsync(newUser, model.Password).Result;
                    if (passwordResult.Succeeded)
                    {
                        _signInManager.SignInAsync(newUser, false).Wait();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        foreach(var error in passwordResult.Errors)
                        {
                            ModelState.AddModelError(error.Code, error.Description);
                        }
                    }
                    
                }

               
             
            }

            return View();

        }

        public IActionResult Signout()
        {
            _signInManager.SignOutAsync().Wait();
            return RedirectToAction("Index", "Home");
        }


        //get
        public IActionResult Signin()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                BeerStoreUser existingUser = _signInManager.UserManager.FindByNameAsync(model.UserName).Result;
                //if user already exists
                if(existingUser != null)
                {   
                    //check to see if their password is correct and attempt sign-in
                    Microsoft.AspNetCore.Identity.SignInResult passwordResult = _signInManager.CheckPasswordSignInAsync(existingUser, model.Password, false).Result;
                    if (passwordResult.Succeeded)
                    {
                        _signInManager.SignInAsync(existingUser, false).Wait();
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("PasswordIncorrect", "Username or password is incorrect");
                }
            }
            else
            {
                ModelState.AddModelError("UserDoesNotExist", "User or password does not exist");
            }

            return View();
        }


     



    }
}