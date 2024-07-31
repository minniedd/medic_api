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

            var detail = await _dbContext.User.Select(x => new GetDetailsByIdResponse
            {
                id = x.id,
                name = x.name,
                username = x.username,
                orders = x.orders.Value,
                lastLoginDate = x.lastLoginDate.ToString(),
                imageUrl = x.imageUrl,
                status = x.status,
                birthDate = x.birthDate.ToString()

            }).SingleAsync(x=>x.id == id);

            return detail;
        }
    }
}
