//COSA ESTRARRE
using System;
using System.Collections.Generic;
using MyCourse.Models.ViewModels;
using MyCourse.Models.Enums;
using MyCourse.Models.ValueTypes;

namespace MyCourse.Models.Services.Application
{
    public class CourseService :ICourseService
    {
        public List<CourseViewModel> GetCourses()
        {
            //throw new NotImplementedException();
            var courseList = new List<CourseViewModel>();
            var rand = new Random();
            for (int i = 0; i <=20; i++)
            {
                var price = Convert.ToDecimal(rand.NextDouble()*10*10);
                var course = new CourseViewModel
                {
                    Id=i,
                    Title = $"Corso {i}",
                    CurrentPrice = new Money(Currency.EUR,price),
                    FullPrice = new Money(Currency.EUR,rand.NextDouble()>0.5 ? price : price-1),
                    Author = "Nome Cognome",
                    Rating = rand.Next(10,50)/10.0,
                    ImagePath = "logo.png"
                };
                courseList.Add(course);
            }

            return courseList;
        }

        public CourseDetailViewModel GetCourse(int id)
        {

            //throw new NotImplementedException();
            var rand = new Random();
            var price = Convert.ToDecimal(rand.NextDouble() * 10 + 10);
            var course = new CourseDetailViewModel
            {
                Id = id,
                Title = $"Corso {id}",
                CurrentPrice = new Money(Currency.EUR, price),
                FullPrice = new Money(Currency.EUR, rand.NextDouble() > 0.5 ? price : price - 1),
                Author = "Nome Cognome",
                Rating = rand.Next(10, 50) / 10,
                ImagePath = "/logo.svg",
                Description = $"Descrizione {id}",
                //TotalCourseDuration = TimeSpan.FromSeconds(rand.Next(500, 1500)),
                Lessons = new List<LessonViewModel>()
            };

            for (var i=1; i <= 5; i++)
            {
                var lesson = new LessonViewModel
                {
                    Title = $"Lezione {i}",
                    Description = $"Descrizione {i}",
                    Duration = TimeSpan.FromSeconds(rand.Next(40, 90))                    
                };
                course.Lessons.Add(lesson);
            }

            return course;
        }
    }
}