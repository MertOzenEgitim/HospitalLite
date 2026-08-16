using HL.Core.Common;
using Microsoft.AspNetCore.Mvc;

namespace HL.API.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    protected IActionResult CreateResponse<T>(ApiResponse<T> response)
        => StatusCode(response.StatusCode, response);
}