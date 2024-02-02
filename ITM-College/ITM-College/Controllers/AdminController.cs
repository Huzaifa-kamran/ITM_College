using Microsoft.AspNetCore.Mvc;

namespace ITM_College.Controllers
{
    public class AdminController : Controller
    {
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

    }
}
