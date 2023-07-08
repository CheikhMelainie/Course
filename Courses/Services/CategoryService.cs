using Courses.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Courses.Services
{
    public interface ICategoryService
    {
        List<Category> ReadAll();
        int Create(Category newCategory);
    }
    public class CategoryService : ICategoryService
    {
        private readonly Courses_DBEntities1 db;
        public CategoryService()
        {
            db = new Courses_DBEntities1();
        }

        public int Create(Category newCategory)
        {
            var categoryName = newCategory.Name.ToLower();
            var categoryNameExixts = db.Categories.Where(c => c.Name.ToLower() == categoryName).Any();
            if (categoryNameExixts)
            {
                return -2;
            }

            db.Categories.Add(newCategory);
            return db.SaveChanges();
        }

        public List<Category> ReadAll()
        {
           return  db.Categories.ToList();
        }
    }
}