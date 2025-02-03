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
    public class SpecialPackagesController : ControllerBase
    {
        private readonly ApplicationDBContext _DBContext;
        private readonly IWebHostEnvironment _Environment;

        public SpecialPackagesController(ApplicationDBContext DataContext, IWebHostEnvironment environment)
        {
            _DBContext = DataContext;
            _Environment = environment;
        }

        [HttpPost]
        [Route("AddPackages")]
        [RequestSizeLimit(1000 * 1024 * 1024)]       //unit is bytes => 500Mb
        [RequestFormLimits(MultipartBodyLengthLimit = 1000 * 1024 * 1024)]

        public async Task<IActionResult> AddPackages(SpecialPackages Model)
        {
            try
            {
                Guid guid = Guid.NewGuid();
                string productFileName = guid.ToString() + ".png";
                byte[] imageBytes = Convert.FromBase64String(Model.imgPath);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    Image image = Image.FromStream(ms);
                    var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\SpecialPackages", productFileName);
                    image.Save(rootPath); // Specify the path and format
                    Model.imgPath = productFileName;
                }

                _DBContext.SpecialPackages.Add(Model);
                await _DBContext.SaveChangesAsync();
                return Ok(new { Status = "OK", Result = "Package Successfully Saved" });
            }
            catch (Exception ex)
            {
                return Ok(new { Status = "Error", Result = "Error: " + ex.Message });
            }
        }


        [HttpPost]
        [Route("UpdatePackages")]
        [RequestSizeLimit(1000 * 1024 * 1024)] // 500 MB
        [RequestFormLimits(MultipartBodyLengthLimit = 1000 * 1024 * 1024)]
        public async Task<IActionResult> UpdatePackages(SpecialPackages Model)
        {
            try
            {
                var existingProduct = await _DBContext.SpecialPackages.FindAsync(Model.Id);
                if (existingProduct == null)
                {
                    return Ok(new { Status = "Error", Result = "Product not found" });
                }

                if (!string.IsNullOrEmpty(Model.imgPath))
                {
                    // New image provided
                    Guid guid = Guid.NewGuid();
                    string productFileName = guid.ToString() + ".png";
                    byte[] imageBytes = Convert.FromBase64String(Model.imgPath);
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        Image image = Image.FromStream(ms);
                        var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Products", productFileName);
                        image.Save(rootPath);
                        Model.imgPath = productFileName; // Update with new image name
                    }
                }
                else
                {
                    Model.imgPath = existingProduct.imgPath;
                }

                _DBContext.Entry(existingProduct).CurrentValues.SetValues(Model);
                await _DBContext.SaveChangesAsync();

                return Ok(new { Status = "OK", Result = "Package Successfully Updated" });
            }
            catch (Exception ex)
            {
                return Ok(new { Status = "Error", Result = "Error: " + ex.Message });
            }
        }


        [HttpDelete]
        [Route("deletePackages/{Id?}")]
        public async Task<IActionResult> removePackages(int? Id)
        {
            try
            {
                var Data = _DBContext.SpecialPackages.Find(Id);

                if (Data != null)
                {
                    _DBContext.SpecialPackages.Remove(Data);
                    await _DBContext.SaveChangesAsync();
                    return Ok(new { Status = "OK", Result = "Package Successfully Removed" });
                }
                else
                {
                    return Ok(new { Status = "Fail", Result = "Package Not Found" });
                }

            }
            catch (Exception ex)
            {
                return Ok(new { Status = "Error", Result = "Error: " + ex.Message });
            }
        }

        [HttpGet]
        [Route("AllPackages")]
        public async Task<IActionResult> getPackages()
        {
            try
            {
                var Data = await _DBContext.SpecialPackages.ToListAsync();
                return Ok(new { Status = "OK", Result = Data });
            }
            catch (Exception ex)
            {
                return Ok(new { Status = "Error", Result = "Error: " + ex.Message });
            }
        }


        [HttpGet]
        [Route("showPackages")]
        public async Task<IActionResult> showPackages()
        {
            try
            {
				DateTime currentDate = System.DateTime.Today; // Already without time component
				var Data = await _DBContext.SpecialPackages
					.Where(o => o.DateFrom.Value.Date <= currentDate.AddDays(-1).Date && o.DateTo.Value.Date >= currentDate.AddDays(1).Date)
					.ToListAsync();

				if (Data != null && Data.Any())
				{
					return Ok(new { Status = "OK", Result = Data });
				}
				else
				{
					return Ok(new { Status = "Fail", Result = "No Data Found" });
				}
			}
			catch (Exception ex)
            {
                return Ok(new { Status = "Fail", Result = "Error: " + ex.Message });
            }
        }

        [HttpGet]
        [Route("detailPackages/{Id?}")]
        public async Task<IActionResult> detailPackages(int Id)
        {
            try
            {
                var Data = await _DBContext.SpecialPackages.Where(o => o.Id == Id).FirstOrDefaultAsync();
                if (Data != null)
                {
                    return Ok(new { Status = "OK", Result = Data });
                }
                else
                {
                    return Ok(new { Status = "OK", Result = "No Data Found" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { Status = "Error", Result = "Error: " + ex.Message });
            }
        }

    }
}
