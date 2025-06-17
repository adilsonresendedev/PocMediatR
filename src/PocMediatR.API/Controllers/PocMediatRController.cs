using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PocMediatR.API.Filters;

namespace PocMediatR.API.Controllers
{
    [ApiController]
    [ValidateModelState]
    //[Authorize]
    public class PocMediatRController : ControllerBase
    {
    }
}
