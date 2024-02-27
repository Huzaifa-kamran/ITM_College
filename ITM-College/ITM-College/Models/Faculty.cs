using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ITM_College.Models
{
    public partial class Faculty
    {
        public Faculty()
        {
            Courses = new HashSet<Course>();
        }

        public int FacultyId { get; set; }

        [Required(ErrorMessage = "Faculty Name is required.")]
        public string? FacultyName { get; set; }

        [Required(ErrorMessage = "Faculty Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? FacultyEmail { get; set; }

        [Required(ErrorMessage = "Faculty Password is required.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 50 characters.")]
        public string? FacultyPassword { get; set; }

        [Required(ErrorMessage = "Faculty Department is required.")]
        public int? FacultyDepartment { get; set; }

        // You can add additional validations for other properties as needed

        public string? FacultyImg { get; set; }
        public int? Gender { get; set; }
        public int? Role { get; set; }

        public virtual Department? FacultyDepartmentNavigation { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
