using Microsoft.AspNetCore.Mvc;

namespace WebAppEssentials.Controllers;

/// <summary>
/// This is the base controller for all API controllers in the WebAppEssentials.WebApi project.
/// It provides common functionality and configurations for all API controllers.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
}