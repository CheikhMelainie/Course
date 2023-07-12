using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Courses.Models
{
    public class TraineeCourseModel
    {
        public int CourseId { get; set; }
        public DateTime Registration_Date { get; set; }
        public TraineeModel Trainee { get; set; }
    }

    public class TraineeModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}