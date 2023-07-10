using AutoMapper;
using Courses;
using Courses.Data;
using Courses.Models;
using Courses.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Courses.Controllers
{
    public class CourseController : Controller
    {
        private readonly IMapper mapper;
        private readonly CourseService courseService;
        public CourseController()
        {
            mapper = AutoMapperConfig.Mapper;

            courseService = new CourseService();
        }
        // GET: Course
        public ActionResult Index()
        {
            var courses = courseService.ReadAll();
            return View(mapper.Map<List<Course>, List<CourseModel>>(courses));
        }

        public ActionResult Info(int? Id)
        {
            if (Id == null || Id == 0)
                return HttpNotFound("This course not found!");

            var courseInfo = courseService.Get(Id.Value);
            if (courseInfo == null)
                return HttpNotFound("This course not found!");

            var mappedCourseInfo = mapper.Map<Course, CourseModel>(courseInfo);

            return View(mappedCourseInfo);
        }


        public ActionResult Subscribe(int Id)
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = $"/Course/Subscribe/{Id}" });
            }

            return View();
        }

    }
}