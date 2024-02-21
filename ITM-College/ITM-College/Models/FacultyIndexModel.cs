namespace ITM_College.Models
{
	public class FacultyIndexModel
	{
		public int CoursesCount { get; set; }
		public Faculty faculty { get; set; }

		public List<Course> courses { get; set; }

		public List<StudentCourseRegistration> students { get; set; }
	}
}
