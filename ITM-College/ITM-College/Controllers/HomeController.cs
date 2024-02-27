using ITM_College.Data;
using ITM_College.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ITM_College.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ITM_CollegeContext db;
		private readonly IHttpContextAccessor contx;
		public HomeController(ILogger<HomeController> logger, IHttpContextAccessor contx, ITM_CollegeContext db)
		{
			_logger = logger;
			this.contx = contx;
			this.db = db;

		}
		//===============================================================Login and registration Start================================================================
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Login(Student login)
		{
			var login_Email = login.StudentEmail;
			var login_pass = login.Password;


			var studentFetch = db.Students.Where(user => user.StudentEmail == login_Email).ToList();

			if (studentFetch.Count > 0)
			{
				var dbEmail = studentFetch[0].StudentEmail;
				var dbPass = studentFetch[0].Password;
				var dbRole = studentFetch[0].Role;
                var dbid = studentFetch[0].StudentId;

                if (dbEmail == login_Email && dbPass == login_pass)
				{

					contx.HttpContext.Session.SetString("sessionEmail", dbEmail);
					contx.HttpContext.Session.SetString("sessionPassword", dbPass);
					contx.HttpContext.Session.SetString("sessionRole", Convert.ToString(dbRole));
                    contx.HttpContext.Session.SetString("sessionId", Convert.ToString(dbid));
                    return RedirectToAction("Index", "Student");
				}
			}

			// Fetch Faculty
			var facultyFetch = db.Faculties.Where(user => user.FacultyEmail == login_Email).ToList();

			if (facultyFetch.Count > 0)
			{
				var dbEmail = facultyFetch[0].FacultyEmail;
				var dbPass = facultyFetch[0].FacultyPassword;
				var dbRole = facultyFetch[0].Role;
				var dbid = facultyFetch[0].FacultyId;

				if (dbEmail == login_Email && dbPass == login_pass)
				{
					contx.HttpContext.Session.SetString("sessionEmail", dbEmail);
					contx.HttpContext.Session.SetString("sessionPassword", dbPass);
					contx.HttpContext.Session.SetString("sessionRole", Convert.ToString(dbRole));
					contx.HttpContext.Session.SetString("sessionId", Convert.ToString(dbid));

                    return RedirectToAction("Index", "Faculty");

				}
			}

			// Fetch Admin
			var adminFetch = db.Admins.Where(user => user.AdminEmail == login_Email).ToList();

			if (adminFetch.Count > 0)
			{
				var dbEmail = adminFetch[0].AdminEmail;
				var dbPass = adminFetch[0].Password;
				var dbRole = adminFetch[0].Role;

				if (dbEmail == login_Email && dbPass == login_pass)
				{
					contx.HttpContext.Session.SetString("sessionEmail", dbEmail);
					contx.HttpContext.Session.SetString("sessionPassword", dbPass);
					contx.HttpContext.Session.SetString("sessionRole", Convert.ToString(dbRole));
					return RedirectToAction("Index", "Admin");
				}

			}
		
			ViewBag.error= "Invalid login credentials. Please ensure your email and password are correct.";
            return View();

		}
		[HttpGet]
		public IActionResult Registration()
		{

			return View();
		}
		[HttpPost]
		public IActionResult Registration(Student registration, IFormFile img)
		{
			//checking Image path was null or not ?


			if (img != null && img.Length > 0)
			{
				// GETTING IMAGE FILE EXTENSION 
				var fileExt = System.IO.Path.GetExtension(img.FileName).Substring(1);

				// GETTING IMAGE NAME
				var random = Path.GetFileName(img.FileName);

				// GUID ID COMBINE WITH IMAGE NAME - TO ESCAPE IMAGE NAME REDENDNCY 
				var FileName = Guid.NewGuid() + random;

				// GET PATH OF CUSTOM IMAGE FOLDER
				string imgFolder = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/admincss/students");

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
				var dbAddress = Path.Combine("admincss/students", FileName);

				// EQUALIZE TABLE (MODEL) PROPERTY WITH CUSTOM PATH 
				registration.StdImg = dbAddress;
				//MYIMAGES/imagetodbContext.JGP

				// SEND TO TABLE 
				var newreg = registration;
				db.Students.Add(newreg);
				db.SaveChanges();

				return RedirectToAction("Index");
			}
			return View();
		}
		public IActionResult Logout()
		{

			HttpContext.Session.Remove("sessionEmail");
			HttpContext.Session.Remove("sessionPassword");
			HttpContext.Session.Remove("sessionRole");


			return RedirectToAction("Login", "Home");
		}
		//====================================================================Login and registration End=====================================================================

		public IActionResult Index()
		{

			VisitorIndex model = new VisitorIndex()
			{
				courses = db.Courses.ToList(),
				faculties = db.Faculties.ToList(),
				departments = db.Departments.ToList(),
				Facilities = db.Facilities.ToList()
			};
			return View(model);
		}
		public IActionResult About()
		{
			return View();
		}
		public IActionResult Courses()
		{
			VisitorIndex model = new VisitorIndex()
			{
				courses = db.Courses.ToList(),
				faculties = db.Faculties.ToList(),
				departments = db.Departments.ToList(),
				Facilities = db.Facilities.ToList()
			};
			return View(model);
		}

		public IActionResult Contact(Contact contacts)
		{
			var msg = contacts;
			db.Contacts.Add(msg);
			db.SaveChanges();
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}