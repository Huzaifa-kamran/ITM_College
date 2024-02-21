using ITM_College.Data;
using ITM_College.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITM_College.Controllers
{

    public class FacultyController : Controller
    {
		private readonly ITM_CollegeContext db;
		public FacultyController(ITM_CollegeContext db)
		{
			this.db = db;
		}
		public IActionResult Index()
        {
			var data = new FacultyIndexModel
			{
				CoursesCount = db.Courses.Where(col => col.FacultyId == 1).Count(),

				faculty = db.Faculties.Include(d => d.FacultyDepartmentNavigation).FirstOrDefault(col => col.FacultyId == 1),

				courses = db.Courses.Where(f=>f.FacultyId == 1).ToList(),

				students = db.StudentCourseRegistrations
	           .Include(s => s.AddmissionForNavigation)
		       .ThenInclude(a => a.Faculty)
	           .Include(c => c.Student)
	           .Where(s => s.AddmissionForNavigation.FacultyId == 1).Where(s=>s.Status == 3)
	           .ToList(),

		};
            return View(data);
        }
		public IActionResult Assignments(string message)
		{
			ViewBag.message = message;
			var course = db.Courses.Where(col=>col.FacultyId == 1).ToList();
			return View(course);
		}
		public IActionResult Student()
		{
			var students = db.StudentCourseRegistrations
			   .Include(s => s.AddmissionForNavigation)
			   .ThenInclude(a => a.Faculty)
			   .Include(c => c.Student)
			   .Where(s => s.AddmissionForNavigation.FacultyId == 1).Where(s => s.Status == 3)
			   .ToList();
			return View(students);
		}

	}
}
