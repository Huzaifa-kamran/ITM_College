using ITM_College.Data;
using ITM_College.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITM_College.Controllers
{

    public class FacultyController : Controller
    {
		private readonly ITM_CollegeContext db;
        private readonly IHttpContextAccessor contx;
        public FacultyController(ITM_CollegeContext db, IHttpContextAccessor contx)
		{
			this.db = db;
            this.contx = contx;
        }
		public IActionResult Index()
        {

			string sessionId = HttpContext.Session.GetString("sessionId");
			var facid = Convert.ToInt32(sessionId);
            var data = new FacultyIndexModel
			{
				CoursesCount = db.Courses.Where(col => col.FacultyId == facid).Count(),

				faculty = db.Faculties.Include(d => d.FacultyDepartmentNavigation).FirstOrDefault(col => col.FacultyId == facid),

				courses = db.Courses.Where(f=>f.FacultyId == facid).ToList(),

				students = db.StudentCourseRegistrations
	           .Include(s => s.AddmissionForNavigation)
		       .ThenInclude(a => a.Faculty)
	           .Include(c => c.Student)
	           .Where(s => s.AddmissionForNavigation.FacultyId == facid).Where(s=>s.Status == 2)
	           .ToList(),

		};
            return View(data);
        }
<<<<<<< HEAD
		

		
=======
		public IActionResult Assignments(string message)
		{
			ViewBag.message = message;
			var course = db.Courses.Where(col=>col.FacultyId == 1).ToList();
			return View(course);
		}

		public IActionResult AssignmentOfCourse(int id)
		{

			var assignments = db.Assignments.Include(c=>c.Course).Include(f=>f.Faculty).Where(col => col.CourseId == id).ToList();
			return View(assignments);
		}

		public IActionResult AssignmentDetail(int id)
		{

			var assignment = db.Assignments.Include(c => c.Course).Include(f => f.Faculty).FirstOrDefault(a=>a.Id == id);
			return View(assignment);
		}
>>>>>>> 7cf72848e5a11946f323e0bb09c0a99ab08fa160
		public IActionResult Student()
		{
            string sessionId = HttpContext.Session.GetString("sessionId");
            var facid = Convert.ToInt32(sessionId);
            var students = db.StudentCourseRegistrations
			   .Include(s => s.AddmissionForNavigation)
			   .ThenInclude(a => a.Faculty)
			   .Include(c => c.Student)
			   .Where(s => s.AddmissionForNavigation.FacultyId == facid).Where(s => s.Status == 2)
			   .ToList();
			return View(students);
		}

	}
}
