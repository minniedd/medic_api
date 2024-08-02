using medic_api.Data;
using medic_api.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace medic_api.Endpoints.Users.GetAll
{
    [Route("user/[controller]")]
    [ApiController]
    public class UserGetAllEndpoint : MyBaseController<UserGetAllRequest,  UserGetAllResponse>
    {
        private readonly ApplicationDbContext _dbContext;

        public UserGetAllEndpoint(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public override async Task<UserGetAllResponse> Obradi([FromQuery]UserGetAllRequest request)
        {
            var users = await _dbContext.User.Where(x => x.status != "Blocked" && x.isAdmin == false).Select(x => new UserGetAllResponseUser
            {
                id = x.id,
                name = x.name,
                username = x.username,
                lastLoginDate = x.lastLoginDate.ToString()
            }).ToListAsync();

            return new UserGetAllResponse
            {
                Users = users
            };
        }
    }
}
