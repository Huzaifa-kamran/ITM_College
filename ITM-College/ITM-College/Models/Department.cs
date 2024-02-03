using System;
using System.Collections.Generic;

namespace ITM_College.Models
{
    public partial class Department
    {
        public Department()
        {
            Faculties = new HashSet<Faculty>();
        }

        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public string? DepartmentDesc { get; set; }

        public virtual ICollection<Faculty> Faculties { get; set; }
    }
}
