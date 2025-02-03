using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSSolar_Project.ApplicationContext;
using SSSolar_Project.Models;

namespace SSSolar_Project.Controllers
{
    [Route("api/")]
    [ApiController]
    public class POSTAPIController : ControllerBase
    {
        private readonly ApplicationDBContext _DBContext;

        public POSTAPIController(ApplicationDBContext DataContext)
        {
            _DBContext = DataContext;
        }

        [HttpPost]
        [Route("Authentication")]
        public async Task<IActionResult> Authentication(AdminMaster model) {

            try
            {
                var Data = await _DBContext.AdminMaster.Where(o => o.UserName == model.UserName && o.Password == model.Password)
                                                   .Select(o => new
                                                   {
                                                       o.Id,
                                                       o.FullName,
                                                       o.UserName
                                                   }).FirstOrDefaultAsync();

                if (Data != null)
                {
                    return Ok(new { Status = "Ok", Result = Data });
                }
                else
                {
                    return Ok(new { Status = "Fail", Result = "Incorrect Username or Password" });
                }
            }
            catch (Exception ex) {
                return Ok(new { Status = "Error", Result = "Error: " + ex.Message });
            }
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(AdminMaster AdminModel)
        {
            try
            {
                var Data = await _DBContext.AdminMaster.Where(o => o.Id == AdminModel.Id && o.Password == AdminModel.Password).FirstOrDefaultAsync();

                if (Data != null)
                {
                    Data.Password = AdminModel.newPassword;
                    _DBContext.AdminMaster.Update(Data);
                    await _DBContext.SaveChangesAsync();
                    return Ok(new { Status = "OK", Result = "Password Successfully Changed" });
                }
                else
                {
                    return Ok(new { Status = "Fail", Result = "Old Password is Incorrect" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { Status = "Error", Result = "Error: " + ex.Message });
            }
        }

        [HttpGet]
        [Route("adminDetail/{Id?}")]
        public async Task<IActionResult> adminDetail(int? Id)
        {
            try
            {
                var Data = await _DBContext.AdminMaster.Where(o => o.Id == Id).FirstOrDefaultAsync();
                if (Data != null)
                {
                    return Ok(new { Status = "OK", Result = Data });
                }
                else
                {
                    return Ok(new { Status = "Fail", Result = "Not Found" });
                }
            }
            catch (Exception ex)
            {
                return Ok(new { Status = "Error", Result = "Error: " + ex.Message });
            }
        }

        [HttpPost]
        [Route("UpdateAdmin")]
        public async Task<IActionResult> UpdateAdmin(AdminMaster Model)
        {
            try
            {
                var Data = await _DBContext.AdminMaster.Where(o => o.Id == Model.Id).FirstOrDefaultAsync();

                if (Data != null)
                {
                    Data.FullName = Model.FullName;
                    Data.UserName = Model.UserName;
                    Data.AlternateNo = Model.AlternateNo;
                    Data.ContactNo = Model.ContactNo;
                    Data.Email = Model.Email;

                    _DBContext.AdminMaster.Update(Data);
                    await _DBContext.SaveChangesAsync();
                    return Ok(new { Status = "OK", Result = "Successfully Updated Profile" });
                }
                else
                {
                    // Return not found if admin does not exist
                    return Ok(new { Status = "Fail", Result = "Not Found" });
                }
            }
            catch (Exception ex)
            {
                // Return error response with exception message
                return Ok(new { Status = "Error", Result = "Error: " + ex.Message });
            }
        }

    }
}
