using System;
using System.Collections.Generic;

namespace ITM_College.Models
{
    public partial class StudentCourseRegistration
    {
        public StudentCourseRegistration()
        {
            PreviousExams = new HashSet<PreviousExam>();
        }

        public int Id { get; set; }
        public int? StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public DateTime? Dob { get; set; }
        public string? Gender { get; set; }
        public string? ResidentalAddress { get; set; }
        public string? PermanentAddress { get; set; }
        public int? AddmissionFor { get; set; }
        public string? TrackingId { get; set; }
        public int? Status { get; set; }

        public virtual Course? AddmissionForNavigation { get; set; }
        public virtual Student? Student { get; set; }
        public virtual ICollection<PreviousExam> PreviousExams { get; set; }
    }
}
