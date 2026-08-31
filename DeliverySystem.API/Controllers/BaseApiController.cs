using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.API.Controllers
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected string CurrentUserId =>
            User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedAccessException("User is not authenticated");
    }
}