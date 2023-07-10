using Courses.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Courses.Services
{
    public interface ICourseService
    {
        int Create(Course course);
        int Update(Course updatedCourse);
        List<Course> ReadAll(string query = null, int? trainerId = null, int? categoryId = null);
        Course Get(int Id);
    }
    public class CourseService : ICourseService
    {
        private readonly Courses_DBEntities1 db;
        public CourseService()
        {
            db = new Courses_DBEntities1();
        }
        public int Create(Course course)
        {
            course.Creation_Date = DateTime.Now;

            db.Courses.Add(course);
            return db.SaveChanges();
        }

        public Course Get(int Id)
        {
            return db.Courses.Find(Id);
        }

        public List<Course> ReadAll(string query = null, int? trainerId = null, int? categoryId = null)
        {
            return db.Courses.Where(c =>
                                        (trainerId == null || c.Trainer_id == trainerId)
                                      && (categoryId == null || c.Category_id == categoryId)
                                      && (query == null || c.Name.Contains(query))).ToList();
        }

        public int Update(Course updatedCourse)
        {
            db.Courses.Attach(updatedCourse);
            db.Entry(updatedCourse).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
    }
}