using medic_api.Data;
using medic_api.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace medic_api.Endpoints.Users.GetDetailsById
{
    [Route("user/[controller]")]
    [ApiController]
    public class GetDetailsByIdEndpoint : MyBaseController<int, GetDetailsByIdResponse>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetDetailsByIdEndpoint(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public override async Task<GetDetailsByIdResponse> Obradi(int id)
        {
            var user = _dbContext.User.FirstOrDefault(x=>x.id == id);

            if(user == null)
            {
                throw new Exception("User not found");
            }

            var detail = new GetDetailsByIdResponse
            {
                id = user.id,
                name = user.name,
                username = user.username,
                orders = user.orders.Value,
                lastLoginDate = user.lastLoginDate.ToString(),
                imageUrl = user.imageUrl,
                status = user.status,
                birthDate = user.birthDate.ToString()

            };

            return detail;
        }

        [HttpPatch("Update")]
        public ActionResult UpdateUserDetails(int id,[FromBody]UpdateUserDetailsRequest request)
        {
            var user = _dbContext.User.FirstOrDefault(x => x.id == id);
            if(user == null)
            {
                return BadRequest("user not found");
            }

            user.name = request.name;
            user.username = request.username;
            user.orders = request.orders;
            user.imageUrl = request.imageUrl;
            user.birthDate = request.birthDate;

            _dbContext.SaveChanges();

            var updatedDetailResponse = new UpdateUserDetailResponse
            {
                name = user.name,
                username = user.username,
                orders = user.orders.Value,
                imageUrl = user.imageUrl,
                birthDate = user.birthDate
            };

            return Ok(updatedDetailResponse);
        }
    }
}
