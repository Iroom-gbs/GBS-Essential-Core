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

    [HttpGet("timetable/{grd:int}/{cls:int}")]
    public IActionResult GetClass(int grd, int cls) => Comsi.GetJsonOf($"{grd}-{cls}") switch
    {
        null => NotFound(new
        {
            Error = $"Class {grd}-{cls} not found on parsed list. If you are right, then try parse first.",
            Data = Array.Empty<object>()
        }),
        var x => Ok(x)
    };

    [HttpGet("timetable/raw/{grd:int}/{cls:int}")]
    public IActionResult GetRawClass(int grd, int cls) => Comsi.GetRawJsonOf($"{grd}-{cls}") switch
    {
        null => NotFound(new
        {
            Error = $"Class {grd}-{cls} not found on parsed list. If you are right, then try parse first.",
            Data = Array.Empty<object>()
        }),
        var x => Ok(x)
    };
    
    [HttpGet("timetable/{grd:int}/{cls:int}/{date:int}")]
    public IActionResult GetClass(int grd, int cls, int date) => Comsi.GetJsonOf($"{grd}-{cls}", date) switch
    {
        null => NotFound(new
        {
            Error = $"Class {grd}-{cls} Date: {date} not found on parsed list. If you are right, then try parse first.",
            Data = Array.Empty<object>()
        }),
        var x => Ok(x)
    };
    
    [HttpGet("timetable/raw/{grd:int}/{cls:int}/{date:int}")]
    public IActionResult GetRawClass(int grd, int cls, int date) => Comsi.GetRawJsonOf($"{grd}-{cls}", date) switch
    {
        null => NotFound(new
        {
            Error = $"Class {grd}-{cls} Date: {date} not found on parsed list. If you are right, then try parse first.",
            Data = Array.Empty<object>()
        }),
        var x => Ok(x)
    };
    
    [HttpGet("timetable/teacher/{subject}/{teacher}")]
    public IActionResult GetTeacherInfo(string subject, string teacher)
        => Ok(Comsi.GetTeacherClass(subject, teacher));

    [HttpGet("meal")]
    public IActionResult GetMealInfo()
        => Ok();

    [HttpGet("meal/{time:int}")]
    public IActionResult GetMealInfo(int time)
        => Ok();
} 