using FirstDatabaseLab.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FirstDatabaseLab.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherController(ILogger<WeatherController> _logger) : ControllerBase
{
  [HttpGet("[action]")]
  public List<WeatherForecast> Forecast()
  {
    var infoList = Enumerable.Range(1, 5).Select(index =>
    {
      var temperatureC = Random.Shared.Next(-20, 55);
      return new WeatherForecast
      {
        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        TemperatureC = temperatureC,
        Summary = MapSummary(temperatureC)
      };
    }).ToList();

    _logger.LogInformation("Getting weather forecast");
    return infoList;
  }

  [NonAction]
  private string MapSummary(int temp) => temp switch
  {
    < -10 => "極寒",
    < 0 => "嚴寒",
    < 10 => "寒冷",
    < 15 => "涼爽",
    < 20 => "溫和",
    < 25 => "溫暖",
    < 30 => "舒適",
    < 35 => "炎熱",
    < 40 => "酷熱",
    _ => "灼熱"
  };
}
