
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MyCourse.Models.Services.Application;
using MyCourse.Models.ViewModels;

namespace MyCourse.Controllers
{
    public class CoursesController : Controller
    {
        //ctor + tab per creare un costruttire
        //aggiungere il servizio anche nella classe startup
        //private readonly CourseService courseService;       
        private readonly ICourseService courseService;
        //public CoursesController(CourseService courseService)
        public CoursesController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        public IActionResult Index()
        {
            //return Content("Sono Index");
            //CourseService courseService = new CourseService();
            //oppure
            //var courseService = new CourseService();
            List<CourseViewModel> courses = courseService.GetCourses();
            ViewData["Title"] = "Catalogo Dei Corsi !!";
            return View(courses);
        }

        public IActionResult Detail(int id)
        {
            //return Content($"Sono Detail, ho ricevuto l'id {id}");
            //var courseService = new CourseService();
            CourseDetailViewModel viewModel = courseService.GetCourse(id);
            //viewdata e viewbag servono allo stesso scopo
            ViewBag.Title = viewModel.Title;
            ViewData["Title"] = viewModel.Title;
            return View(viewModel);
        }
    }
}