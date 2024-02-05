using ITM_College.Data;
using ITM_College.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITM_College.Controllers
{
    public class AdminController : Controller
    {

		private readonly ITM_CollegeContext db;
        public AdminController(ITM_CollegeContext db)
        {
			this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

		// ------ Controller 1 Faculty Controller ------
		// i- All Faculties
		// ii- Add Faculties
		// iii- Update Faculty
		// iv- Delete Faculty
		public IActionResult Faculty()
        {
            return View();
        }
		[HttpGet]
		public IActionResult AddFaculty()
		{
			return View();
		}
		//[HttpPost]
		//public IActionResult AddFaculty()
		//{
		//	return View();
		//}

		[HttpGet]
		public IActionResult UpdateFaculty()
		{
			return View();
		}
		//[HttpPost]
		//public IActionResult UpdateFaculty()
		//{
		//	return View();
		//}

		public IActionResult DeleteFaculty()
		{
			return View();
		}
		// Faculty Controller End


		// ------ Controller 2 Student Controller ------
		// i- All Students
		// ii- Add Students
		// iii- Update Student
		// iv- Delete Student
		public IActionResult Students()
		{
			return View();
		}

		[HttpGet]
		public IActionResult AddStudent()
		{
			return View();
		}
		//[HttpPost]
		//public IActionResult AddStudent()
		//{
		//	return View();
		//}

		[HttpGet]
		public IActionResult UpdateStudent()
		{
			return View();
		}
		//[HttpPost]
		//public IActionResult UpdateStudent()
		//{
		//	return View();
		//}

		public IActionResult DeleteStudent()
		{
			return View();
		}
        // Students Controller End


        // ------ Controller 3 Courses Controller ------
        // i- All Students
        // ii- Add Students
        // iii- Update Student
        // iv- Delete Student
        public IActionResult Courses()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddCourse()
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult AddCourse()
        //{
        //	return View();
        //}

        [HttpGet]
        public IActionResult UpdateCourse()
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult UpdateCourse()
        //{
        //	return View();
        //}

        public IActionResult DeleteCourse()
        {
            return View();
        }
        // Courses Controller End



        // ------ Controller 4 Department Controller ------
        // i- All Departments
        // ii- Add Department
        // iii- Update Department
        // iv- Delete Department
        public IActionResult Departments(string message)
        {
            ViewBag.message = message;
            return View(db.Departments.ToList());
        }

        [HttpGet]
        public IActionResult AddDepartment()
        {
            return View();
        }
		[HttpPost]
		public IActionResult AddDepartment(Department dep)
		{
			var asjd = dep;
			if (ModelState.IsValid)
			{
				db.Departments.Add(dep);
				db.SaveChanges();
				ViewBag.message = "Department Add Successfully";
				return RedirectToAction("Departments", new { message = ViewBag.message });
			}
			return View();
		}

		[HttpGet]
        public IActionResult UpdateDepartment(int id)
        {
			Department department = db.Departments.FirstOrDefault(cols=>cols.DepartmentId == id);
            return View(department);
        }
		[HttpPost]
		public IActionResult UpdateDepartment(Department dep)
		{
			var updatedDep = dep;
			var id = updatedDep.DepartmentId;
			Department fetchDepartment = db.Departments.FirstOrDefault(cols => cols.DepartmentId == id);

			if (fetchDepartment != null)
			{
				fetchDepartment.DepartmentName = updatedDep.DepartmentName;
				fetchDepartment.DepartmentDesc = updatedDep.DepartmentDesc;

				db.SaveChanges();
				ViewBag.message = "Department Updated Successfully";
				return RedirectToAction("Departments", new { message = ViewBag.message });
			}

			return View();
		}

		public IActionResult DeleteDepartment(int id)
        {
			Department dep = db.Departments.Find(id);
			db.Departments.Remove(dep);
			db.SaveChanges();
			ViewBag.message = "Department Deleted Successfully";
			return RedirectToAction("Departments", new { message = ViewBag.message });
		}
		// Department Controller End



		// ------ Controller 5 Facilities Controller ------
		// i- All Departments
		// ii- Add Department
		// iii- Update Department
		// iv- Delete Department
		public IActionResult Facilities()
		{
			return View();
		}

		[HttpGet]
		public IActionResult AddFacility()
		{
			return View();
		}
		//[HttpPost]
		//public IActionResult AddFacility()
		//{
		//	return View();
		//}

		[HttpGet]
		public IActionResult UpdateFacility()
		{
			return View();
		}
		//[HttpPost]
		//public IActionResult UpdateFacility()
		//{
		//	return View();
		//}

		public IActionResult DeleteFacility()
		{
			return View();
		}
		// Facilities Controller End
	}
}
