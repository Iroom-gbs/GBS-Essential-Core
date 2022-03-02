using GBS_Essential_Core.Data;
using GBS_Essential_Core.Data.Comsi;
using Microsoft.AspNetCore.Mvc;

namespace GBS_Essential_Core.Controllers;

[ApiController]
[Route("action")]
public class ActionController: Controller
{
    [HttpGet]
    public IActionResult Index()
        => Ok("Essential action working fine.");

    [HttpGet("timetable/renew")]
    public IActionResult Parse() => Comsi.ParseAll() switch
    {
        RenewResult.Ok => Ok(), // Ok
        RenewResult.Failed => StatusCode(500), // Internal error
        RenewResult.AlreadyRunning => StatusCode(429), // Too many requests
        _ => StatusCode(500)
    };
}