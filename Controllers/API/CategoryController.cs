using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSSolar_Project.ApplicationContext;
using SSSolar_Project.Models;

namespace SSSolar_Project.Controllers.API
{
    [Route("api")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDBContext _DBContext;
        public CategoryController(ApplicationDBContext DataContext)
        {
            _DBContext = DataContext;
        }

        [HttpPost]
        [Route("AddCategory")]
        public async Task<IActionResult> AddCategory(Categories Model)
        {
            try
            {
                var Data = await _DBContext.Categories.Where(o => o.Name == Model.Name).FirstOrDefaultAsync();
                if (Data == null)
                {
                    _DBContext.Categories.Add(Model);
                    await _DBContext.SaveChangesAsync();
                    return Ok(new { Status = "OK", Result = "Successfully Saved" });
                }
                else
                {
                    return Ok(new { Status = "Fail", Result = "Already Exists" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { Status = "Error", Result = "Error: " + ex.Message });
            }
        }

        [HttpPost]
        [Route("EditCategory")]
        public async Task<IActionResult> EditCategory(Categories Model)
        {
            try
            {
                var Data = await _DBContext.Categories.Where(o => o.Name == Model.Name && o.Id != Model.Id).FirstOrDefaultAsync();
                if (Data == null)
                {
                    _DBContext.Categories.Update(Model);
                    await _DBContext.SaveChangesAsync();
                    return Ok(new { Status = "OK", Result = "Successfully Saved" });
                }
                else
                {
                    return Ok(new { Status = "Fail", Result = "Already Exists" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { Status = "Fail", Result = "Error: " + ex.Message });
            }
        }

        [HttpGet]
        [Route("Categories")]
        public async Task<IActionResult> getCategories()
        {
            try
            {
                var Data = await _DBContext.Categories.ToListAsync();
                return Ok(new { Status = "OK", Result = Data });
            }
            catch (Exception ex)
            {
                return Ok(new { Status = "Fail", Result = "Error: " + ex.Message });
            }
        }

        [HttpGet]
        [Route("detailCategory/{Id?}")]
        public async Task<IActionResult> detailCategory(int Id)
        {
            try
            {
                var Data = await _DBContext.Categories.Where(o => o.Id == Id).FirstOrDefaultAsync();
                if (Data != null)
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

        [HttpDelete]
        [Route("deleteCategory/{Id?}")]
        public async Task<IActionResult> removeCategory(int? Id)
        {
            try
            {
                var Data = _DBContext.Categories.Find(Id);

                if (Data != null)
                {
                    _DBContext.Categories.Remove(Data);
                    await _DBContext.SaveChangesAsync();
                    return Ok(new { Status = "OK", Result = "Successfully Removed" });
                }
                else
                {
                    return Ok(new { Status = "Fail", Result = "Category Not Found" });
                }

            }
            catch (Exception ex)
            {
                return Ok(new { Status = "Error", Result = "Error: " + ex.Message });
            }
        }
    }
}
