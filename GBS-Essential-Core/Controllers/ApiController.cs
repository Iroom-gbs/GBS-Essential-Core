using System.Diagnostics;
using GBS_Essential_Core.Data.Comsi;
using Microsoft.AspNetCore.Mvc;

namespace GBS_Essential_Core.Controllers;

[ApiController]
[Route("api")]
public class GbsEssentialController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
        => Ok("Essential api is working fine");

    [HttpGet("timetable/{grd}/{cls}")]
    public IActionResult GetClass(int grd, int cls) => Comsi.GetJsonOf($"{grd}-{cls}") switch
    {
        null => NotFound(new
        {
            Error = $"Class {grd}-{cls} not found on parsed list. Is parse action executed?",
            Data = new object[0]
        }),
        var x => Ok(x)
    };
} 