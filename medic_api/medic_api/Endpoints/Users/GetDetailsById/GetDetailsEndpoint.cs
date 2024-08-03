using medic_api.Data;
using medic_api.Data.Models;
using medic_api.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace medic_api.Endpoints.Users.GetDetailsById
{
    [Route("user/[controller]")]
    [ApiController]
    public class GetDetailsEndpoint : ControllerBase
    {

        private readonly ApplicationDbContext _dbContext;

        public GetDetailsEndpoint(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllDetails(int id)
        {
            var detail = _dbContext.User.FirstOrDefault(x => x.id == id);

            if(detail == null)
            {
                return NotFound();
            }

            var userdetails = new
            {
                detail.id,
                detail.name,
                detail.username,
                detail.orders,
                detail.lastLoginDate,
                detail.imageUrl,
                detail.status,
                detail.birthDate
            };

            return Ok(userdetails);
        }
    }
}
