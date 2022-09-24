using System.Reflection;
using API.Abstractions;
using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ApplicationContext applicationContext;
    public WeatherForecastController(ApplicationContext applicationContext)
    {
        this.applicationContext = applicationContext;
    }
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
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
    public string GetAbstracts()
    {
        



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
