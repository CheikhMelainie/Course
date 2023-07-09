﻿using Courses.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Courses.Services
{
    public interface ICategoryService
    {
        int Update(Category updatedCategory);
        Category ReadById(int id);
        List<Category> ReadAll();
        int Create(Category newCategory);
        bool Delete(int id);
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

        public bool Delete(int id)
        {
            var category = ReadById(id);
            if(category != null)
            {
                db.Categories.Remove(category);
                return  db.SaveChanges() > 0 ? true : false;
            }

            return false;
        }

        public List<Category> ReadAll()
        {
           return  db.Categories.ToList();
        }

        public Category ReadById(int id)
        {
            return db.Categories.Find(id);
        }

        public int Update(Category updatedCategory)
        {
            var categoryName = updatedCategory.Name.ToLower();
            var categoriesList = db.Categories.Where(c => c.Name.ToLower() != categoryName);

            if (categoriesList.Where(c => c.Name.ToLower() == categoryName).Any()) 
            {
                return - 2;
            }


            db.Categories.Attach(updatedCategory);
            db.Entry(updatedCategory).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();

           
        }
    }
}