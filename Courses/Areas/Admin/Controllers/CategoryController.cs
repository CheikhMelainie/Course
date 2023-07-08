using Courses.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Courses.Models;

namespace Courses.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService categoryService;
        public CategoryController()
        {
            categoryService = new CategoryService();
        }

        // GET: Admin/Category
        public ActionResult Index()
        {
            var categories = categoryService.ReadAll();
            var categoriesList = new List<Category>();
            foreach (var item in categories)
            {
                categoriesList.Add(new Category
                {
                    Id = item.ID,
                    Name = item.Name,
                    ParentId = item.Parent_id,
                    ParentName = item.Category2?.Name
                });
            }

            return View(categoriesList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category data)
        {
            if(ModelState.IsValid)
            {
                int creationResult = categoryService.Create(new Data.Category
                {
                    Name = data.Name
                });

                if(creationResult == -2)
                {
                    ViewBag.Message = "Category Name is exists!";
                    return View (data);
                }

                return RedirectToAction("Index");
            }
            return View();
        }
    }
}