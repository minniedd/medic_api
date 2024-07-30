using medic_api.Data;
using medic_api.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace medic_api.Endpoints.Users.Block
{
    [Route("user/[controller]")]
    [ApiController]
    public class BlockUserEndpoint : MyBaseController<int, BlockUserResponse>
    {
        private readonly ApplicationDbContext _dbContext;

        public BlockUserEndpoint(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("{id}")]
        public override async Task<BlockUserResponse> Obradi(int id, CancellationToken cancellationToken)
        {
            var user = await _dbContext.User.FindAsync(id);

            if(user == null)
            {
                return new BlockUserResponse
                {
                    message = "User has not been found!"
                };
            }

            user.isBlocked = true;
            user.status = "Blocked";

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new BlockUserResponse
            {
                message = "User has been blocked"
            };
        }
    }
}
