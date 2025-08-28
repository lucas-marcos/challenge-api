namespace ServiceControl.Domain.Interfaces;

public interface IWeatherService
{
    Task<decimal> GetTemperatureAsync(string city);
}
