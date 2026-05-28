using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HL.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("public")]
    public IActionResult Public()
    {
        return Ok("Bu endpoint herkese açık.");
    }

    [Authorize]
    [HttpGet("authenticated")]
    public IActionResult Authenticated()
    {
        return Ok("Bu endpoint sadece giriş yapmış kullanıcılar içindir.");
    }

    [Authorize(Roles="Doctor")]
    [HttpGet("doctor")]
    public IActionResult DoctorOnly()
    {
        return Ok("Bu endpoint sadece doktorların erişimine açık.");
    }
}