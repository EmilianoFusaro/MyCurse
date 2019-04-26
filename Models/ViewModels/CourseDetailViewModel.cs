﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyCourse.Models.ValueTypes;
using MyCourse.Models.ViewModels;

namespace MyCourse.Models.ViewModels
{
    public class CourseDetailViewModel : CourseViewModel
    {
        //scrivere prop + tab
        public string Description { get; set; }
        public List<LessonViewModel> Lessons { get; set; }
        //public TimeSpan TotalCourseDuration { get; set; }

        public TimeSpan TotalCourseDuration
        {
            get => TimeSpan.FromSeconds(Lessons?.Sum(l => l.Duration.TotalSeconds) ?? 0);
        }
    }
}