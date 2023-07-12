using Courses.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Courses.Services
{
    public interface ITraineeCourseService
    {
        IEnumerable<Trainee_Courses> GetTrainees(int? courseId = null);
    }
    public class TraineeCourseService : ITraineeCourseService
    {
        private readonly Courses_DBEntities1 courses_DBEntity;
        public TraineeCourseService()
        {
            courses_DBEntity = new Courses_DBEntities1();
        }

        public IEnumerable<Trainee_Courses> GetTrainees(int? courseId = null)
        {
            return courses_DBEntity.Trainee_Courses.Where(t =>
                            courseId == null || t.Course_id == courseId);
        }
    }
}