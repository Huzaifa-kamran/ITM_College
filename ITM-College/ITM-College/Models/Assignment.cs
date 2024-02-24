using System;
using System.Collections.Generic;

namespace ITM_College.Models
{
    public partial class Assignment
    {
        public int Id { get; set; }
        public int? FacultyId { get; set; }
        public int? CourseId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Media { get; set; }
        public DateTime? UploadDate { get; set; }
        public DateTime? MaxDate { get; set; }
        public int? TotalMarks { get; set; }

        public virtual Course? Course { get; set; }
        public virtual Faculty? Faculty { get; set; }
    }
}
