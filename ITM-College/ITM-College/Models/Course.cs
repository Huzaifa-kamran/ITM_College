using System;
using System.Collections.Generic;

namespace ITM_College.Models
{
    public partial class Course
    {
        public Course()
        {
            StudentCourseRegistrations = new HashSet<StudentCourseRegistration>();
        }

        public int CourseId { get; set; }
        public string? CourseName { get; set; }
        public string? CourseDesc { get; set; }
        public string? CourseImg { get; set; }
        public string? CourseDuration { get; set; }
        public int? FacultyId { get; set; }

        public virtual Faculty? Faculty { get; set; }
        public virtual ICollection<StudentCourseRegistration> StudentCourseRegistrations { get; set; }
    }
}
