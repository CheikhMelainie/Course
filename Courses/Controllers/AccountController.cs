using Courses.Data;
using Courses.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Courses.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<MyIdentityUser> userManager;

        public AccountController()
        {
            var db = new CoursesIdentityContext();
            var userStore = new UserStore<MyIdentityUser>(db);
            userManager = new UserManager<MyIdentityUser>(userStore);
        }
            

        // GET: Account
        public async Task<ActionResult> Login()
        {
           var user = await userManager.CreateAsync(new MyIdentityUser
            {
                Email = "ao@ao.com",
                UserName = "ao@ao.com"

            }, "123456");
            ViewBag.User = user.Succeeded;
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel userInfo)
        {
            if (ModelState.IsValid)
            {
                var identityUser = new MyIdentityUser
                {
                    Email = userInfo.Email,
                    UserName = userInfo.Email
                };
                var creationResult = await userManager.CreateAsync(identityUser, userInfo.Password);


                // User Created
                if (creationResult.Succeeded)
                {
                    var userId = identityUser.Id;
                    creationResult = userManager.AddToRole(userId, "Admin");

                    return RedirectToAction("Index", "Default", new { area = "Admin" });
                }

                var message = creationResult.Errors.FirstOrDefault();

                userInfo.Message = message;
                return View(userInfo);
            }

            return View(userInfo);
        }
    }
}