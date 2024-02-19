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
            return View();
        }
		public IActionResult Course(string message)
		{
			ViewBag.message = message;
			var course = db.Courses.Include(c => c.Faculty).Include(f => f.Faculty.FacultyDepartmentNavigation).ToList();
			return View();
		}
		public IActionResult Department(string message)
		{
			ViewBag.message = message;
			var dep = db.Departments.Include(d => d.Faculties).ToList();
			return View(dep);
		}
		public IActionResult Student()
		{
			return View();
		}
	}
}
