using ITM_College.Data;
using ITM_College.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITM_College.Controllers
{
	public class StudentController : Controller
	{
		private readonly ITM_CollegeContext db;
		public StudentController(ITM_CollegeContext db)
		{
			this.db = db;
		}
		private string GenerateTrackingId()
		{
			// Generate a GUID
			Guid guid = Guid.NewGuid();

			// Convert the first 8 characters of the GUID to an integer
			string guidString = guid.ToString("N"); // Remove hyphens
			int trackingIdInt = int.Parse(guidString.Substring(0, 5), System.Globalization.NumberStyles.HexNumber);

			// Convert the integer tracking ID to a string
			string trackingIdString = trackingIdInt.ToString();

			return trackingIdString;
		}
		public IActionResult Index()
		{
            string sessionId = HttpContext.Session.GetString("sessionId");
            var facid = Convert.ToInt32(sessionId);
			var student = db.Students.FirstOrDefault(col => col.StudentId == facid);
			return View(student);
		}

		// ------ Controller 1 Student registration Controller ------
		// i- Registration
		// ii- Update Form

		[HttpGet]
		public IActionResult RegistrationForm()
		{
			Registration viewModel = new Registration
			{
				StudentReg = new StudentCourseRegistration(),
				PreviousExams = new PreviousExam(),


			};
			return View(viewModel);
		}
		[HttpPost]
		public IActionResult RegistrationForm(Registration newReg)
		{

			// Save data to database
			var newstd = newReg.StudentReg;
			var newreg = newReg.PreviousExams;


            string sessionId = HttpContext.Session.GetString("sessionId");
            var facid = Convert.ToInt32(sessionId);
            newstd.Dob = Convert.ToDateTime("2002-3-9");
            newstd.Status = 1;
            newstd.StudentId = facid;

            db.StudentCourseRegistrations.Add(newstd);
			db.PreviousExams.Add(newreg);
			// Generate tracking ID
			string trackingId = GenerateTrackingId();

			// Store tracking ID along with registration details
			newReg.StudentReg.TrackingId = trackingId;
			// Store tracking ID in TempData
			TempData["TrackingId"] = trackingId;
			db.SaveChanges();
			ViewBag.message = "Registration Succesfull";
			return RedirectToAction("Track", new { message = ViewBag.message });


		}
		public IActionResult Track(string message, string error)
		{
			ViewBag.message = message;
			ViewBag.error = error;
			return View();
		}

        public IActionResult CheckStatus(string message, string error)
        {
            ViewBag.message = message;
            ViewBag.error = error;
            return View();
        }
    }
}
