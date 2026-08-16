using HL.Core.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HL.API.Controllers;

[Route("api/[controller]")]
public class TestController : BaseController
{
    [HttpGet("public")]
    public IActionResult Public()
        => CreateResponse(ApiResponse<string>.SuccessResponse("Bu endpoint herkese açık."));

    [Authorize]
    [HttpGet("authenticated")]
    public IActionResult Authenticated()
        => CreateResponse(ApiResponse<string>.SuccessResponse("Bu endpoint sadece giriş yapmış kullanıcılar içindir."));

    [Authorize(Roles="Doctor")]
    [HttpGet("doctor")]
    public IActionResult DoctorOnly()
        => CreateResponse(ApiResponse<string>.SuccessResponse("Bu endpoint sadece doktorların erişimine açık."));
}