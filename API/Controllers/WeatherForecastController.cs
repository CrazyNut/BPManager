using System.Reflection;
using System.Text.Json;
using API.ProcessExecutor.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class WeatherForecastController : BaseContoller
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IProcessElementTypesService processElementTypesService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IProcessElementTypesService processElementTypesService)
    {
        _logger = logger;
        this.processElementTypesService = processElementTypesService;
    }

    [HttpGet("GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("getAbstracts")]
    public ActionResult<string> GetAbstracts()
    {
        var types = processElementTypesService.GetProcessElementTypes();
        string s = JsonSerializer.Serialize(types);
        return s;


        // Type[] types = GetInheritedClasses(typeof(BaseProcessElement));
        // List<BaseProcessElement> list = new List<BaseProcessElement>();
        // BaseProcessElement? baseProcessElementE = (BaseProcessElement?)Activator.CreateInstance(types[0]);
        // if(baseProcessElementE != null)
        //     list.Add(baseProcessElementE);
        // return list[0].Execute().ToString();
    }

    Type[] GetInheritedClasses(Type MyType) 
    {
        //if you want the abstract classes drop the !TheType.IsAbstract but it is probably to instance so its a good idea to keep it.
        return Assembly.GetAssembly(MyType).GetTypes().Where(TheType => TheType.IsClass && !TheType.IsAbstract && TheType.IsSubclassOf(MyType)).ToArray();
    }

    
}
