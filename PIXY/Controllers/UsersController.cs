using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PIXY.Data;
using PIXY.Models;

namespace PIXY.Controllers
{
    public class UsersController : Controller
    {
        private readonly PIXYContext _context;

        public UsersController(PIXYContext context)
        {
            _context = context;
        }

        // GET: Users/Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (username != null && password != null) {
                var user = await _context.Users
                    .FirstOrDefaultAsync(m => m.UserName == username && m.Password == password);

                if (user == null)
                {
                    // User not found
                    ViewBag.error = "User name or password is wrong";
                    return View();
                }
                else
                {
                    HttpContext.Session.SetInt32("UserID", user.ID);
                    HttpContext.Session.SetString("LastName", user.LastName);
                    HttpContext.Session.SetString("FirstName", user.FirstName);
                    HttpContext.Session.SetString("Address", user.Address);
                    return RedirectToAction("Index", "Images");
                }
            } else {
                // Either username or password is not inputted
                ViewBag.error = "User name or password is wrong";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserID");
            HttpContext.Session.Remove("LastName");
            HttpContext.Session.Remove("FirstName");
            HttpContext.Session.Remove("Address");
            return RedirectToAction("Index", "Images");
        }


        // GET: Users/SignUp
        public IActionResult SignUp()
        {
            return View();
        }

        // POST: Users/SignUp
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp([Bind("ID,LastName,FirstName,Email,PhoneNumber,Address,UserName,Password,ConfirmPassword")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();

                // Sign in user
                HttpContext.Session.SetInt32("UserID", user.ID);
                HttpContext.Session.SetString("LastName", user.LastName);
                HttpContext.Session.SetString("FirstName", user.FirstName);
                HttpContext.Session.SetString("Address", user.Address);
            }
            return RedirectToAction("Index", "Images");
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit()
        {

            if (HttpContext.Session.GetInt32("UserID") == null)
            {
                // Haven't login
                return RedirectToAction("Login", "Users");
            }
            else
            {
                // Have login
                int UserID = (int)HttpContext.Session.GetInt32("UserID");
                var user = await _context.Users.FindAsync(UserID);
                
                if (user == null)
                {
                    
                    return RedirectToAction("Login", "Users");
                }
                else
                {
                    UserVM userVM = new UserVM();
                    userVM.ID = user.ID;
                    userVM.LastName = user.LastName;
                    userVM.FirstName = user.FirstName;
                    userVM.Email = user.Email;
                    userVM.PhoneNumber = user.PhoneNumber;
                    userVM.Address = user.Address;
                    userVM.UserName = user.UserName;
                    userVM.Password = user.Password;
                    userVM.ConfirmPassword = user.Password;   //autofill ConfirmPassword
                    return View(userVM);
                }
            }
                
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstName,Email,PhoneNumber,Address,UserName,Password,ConfirmPassword")] UserVM userVM)
        {
            //ModelState.Remove("UserName");
            if (id != userVM.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    User user = new User();
                    user.ID = userVM.ID;
                    user.LastName = userVM.LastName;
                    user.FirstName = userVM.FirstName;
                    user.Email = userVM.Email;
                    user.PhoneNumber = userVM.PhoneNumber;
                    user.Address = userVM.Address;
                    user.UserName = userVM.UserName;
                    user.Password = userVM.Password;

                    _context.Update(user);
                    await _context.SaveChangesAsync();

                    // Refresh Displayed Name
                    HttpContext.Session.SetString("LastName", user.LastName);
                    HttpContext.Session.SetString("FirstName", user.FirstName);
                    HttpContext.Session.SetString("Address", user.Address);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(userVM.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Images");
            }
            return RedirectToAction("Index", "Images");
        }

        private bool UserExists(int id)
        {
          return _context.Users.Any(e => e.ID == id);
        }

        [HttpPost]
        public JsonResult IsAlreadySigned(string UserName)
        {

            return Json(IsUserAvailable(UserName));

        }
        public bool IsUserAvailable(string UserName)
        {

            var RegUserName = (from u in _context.Users
                              where u.UserName.ToUpper() == UserName.ToUpper()
                              select new { UserName }).FirstOrDefault();

            bool status;
            if (RegUserName != null)
            {
                //Already registered  
                status = false;
            }
            else
            {
                //Available to use  
                status = true;
            }

            return status;
        }
    }
}
