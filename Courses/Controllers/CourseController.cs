using Courses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Courses.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult Index()
        {
            var lstCourses = new List<CourseModel>
            {
                new CourseModel
                {
                    Id = 1,
                    Title = "hhhh",
                    Description="hohoho"
                },
                  new CourseModel
                {
                    Id = 2,
                    Title = "kkkkk",
                    Description="kokoko"
                },
                  new CourseModel
                {
                    Id = 3,
                    Title = "llllll",
                    Description="lolololo"
                }
            };

            return View(lstCourses);
        }
    }
}