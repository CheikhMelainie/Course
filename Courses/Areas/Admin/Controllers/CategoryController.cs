using Courses.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Courses.Models;
using Courses.Data;

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
            var categoriesList = new List<CategoryModel>();
            foreach (var item in categories)
            {
                categoriesList.Add(new CategoryModel
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
        public ActionResult Create(CategoryModel data)
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

        public ActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return RedirectToAction("index", "Home");
            }

            var currentCategory = categoryService.ReadById(id.Value);

            if(currentCategory == null)
            {
                return HttpNotFound($"This category ({id}) not found! ");
            }
            var categoryModel = new CategoryModel
            {
                Id = currentCategory.ID,
                Name = currentCategory.Name,
                ParentId = currentCategory.Parent_id
            };

            return View(categoryModel);
        }
        [HttpPost]
        public ActionResult Edit(CategoryModel data)
        {
            if(ModelState.IsValid)
            {
                var updatedCategory = new Category
                {
                    ID = data.Id,
                    Name = data.Name,
                    Parent_id = data.ParentId
                };
                var result = categoryService.Update(updatedCategory);

                if (result == -2)
                {
                    ViewBag.Message = "Category Name is exists!";
                    return View(data);
                }
                else if (result > 0)
                {
                    ViewBag.Success = true;
                    ViewBag.Message = $"Category ({data.Id}) updated successfully";
                }
                else
                {
                    ViewBag.Message = $"An error occured!";
                }
            }
            return View(data);
        }
    }
}