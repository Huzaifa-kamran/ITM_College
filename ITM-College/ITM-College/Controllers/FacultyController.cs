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
