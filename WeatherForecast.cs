namespace Sicu;

/// <summary>
/// WeatherForecast
/// </summary>
public class WeatherForecast
{
    /// <summary>
    /// Date
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// TemperatureC
    /// </summary>
    public int TemperatureC { get; set; }

    /// <summary>
    /// TemperatureF
    /// </summary>
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    /// <summary>
    /// Summary WeatherForecast
    /// </summary>
    public string? Summary { get; set; }
}