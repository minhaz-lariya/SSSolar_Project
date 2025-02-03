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
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDBContext _DBContext;
        private readonly IWebHostEnvironment _Environment;

        public ProductController(ApplicationDBContext DataContext, IWebHostEnvironment environment)
        {
            _DBContext = DataContext;
            _Environment = environment;
        }

        [HttpPost]
        [Route("AddProducts")]
        [RequestSizeLimit(1000 * 1024 * 1024)]       //unit is bytes => 500Mb
        [RequestFormLimits(MultipartBodyLengthLimit = 1000 * 1024 * 1024)]

        public async Task<IActionResult> AddProducts(Products Model)
        {
            try
            {
                Guid guid = Guid.NewGuid();
                string productFileName = guid.ToString() + ".png";
                byte[] imageBytes = Convert.FromBase64String(Model.ProductImage);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    Image image = Image.FromStream(ms);
                    var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Products", productFileName);
                    image.Save(rootPath); // Specify the path and format
                    Model.ProductImage = productFileName;
                }

                _DBContext.Products.Add(Model);

                await _DBContext.SaveChangesAsync();
                return Ok(new { Status = "OK", Result = "Product Successfully Saved" });
            }
            catch (Exception ex) {
                return Ok(new { Status = "Error", Result = "Error: " + ex.Message });
            }
        }

        [HttpPost]
        [Route("UpdateProducts")]
        [RequestSizeLimit(1000 * 1024 * 1024)] // 500 MB
        [RequestFormLimits(MultipartBodyLengthLimit = 1000 * 1024 * 1024)]
        public async Task<IActionResult> editProducts(Products Model)
        {
            try
            {
                var existingProduct = await _DBContext.Products.FindAsync(Model.Id);
                if (existingProduct == null)
                {
                    return Ok(new { Status = "Error", Result = "Product not found" });
                }

                if (!string.IsNullOrEmpty(Model.ProductImage))
                {
                    // New image provided
                    Guid guid = Guid.NewGuid();
                    string productFileName = guid.ToString() + ".png";
                    byte[] imageBytes = Convert.FromBase64String(Model.ProductImage);
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        Image image = Image.FromStream(ms);
                        var rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Products", productFileName);
                        image.Save(rootPath);
                        Model.ProductImage = productFileName; // Update with new image name
                    }
                }
                else
                {
                    Model.ProductImage = existingProduct.ProductImage;
                }

                existingProduct.Name = Model.Name;
                existingProduct.Description = Model.Description;
                existingProduct.Category = Model.Category;
                existingProduct.BriefDescription = Model.BriefDescription;
                existingProduct.SEOKeywords = Model.SEOKeywords;
                existingProduct.Brand = Model.Brand;
                existingProduct.Price = Model.Price;
                existingProduct.isShowPrice = Model.isShowPrice;
                existingProduct.isSpecialProduct = Model.isSpecialProduct;
                _DBContext.Entry(existingProduct).CurrentValues.SetValues(Model);
                await _DBContext.SaveChangesAsync();

                return Ok(new { Status = "OK", Result = "Product Successfully Updated" });
            }
            catch (Exception ex)
            {
                return Ok(new { Status = "Error", Result = "Error: " + ex.Message });
            }
        }


        [HttpDelete]
        [Route("deleteProducts/{Id?}")]
        public async Task<IActionResult> removeProducts(int? Id)
        {
            try
            {
                var Data = _DBContext.Products.Find(Id);

                if (Data  != null)
                {
                    _DBContext.Products.Remove(Data);
                    await _DBContext.SaveChangesAsync();
                    return Ok(new { Status = "OK", Result = "Product Successfully Removed" });
                }
                else
                {
                    return Ok(new { Status = "Fail", Result = "Product Not Found" });
                }
                
            }
            catch (Exception ex)
            {
                return Ok(new { Status = "Error", Result = "Error: " + ex.Message });
            }
        }


        [HttpGet]
        [Route("AllProducts")]
        public async Task<IActionResult> getProducts()
        {
            try
            {
                var Data = await _DBContext.Products.ToListAsync();
                return Ok(new { Status = "OK", Result = Data });
            }
            catch (Exception ex)
            {
                return Ok(new { Status = "Error", Result = "Error: " + ex.Message });
            }
        }

		[HttpGet]
		[Route("filterProduct/{Name?}")]
		public async Task<IActionResult> filterProduct(string? Name)
		{
			try
			{
				var Data = await _DBContext.Products
					                       .Where(o => Name == null || o.Name.Contains(Name.ToUpper()))
					                       .ToListAsync();
				return Ok(new { Status = "OK", Result = Data });
			}
			catch (Exception ex)
			{
				return Ok(new { Status = "Error", Result = "Error: " + ex.Message });
			}
		}

		[HttpGet]
		[Route("SearchProduct/{Name?}")]
		public async Task<IActionResult> SearchProduct(string? Name)
		{
			try
			{
				if (string.IsNullOrEmpty(Name))
				{
					return Ok(new { Status = "Fail", Result = "Name is required" });
				}

				// Exact match for case-insensitive comparison using String.Equals
				var Data = await _DBContext.Products
					.Where(o => string.Equals(o.Name.Replace("-", " "), Name.ToUpper()))  // Case-insensitive exact match
					.FirstOrDefaultAsync();

				if (Data != null)
				{
					return Ok(new { Status = "OK", Result = Data });
				}
				else
				{
					return Ok(new { Status = "Fail", Result = "No products found" });
				}
			}
			catch (Exception ex)
			{
				return Ok(new { Status = "Error", Result = "Error: " + ex.Message });
			}
		}



		[HttpGet]
        [Route("AllProductsAccessories")]
        public async Task<IActionResult> getAccessories()
        {
            try
            {
                var categories = new List<string> { "INVERTERS", "SOLAR PANEL", "LITHIUM BATTERY" };

                var data = await _DBContext.Products.Where(o => !categories.Any(c => o.Category.Contains(c))).ToListAsync();

                return Ok(new { Status = "OK", Result = data });
            }
            catch (Exception ex)
            {
                return Ok(new { Status = "Fail", Result = "Error: " + ex.Message });
            }
        }

        [HttpGet]
        [Route("categoriesProducts/{Category}/{Brand}")]
        public async Task<IActionResult> categoriesProducts(string Category, string Brand)
        {
            try
            {
                var Data = await _DBContext.Products.Where(o => o.Category == Category && o.Brand == Brand).ToListAsync();
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

        [HttpGet]
        [Route("detailProducts/{Id?}")]
        public async Task<IActionResult> detailProducts(int Id)
        {
            try
            {
                var Data = await _DBContext.Products.Where(o => o.Id == Id).FirstOrDefaultAsync();
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
