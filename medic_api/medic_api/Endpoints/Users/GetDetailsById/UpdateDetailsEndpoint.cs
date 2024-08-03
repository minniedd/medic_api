using medic_api.Data;
using medic_api.Data.Models;
using medic_api.Endpoints.Users.GetAll;
using medic_api.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace medic_api.Endpoints.Users.GetDetailsById
{
    [Route("user/[controller]")]
    [ApiController]
    public class UpdateDetailsEndpoint : ControllerBase 
    {
        private readonly ApplicationDbContext _dbContext;

        public UpdateDetailsEndpoint(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpPost("Update")]
        public ActionResult UpdateUserDetails([FromBody]UpdateUserDetailsRequest request)
        {
            var user = _dbContext.User.Find(request.id);

            if (user == null)
            {
                return NotFound(); 
            }

            user.name = request.name;
            user.username = request.username;
            user.orders = request.orders;
            //user.lastLoginDate = DateTime.Parse(request.lastLoginDate);

            if (!string.IsNullOrEmpty(request.lastLoginDate))
            {
                if (DateTime.TryParse(request.lastLoginDate, out var parsedDate))
                {
                    user.lastLoginDate = parsedDate;
                }
                else
                {
                    return BadRequest("invalid date format for lastLoginDate.");
                }
            }
            else
            {
                user.lastLoginDate = null;
            }

            user.imageUrl = request.imageUrl;
            user.status = request.status;
            user.birthDate = request.birthDate;

            _dbContext.Update(user);
            _dbContext.SaveChanges();

            return Ok(user);
        }
    }
}
