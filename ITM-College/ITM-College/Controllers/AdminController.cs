using ITM_College.Data;
using ITM_College.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace ITM_College.Controllers
{
	public class AdminController : Controller
	{

		private readonly ITM_CollegeContext db;
		public AdminController(ITM_CollegeContext db)
		{
			this.db = db;
		}
		[HttpGet]
		public IActionResult Index(string message, string error)
		{
			var dashboardData = new DashboardData
			{
				DepartmentsCount = db.Departments.Count(),
				FacultiesCount = db.Faculties.Count(),
				StudentsCount = db.Students.Count(),
				CoursesCount = db.Courses.Count(),
				Departments = db.Departments.ToList(),
				Faculties = db.Faculties.ToList(),
				Students = db.Students.ToList(),
				Courses = db.Courses.ToList(),
				newStd = db.StudentCourseRegistrations.Include(s => s.AddmissionForNavigation)
				.Include(c => c.Student).Where(col => col.Status == 1).ToList()
			};

			ViewBag.message = message;
			ViewBag.error = error;
			return View(dashboardData);
		}

		// ------ Controller 1 Faculty Controller ------
		// i- All Faculties
		// ii- Add Faculties
		// iii- Update Faculty
		// iv- Delete Faculty
		public IActionResult Faculty(string message, string error)
		{
			var faculties = db.Faculties.Include(f => f.FacultyDepartmentNavigation);
			ViewBag.message = message;
			ViewBag.error = error;
			return View(faculties);
		}


		public IActionResult FacultyDetail(int id)
		{
			var facultyWithCourses = db.Faculties
			.Include(f => f.FacultyDepartmentNavigation)
			.Include(f => f.Courses) // Include the Courses navigation property
			.FirstOrDefault(f => f.FacultyId == id);


			var count = db.Courses
			.Include(c => c.StudentCourseRegistrations)
		   .Where(col => col.FacultyId == id)
		   .SelectMany(c => c.StudentCourseRegistrations)
		   .Count();

			ViewBag.count = count;
			return View(facultyWithCourses);
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
		public IActionResult AddFaculty(FacultyAndDepartment newFaculty, IFormFile img)
		{
			if (db.Departments == null || !db.Departments.Any())
			{
				ViewBag.error = "Please add at least one department to add a Faculty.";
				return RedirectToAction("Faculty", new { error = ViewBag.error });
			}

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
				faculty.Gender = newFaculty.FacultyTable.Gender;
				faculty.FacultyDepartment = newFaculty.FacultyTable.FacultyDepartment;
				faculty.FacultyImg = dbAddress;
				faculty.Role = newFaculty.FacultyTable.Role;


				// SEND TO TABLE 
				db.Faculties.Add(faculty);
				db.SaveChanges();
				ViewBag.message = "Faculty Add Successfully";
				return RedirectToAction("Faculty", new { message = ViewBag.message });
			}


			return View();
		}

		[HttpGet]
		public IActionResult UpdateFaculty(int id)
		{
			Faculty faculty = db.Faculties.FirstOrDefault(cols => cols.FacultyId == id);
			var facs = new FacultyAndDepartment
			{
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
			existingFaculty.Gender = updatedFaculty.FacultyTable.Gender;
			existingFaculty.FacultyDepartment = updatedFaculty.FacultyTable.FacultyDepartment;
			existingFaculty.Role = updatedFaculty.FacultyTable.Role;
		

			// Save changes to the database
			db.SaveChanges();

			ViewBag.message = "Faculty updated successfully.";
			return RedirectToAction("Faculty", new { message = ViewBag.message });
		}



		public IActionResult DeleteFaculty(int id)
		{
			var faculty = db.Faculties.Include(f => f.Courses).FirstOrDefault(f => f.FacultyId == id);

			if (faculty == null)
			{
				// Faculty not found, handle error
				ViewBag.error = "Faculty not found.";
				return RedirectToAction("Faculty", new { error = ViewBag.error });
			}

			if (faculty.Courses.Any())
			{
				// Faculty cannot be deleted because it has courses associated with it
				ViewBag.error = "Cannot delete faculty. It has associated courses.";
				return RedirectToAction("Faculty", new { error = ViewBag.error });
			}

			// If the faculty has no associated courses, delete it
			db.Faculties.Remove(faculty);
			db.SaveChanges();

			ViewBag.message = "Faculty deleted successfully.";
			return RedirectToAction("Faculty", new { message = ViewBag.message });
		}

		// Faculty Controller End


		// ------ Controller 2 Student Controller ------
		// i- All Students
		// ii- Add Students
		// iii- Update Student
		// iv- Delete Student
		public IActionResult Students(string message, string error)
		{
			ViewBag.message = message;
			ViewBag.error = error;
			var students = db.Students
	.Include(s => s.StudentCourseRegistrations.Where(sr => sr.Status == 2)) // Filter registrations with status 2
	.ThenInclude(sr => sr.AddmissionForNavigation)
	.ToList();

			return View(students);
		}

		public IActionResult StudentDetail(int id)
		{

			// Check if the student ID exists in the StudentCourseRegistration table
			bool isStudentRegistered = db.StudentCourseRegistrations.Any(s => s.StudentId == id);

			if (isStudentRegistered)
			{
				return RedirectToAction("EnrolledStudent", new { id = id });
			}
			else
			{
				
				return RedirectToAction("NotEnrolledStudent", new { id = id });
			}


		}


		public IActionResult EnrolledStudent(int id)
		{
			var student = db.Students.Include(s => s.StudentCourseRegistrations)
									.ThenInclude(scr => scr.AddmissionForNavigation)
									.ThenInclude(c => c.Faculty)
									.ThenInclude(f => f.FacultyDepartmentNavigation)
									.FirstOrDefault(col=>col.StudentId == id);
			return View(student);
		}

		public IActionResult NotEnrolledStudent(int id)
		{
			var student = db.Students.FirstOrDefault(s => s.StudentId == id);


			return View(student);
		}

		public IActionResult StudentRequest()
		{
			var newStd = db.StudentCourseRegistrations.Include(s => s.AddmissionForNavigation)
			 .Include(c => c.Student).Where(col => col.Status == 1).ToList();

            return View(newStd);
		}

        // Students Controller End


        // ------ Controller 3 Courses Controller ------
        // i- All Students
        // ii- Add Students
        // iii- Update Student
        // iv- Delete Student
        public IActionResult Courses(string message,string error)
		{
			ViewBag.message = message;
			ViewBag.error = error;
            var course = db.Courses.Include(c => c.Faculty).Include(f => f.Faculty.FacultyDepartmentNavigation).ToList();
			return View(course);
		}

		public IActionResult CourseDetail(int id)
		{
			var course = db.Courses.Include(c => c.Faculty)
				.Include(f => f.Faculty.FacultyDepartmentNavigation)
				.Include(s => s.StudentCourseRegistrations)
				.FirstOrDefault(col => col.CourseId == id);
			return View(course);
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
		public IActionResult AddCourse(CourseFacultyView newCourse, IFormFile img)
		{
			//checking Image path was null or not ?
			if (db.Faculties == null || !db.Faculties.Any())
			{
				ViewBag.error = "Please add at least one faculty to add a course.";
				return RedirectToAction("Courses", new { error = ViewBag.error });
			}
			else { 
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
				course.FacultyId = newCourse.CourseTable.FacultyId;

				course.CourseImg = dbAddress;


					
						// If the model is valid
						db.Courses.Add(course); // Add the course object to the database context
						db.SaveChanges(); // Save changes to the database
						ViewBag.message = "Course Added Successfully"; // Set a message to be displayed
						return RedirectToAction("Courses", new { message = ViewBag.message }); // Redirect to the "Courses" action
				
				}
			else
			{
				ViewBag.error = "Something went wrong";
				return RedirectToAction("AddCourse");
			}
		}
			
		}

		[HttpGet]
		public IActionResult UpdateCourse(int id)
		{
			CourseFacultyView viewModel = new CourseFacultyView
			{
				CourseTable = db.Courses.FirstOrDefault(col => col.CourseId == id),
				faculties = db.Faculties.ToList()
			};
			return View(viewModel);
		}

		[HttpPost]
		public IActionResult UpdateCourse(CourseFacultyView updatedCourse, IFormFile img)
		{

			if (img != null && img.Length > 0)
			{
				var fileExt = Path.GetExtension(img.FileName).Substring(1);
				var random = Path.GetFileName(img.FileName);
				var fileName = Guid.NewGuid() + random;
				string imgFolder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/admincss/Course");

				if (!Directory.Exists(imgFolder))
				{
					Directory.CreateDirectory(imgFolder);
				}

				string filepath = Path.Combine(imgFolder, fileName);

				using (var stream = new FileStream(filepath, FileMode.Create))
				{
					img.CopyTo(stream);
				}

				var dbAddress = Path.Combine("admincss/Course", fileName);
				updatedCourse.CourseTable.CourseImg = dbAddress;
			}

			var courseToUpdate = db.Courses.FirstOrDefault(c => c.CourseId == updatedCourse.CourseTable.CourseId);

			if (courseToUpdate != null)
			{
				courseToUpdate.CourseName = updatedCourse.CourseTable.CourseName;
				courseToUpdate.CourseDesc = updatedCourse.CourseTable.CourseDesc;
				courseToUpdate.CourseDuration = updatedCourse.CourseTable.CourseDuration;
				courseToUpdate.FacultyId = updatedCourse.CourseTable.FacultyId;

				if (img != null && img.Length > 0)
				{
					courseToUpdate.CourseImg = updatedCourse.CourseTable.CourseImg;
				}

				db.SaveChanges();
				ViewBag.message = "Course Updated Successfully";
				return RedirectToAction("Courses", new { message = ViewBag.message });
			}
			else
			{
				ViewBag.error = "Course not found";
				return RedirectToAction("UpdateCourse", new { id = updatedCourse.CourseTable.CourseId });
			}

		}


		public IActionResult DeleteCourse(int id)
		{
			Course course = db.Courses.Find(id);
			db.Courses.Remove(course);
			db.SaveChanges();
			ViewBag.message = "Course Deleted Successfully";
			return RedirectToAction("Courses", new { message = ViewBag.message });
		}
		// Courses Controller End



		// ------ Controller 4 Department Controller ------
		// i- All Departments
		// ii- Add Department
		// iii- Update Department
		// iv- Delete Department
		public IActionResult Departments(string message, string error)
		{
			ViewBag.message = message;
			ViewBag.error = error;
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
			Department department = db.Departments.FirstOrDefault(cols => cols.DepartmentId == id);
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
			var department = db.Departments.Include(d => d.Faculties).FirstOrDefault(d => d.DepartmentId == id);

			if (department == null)
			{
				// Department not found, handle error
				ViewBag.error = "Department not found.";
				return RedirectToAction("Departments", new { error = ViewBag.error });
			}

			if (department.Faculties.Count > 0)
			{
				// Department cannot be deleted because it has faculties associated with it
				ViewBag.error = "Cannot delete department. It has associated faculties.";
				return RedirectToAction("Departments", new { error = ViewBag.error });
			}

			// If the department has no associated faculties, delete it
			db.Departments.Remove(department);
			db.SaveChanges();

			ViewBag.message = "Department deleted successfully.";
			return RedirectToAction("Departments", new { message = ViewBag.message });

		}
		// Department Controller End



		// ------ Controller 5 Facilities Controller ------
		// i- All Facilities
		// ii- Add Facilities
		// iii- Update Facilities
		// iv- Delete Facilities
		public IActionResult Facilities(string message)
		{
			ViewBag.message = message;
			var facility = db.Facilities.ToList();
			return View(facility);
		}

		[HttpGet]
		public IActionResult AddFacility()
		{
			return View();
		}


		[HttpPost]
		public IActionResult AddFacility(Facility newFacility, IFormFile img)
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
					string imgFolder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/admincss/Facility");

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
					var dbAddress = Path.Combine("admincss/Facility", FileName);


					//MYIMAGES/imagetodbContext.JGP

					Facility facility = new Facility();
					facility.FacilityName = newFacility.FacilityName;
					facility.FacilityDesc = newFacility.FacilityDesc;
					facility.FacilityImg = dbAddress;


					if (ModelState.IsValid)
					{
						// SEND TO TABLE 
						db.Facilities.Add(facility);
						db.SaveChanges();
						ViewBag.message = "Facility Add Successfully";
						return RedirectToAction("Facilities", new { message = ViewBag.message });
					}
					else
					{
						return View();
					}
				}
			}
			else
			{
				ViewBag.error = "Something went wrong";
				return RedirectToAction("AddFacility");
			}

			return View();
		}

		[HttpGet]
		public IActionResult UpdateFacility(int id)
		{
			var facility = db.Facilities.FirstOrDefault(cols => cols.Id == id);
			return View(facility);
		}

		[HttpPost]
		public IActionResult UpdateFacility(Facility updatedFacility, IFormFile img)
		{

			if (img != null && img.Length > 0)
			{
				var fileExt = Path.GetExtension(img.FileName).Substring(1);
				var random = Path.GetFileName(img.FileName);
				var fileName = Guid.NewGuid() + random;
				string imgFolder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/admincss/Facility");

				if (!Directory.Exists(imgFolder))
				{
					Directory.CreateDirectory(imgFolder);
				}

				string filepath = Path.Combine(imgFolder, fileName);

				using (var stream = new FileStream(filepath, FileMode.Create))
				{
					img.CopyTo(stream);
				}

				var dbAddress = Path.Combine("admincss/Facility", fileName);
				updatedFacility.FacilityImg = dbAddress;
			}

			var facilityToUpdate = db.Facilities.FirstOrDefault(f => f.Id == updatedFacility.Id);

			if (facilityToUpdate != null)
			{
				facilityToUpdate.FacilityName = updatedFacility.FacilityName;
				facilityToUpdate.FacilityDesc = updatedFacility.FacilityDesc;

				if (img != null && img.Length > 0)
				{
					facilityToUpdate.FacilityImg = updatedFacility.FacilityImg;
				}

				db.SaveChanges();
				ViewBag.message = "Facility Updated Successfully";
				return RedirectToAction("Facilities", new { message = ViewBag.message });
			}
			else
			{
				ViewBag.error = "Facility not found";
				return RedirectToAction("UpdateFacility", new { id = updatedFacility.Id });
			}

		}


		public IActionResult DeleteFacility(int id)
		{

			Facility facility = db.Facilities.Find(id);
			db.Facilities.Remove(facility);
			db.SaveChanges();
			ViewBag.message = "Facility Deleted Successfully";
			return RedirectToAction("Facilities", new { message = ViewBag.message });
		}
		// Facilities Controller End


		public IActionResult ApproveStudent(int id)
		{
			var std = db.StudentCourseRegistrations.FirstOrDefault(cols => cols.Id == id);
			std.Status = 2;
			db.SaveChanges();
			ViewBag.message = "Approve Successfully";
			return RedirectToAction("Students", new { message = ViewBag.message });
		}

		public IActionResult RejectStudent(int id)
		{
			var std = db.StudentCourseRegistrations.FirstOrDefault(cols => cols.Id == id);
			std.Status = 3;
			db.SaveChanges();
			ViewBag.message = "Reject Student Request";
			return RedirectToAction("Students", new { message = ViewBag.message });
		}

		public IActionResult Feedback(string message)
		{
			var contact = db.Contacts.ToList();
			ViewBag.message = message;
			return View(contact);
		}
		public IActionResult DeleteFeedback(int id)
		{

			Contact feedback = db.Contacts.Find(id);
			db.Contacts.Remove(feedback);
			db.SaveChanges();
			ViewBag.message = "Feedback Deleted Successfully";
			return RedirectToAction("Feedback", new { message = ViewBag.message });
		}

	}
}
