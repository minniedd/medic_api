using Microsoft.AspNetCore.Mvc;

namespace medic_api.Helpers
{
    [ApiController]
    public abstract class MyBaseController<TRequest, TResponse> : ControllerBase
    {
        public abstract Task<TResponse> Obradi(TRequest request);
    }
}
