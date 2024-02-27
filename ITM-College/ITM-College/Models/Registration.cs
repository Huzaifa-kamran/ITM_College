using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ITM_College.Models
{
	public class Registration
	{
		public StudentCourseRegistration StudentReg { get; set; }
		public PreviousExam PreviousExams { get; set; }
	}
}
