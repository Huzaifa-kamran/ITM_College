namespace ITM_College.Models
{
	public class DashboardData
	{
		public int DepartmentsCount { get; set; }
		public int FacultiesCount { get; set; }
		public int StudentsCount { get; set; }
		public int CoursesCount { get; set; }

		public List<Department> Departments { get; set; }
		public List<Faculty> Faculties { get; set; }
		public List<Student> Students { get; set; }
		public List<Course> Courses { get; set; }

		public List<StudentCourseRegistration> newStd { get; set; }
	}
}