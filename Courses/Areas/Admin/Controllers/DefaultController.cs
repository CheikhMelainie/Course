using Courses.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Courses.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DefaultController : Controller
    {
        Courses_DBEntities1 context = new Courses_DBEntities1();

        // GET: Admin/Default
        public ActionResult Index()
        {
            int nbrCours = context.Courses.Count();
            ViewBag.nbrCourse = nbrCours;

            int nbrTrainer = context.Trainees.Count();
            ViewBag.nbrTrainer = nbrTrainer;

            int nbrTrainee = context.Trainees.Count();
            ViewBag.Trainee = nbrTrainee;

            int nbrCategory = context.Categories.Count();
            ViewBag.Categiry = nbrCategory;

            return View();
        }
    }
}