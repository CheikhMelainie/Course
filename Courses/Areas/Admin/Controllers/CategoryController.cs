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
                    ParentName = item.Category2?.Name
                });
            }

            return View(categoriesList);
        }

        public ActionResult Create()
        {
            var categoryModel = new CategoryModel();
            InitMainCategories(null, ref categoryModel);
            return View(categoryModel);
        }

        [HttpPost]
        public ActionResult Create(CategoryModel data)
        {
          
                int creationResult = categoryService.Create(new Data.Category
                {
                    Name = data.Name,
                    Parent_id = data.ParentId
                });

                if(creationResult == -2)
                {
                InitMainCategories(null, ref data);

                ViewBag.Message = "Category Name is exists!";
                    return View (data);
                }

                return RedirectToAction("Index");
            
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

            InitMainCategories(currentCategory.ID, ref categoryModel);

            return View(categoryModel);
        }
        [HttpPost]
        public ActionResult Edit(CategoryModel data)
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
                    InitMainCategories(data.Id, ref data);
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
            InitMainCategories(data.Id, ref data);
            return View(data);
        }

        public ActionResult Delete(int? Id)
        {
            if(Id != null)
            {
                var category = categoryService.ReadById(Id.Value);
                var categoryInfo = new CategoryModel
                {
                    Id = category.ID,
                    Name = category.Name,
                    ParentName = category.Category2?.Name
                };

                return View(categoryInfo);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int? Id)
        {
            if(Id!=null)
            {
              var deleted =   categoryService.Delete(Id.Value);
                if(deleted)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Delete", new { Id = Id });
            }
            return HttpNotFound();
        }

        private void InitMainCategories(int? categoryToExclude, ref CategoryModel categoryModel)
        {
            var categoriesList = categoryService.ReadAll();

            if(categoryToExclude!=null)
            {
                var currentcategory = categoriesList.Where(c => c.ID == categoryToExclude).FirstOrDefault();
                categoriesList.Remove(currentcategory);
            }

            categoryModel.MainCategories = new SelectList(categoriesList, "ID", "Name");
        }
    }
}