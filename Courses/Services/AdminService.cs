using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Courses.Data;
namespace Courses.Services
{
    public interface IAdminService
    {
        bool Login(string Email, string Password);
        bool ChangePassword(string Email, string Password);
        bool ForgotPassword(string Email);
    }

    public class AdminService : IAdminService
    {
        public Courses_DBEntities1 context { get; set; }

        public AdminService()
        {
            context = new Courses_DBEntities1();
        }

        public bool Login(string Email, string Password)
        {
           return context.Admins
                .Where(a => a.Email == Email && a.Password == Password)
                .Any();

        }

        public bool ChangePassword(string Email, string Password)
        {
            throw new NotImplementedException();
        }

        public bool ForgotPassword(string Email)
        {
            throw new NotImplementedException();
        }

    }

    public class CoursesDBEntity
    {
    }
}