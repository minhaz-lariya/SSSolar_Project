using Microsoft.AspNetCore.Mvc;

namespace SSSolar_Project.Controllers
{
	public class AdminController : Controller
	{
        [Route("Admin/Home")]
        public IActionResult Index()
		{
			return View();
		}

		[Route("Admin/Authentication")]
        public IActionResult Authentication()
        {
            return View();
        }

        [Route("Admin/Categories")]
        public IActionResult Categories()
        {
            return View();
        }

        [Route("Admin/Notification")]
        public IActionResult Notification()
        {
            return View();
        }

        [Route("Admin/Products")]
        public IActionResult Products()
        {
            return View();
        }

		[Route("Admin/Special-Packages")]
        public IActionResult SpecialPackages()
        {
            return View();
        }

		[Route("Admin/UploadSliders")]
		public IActionResult Sliders()
		{
			return View();
		}
	}
}
