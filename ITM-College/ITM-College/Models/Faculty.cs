using System;
using System.Collections.Generic;

namespace ITM_College.Models
{
    public partial class Faculty
    {
        public Faculty()
        {
            Courses = new HashSet<Course>();
        }

        public int FacultyId { get; set; }
        public string? FacultyName { get; set; }
        public string? FacultyEmail { get; set; }
        public string? FacultyPassword { get; set; }
        public int? FacultyDepartment { get; set; }
        public string? FacultyImg { get; set; }
        public int? Gender { get; set; }
        public int? Role { get; set; }

        public virtual Department? FacultyDepartmentNavigation { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
