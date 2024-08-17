using MassAssignmentAttacks.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MassAssignmentAttacks.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ILogger<RegisterController> _logger;

        public RegisterController(ILogger<RegisterController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VulnerableRegister()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterSafe1()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterSafe2()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult RegisterSafe3()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult RegisterSafe4()
        {
            return View();
        }


        // Vulnerable method
        // bind all the properties even if they are not needed/input by the user

        // note that IsAdmin can be set to true by the user even if it is not an admin
        [HttpPost]
        public IActionResult VulnerableRegister(User user)
        {
            if (ModelState.IsValid)
            {
                // Save user to database
                var newUser = new UserCRUD();
                newUser.Add(user);
                TempData["Message"] = $"{user.Email} registered successfully with vulnerable method";
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // Prevention methods
        // only bind the properties that are needed by the user

        // Safe method-1
        // specify the properties in BindAttribute that are allowed to be bound
        [HttpPost]
        public IActionResult RegisterSafe1([Bind(nameof(user.Username), nameof(user.Email), nameof(user.Password))] User user)
        {
            if (ModelState.IsValid)
            {
                // Save user to database
                var newUser = new UserCRUD();
                newUser.Add(user);
                TempData["Message"] = $"{user.Email} registered successfully with safe method 1";
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // Safe method-2
        // use BindNeverAttribute in User model with properties that are not allowed to be bound to prevent binding of sensitive properties
        [HttpPost]
        public IActionResult RegisterSafe2(User user)
        {
            if (ModelState.IsValid)
            {
                // Save user to database
                var newUser = new UserCRUD();
                newUser.Add(user);
                TempData["Message"] = $"{user.Email} registered successfully with safe method 2";
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // Safe method-3
        // specify the properties in UserViewModel that are allowed to be bound
        [HttpPost]
        public IActionResult RegisterSafe3(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                // Save user to database
                var newUser = new UserCRUD();
                newUser.Add(new User
                {
                    Username = user.FullName,
                    Email = user.UserEmail,
                    Password = user.UserPassword
                });
                TempData["Message"] = $"{user.UserEmail} registered successfully with safe method 3";
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // Safe method-3 alternative
        // specify the properties in UserViewModelAlternative that are allowed to be bound
        [HttpPost]
        public IActionResult RegisterSafe4(UserViewModelAlternate user)
        {
            if (ModelState.IsValid)
            {
                // Save user to database
                var newUser = new UserCRUD();
                newUser.Add(new User
                {
                    Username = user.Username,
                    Email = user.Email,
                    Password = user.Password
                });
                TempData["Message"] = $"{user.Email} registered successfully with safe method 4";
                return RedirectToAction("Index");
            }
            return View(user);
        }
    }
}