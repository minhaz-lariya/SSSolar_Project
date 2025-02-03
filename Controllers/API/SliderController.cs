using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSSolar_Project.ApplicationContext;
using SSSolar_Project.Models;
using System.Drawing;

namespace SSSolar_Project.Controllers.API
{
	[Route("api")]
	[ApiController]
	public class SliderController : ControllerBase
	{
		private readonly ApplicationDBContext _DBContext;
		private readonly IWebHostEnvironment _Environment;

		public SliderController(ApplicationDBContext DataContext, IWebHostEnvironment environment)
		{
			_DBContext = DataContext;
			_Environment = environment;
		}

		[HttpPost]
		[Route("uploadSliders")]
		[RequestSizeLimit(1000 * 1024 * 1024)]       //unit is bytes => 500Mb
		[RequestFormLimits(MultipartBodyLengthLimit = 1000 * 1024 * 1024)]

		public async Task<IActionResult> uploadSliders(Sliders Model)
		{
			try
			{
				Guid guid = Guid.NewGuid();
				string productFileName = guid.ToString() + ".png";
				byte[] imageBytes = Convert.FromBase64String(Model.imgPath);
				using (MemoryStream ms = new MemoryStream(imageBytes))
				{
					Image image = Image.FromStream(ms);
					var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Sliders", productFileName);
					image.Save(rootPath); // Specify the path and format
					Model.imgPath = productFileName;
				}

				_DBContext.Sliders.Add(Model);
				await _DBContext.SaveChangesAsync();
				return Ok(new { Status = "OK", Result = "Slider Upload Successfully Saved" });
			}
			catch (Exception ex)
			{
				return Ok(new { Status = "Error", Result = "Error: " + ex.Message });
			}
		}

		[HttpDelete]
		[Route("deleteSlider/{Id?}")]
		public async Task<IActionResult> removeSlider(int? Id)
		{
			try
			{
				var Data = _DBContext.Sliders.Find(Id);

				if (Data != null)
				{
					_DBContext.Sliders.Remove(Data);
					await _DBContext.SaveChangesAsync();
					return Ok(new { Status = "OK", Result = "Slider Successfully Removed" });
				}
				else
				{
					return Ok(new { Status = "Fail", Result = "Slider Not Found" });
				}

			}
			catch (Exception ex)
			{
				return Ok(new { Status = "Error", Result = "Error: " + ex.Message });
			}
		}

        [HttpGet]
        [Route("showSliders")]
        public async Task<IActionResult> showSlider()
        {
            try
            {
				var Data = await _DBContext.Sliders.ToListAsync();
                return Ok(new { Status = "OK", Result = Data });
            }
            catch (Exception ex)
            {
                return Ok(new { Status = "Error", Result = "Error: " + ex.Message });
            }
        }

        [HttpGet]
        [Route("dashboaard")]
        public async Task<IActionResult> dashboaard()
        {
            try
            {
                var categories = new List<string> { "INVERTERS", "SOLAR PANEL", "LITHIUM BATTERY" };
				var Data = await _DBContext.Sliders.Select(o => new
				{
					Products = _DBContext.Products.Where(o => categories.Any(c => o.Category.Contains(c))).Count(),
					Packages = _DBContext.SpecialPackages.Count(),
					Accessories = _DBContext.Products.Where(o => !categories.Any(c => o.Category.Contains(c))).Count(),
					Notification = _DBContext.Notification.Count()
				}).FirstOrDefaultAsync();
                return Ok(new { Status = "OK", Result = Data });
            }
            catch (Exception ex)
            {
                return Ok(new { Status = "Error", Result = "Error: " + ex.Message });
            }
        }

    }
}
