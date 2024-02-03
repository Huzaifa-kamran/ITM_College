using System;
using System.Collections.Generic;

namespace ITM_College.Models
{
    public partial class PreviousExam
    {
        public int ExamId { get; set; }
        public int? StudentDataId { get; set; }
        public string? InstituteName { get; set; }
        public int? EnrollmentNumber { get; set; }
        public string? Center { get; set; }
        public string? Stream { get; set; }
        public string? Field { get; set; }
        public int? Marks { get; set; }
        public int? OutOf { get; set; }
        public int? ClassObtained { get; set; }
        public string? Sports { get; set; }

        public virtual StudentCourseRegistration? StudentData { get; set; }
    }
}
