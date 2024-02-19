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
		[HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //     public IActionResult Login(Student login)
        //     {

        //         var login_Email = login.StudentEmail;
        //         var login_pass = login.Password;



        //         var fetch = db.Students.Where(user => user.StudentEmail == login_Email).ToList();
        //         var afetch = db.Faculties.Where(user => user.FacultyEmail == login_Email).ToList();
        //         var sfetch = db.Admins.Where(user => user.AdminEmail == login_Email).ToList();

        //var dbEmail = fetch[0].StudentEmail;
        //         var dbPass = fetch[0].Password;
        //         var dbRole = fetch[0].Role;


        //         if (dbEmail == login_Email && dbPass == login_pass)
        //         {
        //	contx.HttpContext.Session.SetString("sessionEmail", dbEmail);
        //	contx.HttpContext.Session.SetString("sessionPassword", dbPass);
        //             contx.HttpContext.Session.SetString("sessionRole", Convert.ToString(dbRole));



        //	var Session_useremail = contx.HttpContext.Session.GetString("sessionEmail");
        //	var Session_userpass = contx.HttpContext.Session.GetString("sessionPassword");
        //	var Session_userrole = contx.HttpContext.Session.GetString("sessionRole");

        //             if(Session_userrole == "1")
        //             {
        //                 TempData["Role"] = Session_userrole;
        //                 return RedirectToAction("Index", "Student");
        //             }else if(Session_userrole == "2")
        //             {
        //		TempData["Role"] = Session_userrole;
        //		return RedirectToAction("Index", "Faculty");
        //	}else if(Session_userrole == "3")
        //             {
        //		TempData["Role"] = Session_userrole;
        //		return RedirectToAction("Index", "Admin");
        //	}
        //             else
        //             {
        //                 return RedirectToAction("Index", "Home");
        //             }
        //}

        //         return View();


        //     }
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
                //var dbRole = studentFetch[0].Role;

                if (dbEmail == login_Email && dbPass == login_pass)
                {

                    contx.HttpContext.Session.SetString("sessionEmail", dbEmail);
                    contx.HttpContext.Session.SetString("sessionPassword", dbPass);
                    //contx.HttpContext.Session.SetString("sessionRole", Convert.ToString(dbRole));

                    return RedirectToAction("Index", "Student");
                }
            }

            // Fetch Faculty
            var facultyFetch = db.Faculties.Where(user => user.FacultyEmail == login_Email).ToList();

            if (facultyFetch.Count > 0)
            {
                var dbEmail = facultyFetch[0].FacultyEmail;
                var dbPass = facultyFetch[0].FacultyPassword;
                //var dbRole = facultyFetch[0].Role;

                if (dbEmail == login_Email && dbPass == login_pass)
                {
                    contx.HttpContext.Session.SetString("sessionEmail", dbEmail);
                    contx.HttpContext.Session.SetString("sessionPassword", dbPass);
                    //contx.HttpContext.Session.SetString("sessionRole", Convert.ToString(dbRole));

                    return RedirectToAction("Index", "Faculty");
                }
            }

            // Fetch Admin
            var adminFetch = db.Admins.Where(user => user.AdminEmail == login_Email).ToList();

            if (adminFetch.Count > 0)
            {
                var dbEmail = adminFetch[0].AdminEmail;
                var dbPass = adminFetch[0].Password;
                //var dbRole = adminFetch[0].Role;

                if (dbEmail == login_Email && dbPass == login_pass)
                {
                    contx.HttpContext.Session.SetString("sessionEmail", dbEmail);
                    contx.HttpContext.Session.SetString("sessionPassword", dbPass);
                    //contx.HttpContext.Session.SetString("sessionRole", Convert.ToString(dbRole));

                    return RedirectToAction("Index", "Admin");
                }
            }

            return RedirectToAction("Index", "Home");
        }





        [HttpGet]
        public IActionResult Registration()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Registration(Student registration)
        {

            var newreg = registration;
            db.Students.Add(newreg);
            db.SaveChanges();
            return RedirectToAction("Registration");
        }














        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Courses()
        {
            return View();
        }
        public IActionResult CourseDetails()
        {
            return View();
        }
        public IActionResult Events()
        {
            return View();
        }
        public IActionResult EventDetails()
        {
            return View();
        }

        public IActionResult Gallery()
        {
            return View();
        }

        public IActionResult Teachers()
        {
            return View();
        }

        public IActionResult TeachersDetails()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }

     

        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult BlogDetails()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}