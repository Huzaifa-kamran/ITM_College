using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ITM_College.Models
{
    public partial class Course
    {
        public Course()
        {
            StudentCourseRegistrations = new HashSet<StudentCourseRegistration>();
        }

        public int CourseId { get; set; }

        [Required(ErrorMessage = "Course Name is required.")]
        public string? CourseName { get; set; }

        [Required(ErrorMessage = "Course Description is required.")]
        public string? CourseDesc { get; set; }

        [Display(Name = "Image")] // Custom display name
        public string? CourseImg { get; set; }

        [Required(ErrorMessage = "Course Duration is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Course Duration must be a positive number.")]
        public int? CourseDuration { get; set; }

        [Display(Name = "Faculty")] // Custom display name
        public int? FacultyId { get; set; }

        public virtual Faculty? Faculty { get; set; }
        public virtual ICollection<StudentCourseRegistration> StudentCourseRegistrations { get; set; }
    }
}
