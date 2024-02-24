using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ITM_College.Models
{
    public partial class Department
    {
        public Department()
        {
            Faculties = new HashSet<Faculty>();
        }

        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Department Name is required.")]
        public string? DepartmentName { get; set; }

        [Required(ErrorMessage = "Department Description is required.")]
        public string? DepartmentDesc { get; set; }

        public virtual ICollection<Faculty> Faculties { get; set; }
    }
}
