using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SSSolar_Project.ApplicationContext;
using SSSolar_Project.Models;
using System.Data;
using System.Diagnostics;

namespace SSSolar_Project.Controllers
{
	public class HomeController : Controller
	{
		private readonly ApplicationDBContext _DBContext;

		public HomeController(ApplicationDBContext DataContext)
		{
			_DBContext = DataContext;
		}


		public IActionResult Index()
        {
            return View();
        }

		[Route("AboutUs")]
		public IActionResult AboutUs()
		{
			return View();
		}

		[Route("ContactUs")]
		public IActionResult ContactUs()
		{
			return View();
		}

		[Route("Repair")]
		public IActionResult Repair()
		{
			return View();
		}

		[Route("Products/{Category}/{Brand}")]
		public IActionResult Products(string Category, string Brand)
		{
			ViewBag.Category = Category.Replace("-", " ");
			ViewBag.Brand = Brand.Replace("-", " ");
			return View();
		}

		[Route("Products/Details/{Id?}/{Name?}")]
		public IActionResult ProductDetails(int? Id, string Name)
		{
			ViewBag.Name = Name;
			ViewBag.Id = Id;
			var Data = _DBContext.Products.Where(o => o.Id == Id).FirstOrDefault();
			ViewBag.ProductData = Data;
			return View();
		}

		[Route("Special-Package")]
		public IActionResult SpecialPackage()
		{
			return View();
		}

        [Route("Accessories")]
        public IActionResult Accessories()
        {
            return View();
        }

        [Route("Terms-&-Conditon")]
		public IActionResult TermsConditon()
		{
			return View();
		}

		[Route("Search/{Name}")]
		public IActionResult Search(string Name)
		{
			if (string.IsNullOrEmpty(Name))  // Check if the Name parameter is empty
			{
				return RedirectToAction("Index", "Home");  // Redirect to the Home page
			}

			ViewBag.Name = Name.Replace("-", " ");
			return View();
		}

	}
}
