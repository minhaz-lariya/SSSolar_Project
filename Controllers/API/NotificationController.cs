using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSSolar_Project.ApplicationContext;
using SSSolar_Project.Models;

namespace SSSolar_Project.Controllers.API
{
    [Route("api")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly ApplicationDBContext _DBContext;
        public NotificationController(ApplicationDBContext DataContext)
        {
            _DBContext = DataContext;
        }

        [HttpPost]
        [Route("newNotification")]
        public async Task<IActionResult> newNotification(Notification Model)
        {
            try
            {
                _DBContext.Notification.Add(Model);
                await _DBContext.SaveChangesAsync();
                return Ok(new { Status = "OK", Result = "Successfully Saved" });
            }
            catch (Exception ex)
            {
                return Ok(new { Status = "Error", Result = "Error: " + ex.Message });
            }
        }

        [HttpPost]
        [Route("EditNotification")]
        public async Task<IActionResult> EditNotification(Notification Model)
        {
            try
            {
                _DBContext.Notification.Update(Model);
                await _DBContext.SaveChangesAsync();
                return Ok(new { Status = "OK", Result = "Successfully Saved" });
            }
            catch (Exception ex)
            {
                return Ok(new { Status = "Fail", Result = "Error: " + ex.Message });
            }
        }

        [HttpGet]
        [Route("Notifications/{For}")]
        public async Task<IActionResult> Notifications(string For)
        {
            try
            {
                if (For == "Admin")
                {
                    var Data = await _DBContext.Notification.ToListAsync();
                    return Ok(new { Status = "OK", Result = Data });
                }
                else
                {
                    var Data = await _DBContext.Notification.Where(o=> o.Status == "true").ToListAsync();
                    return Ok(new { Status = "OK", Result = Data });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { Status = "Fail", Result = "Error: " + ex.Message });
            }
        }

        [HttpGet]
        [Route("detailNotification/{Id?}")]
        public async Task<IActionResult> detailNotification(int Id)
        {
            try
            {
                var Data = await _DBContext.Notification.Where(o => o.Id == Id).FirstOrDefaultAsync();
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
        [Route("deleteNotification/{Id?}")]
        public async Task<IActionResult> deleteNotification(int? Id)
        {
            try
            {
                var Data = _DBContext.Notification.Find(Id);

                if (Data != null)
                {
                    _DBContext.Notification.Remove(Data);
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
