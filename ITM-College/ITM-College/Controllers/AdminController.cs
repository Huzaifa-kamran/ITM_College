using ITM_College.Data;
using ITM_College.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
		public IActionResult Faculty(string message)
        {
			var faculties = db.Faculties.Include(f => f.FacultyDepartmentNavigation);
			ViewBag.message = message;
			return View(faculties);
        }
		[HttpGet]
		public IActionResult AddFaculty()
		{
			FacultyAndDepartment viewModel = new FacultyAndDepartment
			{
				FacultyTable = new Faculty(),
				Departments = db.Departments.ToList()
			};
			return View(viewModel);
		}


		[HttpPost]
		public IActionResult AddFaculty(FacultyAndDepartment newFaculty,IFormFile img)
		{

			//checking Image path was null or not ?
			if (ModelState.IsValid)
			{
				if (img != null && img.Length > 0)
				{
					// GETTING IMAGE FILE EXTENSION 
					var fileExt = System.IO.Path.GetExtension(img.FileName).Substring(1);

					// GETTING IMAGE NAME
					var random = Path.GetFileName(img.FileName);

					// GUID ID COMBINE WITH IMAGE NAME - TO ESCAPE IMAGE NAME REDENDNCY 
					var FileName = Guid.NewGuid() + random;

					// GET PATH OF CUSTOM IMAGE FOLDER
					string imgFolder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/admincss/Faculty");

					// CHECKING FOLDER EXIST OR NOT - IF NOT THEN CREATE F0LDER 
					if (!Directory.Exists(imgFolder))
					{
						Directory.CreateDirectory(imgFolder);
					}

					// MAKING CUSTOM AND COMBINE FOLDER PATH WITH IMAHE 
					string filepath = Path.Combine(imgFolder, FileName);

					// COPY IMAGE TO REAL PATH TO DEVELOPER PATH
					using (var stream = new FileStream(filepath, FileMode.Create))
					{
						img.CopyTo(stream);
					}

					// READY SEND PATH TO  IMAGE TO DB  
					var dbAddress = Path.Combine("admincss/Faculty", FileName);

					// EQUALIZE TABLE (MODEL) PROPERTY WITH CUSTOM PATH 
					newFaculty.FacultyTable.FacultyImg = dbAddress;
					//MYIMAGES/imagetodbContext.JGP

					Faculty faculty = new Faculty();
					faculty.FacultyName = newFaculty.FacultyTable.FacultyName;
					faculty.FacultyEmail = newFaculty.FacultyTable.FacultyEmail;
					faculty.FacultyPassword = newFaculty.FacultyTable.FacultyPassword;
					faculty.gender = newFaculty.FacultyTable.gender;
					faculty.FacultyDepartment = newFaculty.FacultyTable.FacultyDepartment;
					faculty.FacultyImg = dbAddress;


					// SEND TO TABLE 
					db.Faculties.Add(faculty);
					db.SaveChanges();
					ViewBag.message = "Faculty Add Successfully";
					return RedirectToAction("Faculty", new { message = ViewBag.message });
				}
			}
			else
			{
				ViewBag.error = "Something went wrong";
				return RedirectToAction("AddFaculty");
			}

			return View();
		}

		[HttpGet]
		public IActionResult UpdateFaculty(int id)
		{
			Faculty faculty = db.Faculties.FirstOrDefault(cols => cols.FacultyId == id);
			var facs = new FacultyAndDepartment { 
				FacultyTable = faculty, 
				Departments = db.Departments.ToList() 
			};
			return View(facs);
		}

		[HttpPost]
		public IActionResult UpdateFaculty(FacultyAndDepartment updatedFaculty, IFormFile img)
		{
			// Check if the faculty ID is valid
			if (updatedFaculty.FacultyTable.FacultyId <= 0)
			{
				ViewBag.error = "Invalid faculty ID.";
				return RedirectToAction("Faculty");
			}

			// Retrieve the existing faculty from the database
			var existingFaculty = db.Faculties.FirstOrDefault(f => f.FacultyId == updatedFaculty.FacultyTable.FacultyId);

			if (existingFaculty == null)
			{
				ViewBag.error = "Faculty not found.";
				return RedirectToAction("Faculty");
			}

			// Check if a new image file is provided
			if (img != null && img.Length > 0)
			{
				// GETTING IMAGE FILE EXTENSION 
				var fileExt = System.IO.Path.GetExtension(img.FileName).Substring(1);

				// GETTING IMAGE NAME
				var random = Path.GetFileName(img.FileName);

				// GUID ID COMBINE WITH IMAGE NAME - TO ESCAPE IMAGE NAME REDENDNCY 
				var FileName = Guid.NewGuid() + random;

				// GET PATH OF CUSTOM IMAGE FOLDER
				string imgFolder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/admincss/Faculty");

				// CHECKING FOLDER EXIST OR NOT - IF NOT THEN CREATE F0LDER 
				if (!Directory.Exists(imgFolder))
				{
					Directory.CreateDirectory(imgFolder);
				}

				// MAKING CUSTOM AND COMBINE FOLDER PATH WITH IMAHE 
				string filepath = Path.Combine(imgFolder, FileName);

				// COPY IMAGE TO REAL PATH TO DEVELOPER PATH
				using (var stream = new FileStream(filepath, FileMode.Create))
				{
					img.CopyTo(stream);
				}

				// READY SEND PATH TO  IMAGE TO DB  
				var dbAddress = Path.Combine("admincss/Faculty", FileName);

				// EQUALIZE TABLE (MODEL) PROPERTY WITH CUSTOM PATH 
				updatedFaculty.FacultyTable.FacultyImg = dbAddress;
				//MYIMAGES/imagetodbContext.JGP
				existingFaculty.FacultyImg = updatedFaculty.FacultyTable.FacultyImg;
			}

			// Update other properties of the faculty
			existingFaculty.FacultyName = updatedFaculty.FacultyTable.FacultyName;
			existingFaculty.FacultyEmail = updatedFaculty.FacultyTable.FacultyEmail;
			existingFaculty.FacultyPassword = updatedFaculty.FacultyTable.FacultyPassword;
			existingFaculty.gender = updatedFaculty.FacultyTable.gender;
			existingFaculty.FacultyDepartment = updatedFaculty.FacultyTable.FacultyDepartment;

			// Save changes to the database
			db.SaveChanges();

			ViewBag.message = "Faculty updated successfully.";
			return RedirectToAction("Faculty", new { message = ViewBag.message });
		}


		public IActionResult DeleteFaculty(int id)
		{
			Faculty faculty = db.Faculties.Find(id);
			db.Faculties.Remove(faculty);
			db.SaveChanges();
			ViewBag.message = "Faculty Deleted Successfully";
			return RedirectToAction("Faculty", new { message = ViewBag.message });
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
        public IActionResult Courses(string message)
        {
			ViewBag.message = message;
			var course = db.Courses.Include(c => c.Faculty).Include(f=>f.Faculty.FacultyDepartmentNavigation).ToList();
            return View();
        }

        [HttpGet]
        public IActionResult AddCourse()
        {
			CourseFacultyView viewModel = new CourseFacultyView
			{
				CourseTable = new Course(),
				faculties = db.Faculties.ToList()
			};
			return View(viewModel);
        }


		[HttpPost]
		public IActionResult AddCourse(CourseFacultyView newCourse,IFormFile img)
		{
			//checking Image path was null or not ?
			
				var asd= img;
				if (img != null && img.Length > 0)
				{
					// GETTING IMAGE FILE EXTENSION 
					var fileExt = System.IO.Path.GetExtension(img.FileName).Substring(1);

					// GETTING IMAGE NAME
					var random = Path.GetFileName(img.FileName);

					// GUID ID COMBINE WITH IMAGE NAME - TO ESCAPE IMAGE NAME REDENDNCY 
					var FileName = Guid.NewGuid() + random;

					// GET PATH OF CUSTOM IMAGE FOLDER
					string imgFolder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/admincss/Course");

					// CHECKING FOLDER EXIST OR NOT - IF NOT THEN CREATE F0LDER 
					if (!Directory.Exists(imgFolder))
					{
						Directory.CreateDirectory(imgFolder);
					}

					// MAKING CUSTOM AND COMBINE FOLDER PATH WITH IMAHE 
					string filepath = Path.Combine(imgFolder, FileName);

					// COPY IMAGE TO REAL PATH TO DEVELOPER PATH
					using (var stream = new FileStream(filepath, FileMode.Create))
					{
						img.CopyTo(stream);
					}

					// READY SEND PATH TO  IMAGE TO DB  
					var dbAddress = Path.Combine("admincss/Course", FileName);

					// EQUALIZE TABLE (MODEL) PROPERTY WITH CUSTOM PATH 
					newCourse.CourseTable.CourseImg = dbAddress;
					//MYIMAGES/imagetodbContext.JGP

					Course course = new Course();
					course.CourseName = newCourse.CourseTable.CourseName;
					course.CourseDesc = newCourse.CourseTable.CourseDesc;
					course.CourseDuration = newCourse.CourseTable.CourseDuration;
					course.CourseImg = newCourse.CourseTable.CourseImg;

					course.CourseImg = dbAddress;


					// SEND TO TABLE 
					db.Courses.Add(course);
					db.SaveChanges();
				    ViewBag.message = "Course Added Successfully";
				return RedirectToAction("Courses", new { message = ViewBag.message });
				}else
			{
				ViewBag.error = "Something went wrong";
				return RedirectToAction("AddCourse");
			}
			return View();
		}

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
			var dep = db.Departments.Include(d => d.Faculties).ToList();
			return View(dep);
        }

        [HttpGet]
        public IActionResult AddDepartment()
        {
            return View();
        }
		[HttpPost]
		public IActionResult AddDepartment(Department dep)
		{
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
