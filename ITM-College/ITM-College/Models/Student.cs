using System;
using System.Collections.Generic;

namespace ITM_College.Models
{
    public partial class Student
    {
        public Student()
        {
            StudentCourseRegistrations = new HashSet<StudentCourseRegistration>();
        }

        public int StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? StudentEmail { get; set; }
        public string? Password { get; set; }
        public int? Role { get; set; }
        public string? StdImg { get; set; }

        public virtual ICollection<StudentCourseRegistration> StudentCourseRegistrations { get; set; }
    }
}
