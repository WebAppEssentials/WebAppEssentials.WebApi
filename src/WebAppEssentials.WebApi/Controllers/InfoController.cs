using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using WebAppEssentials.Models.Info;

namespace WebAppEssentials.Controllers;

/// <inheritdoc />
public class InfoController : BaseController
{
    // GET: api/Info/Version
    [HttpGet("Version")]
    public IActionResult GetVersion()
    {
        var result = new BuildInfo()
        {
            BuildDate = DateTime.MinValue,
            Version = string.Empty
        };
        
        // Get the assembly where this code is executing
        var assembly = Assembly.GetEntryAssembly();

        if (assembly == null) return Ok(result);
        var version = assembly.GetName().Version;
        if (version != null) result.Version = version.ToString();
            
        var filePath = assembly.Location;

        var fileInfo = new FileInfo(filePath);
        result.BuildDate = fileInfo.LastWriteTime;

        return Ok(result);
    }
}