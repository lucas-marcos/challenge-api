namespace ServiceControl.Domain.ValueObjects;

public class WeatherCondition : ValueObject
{
    public string Value { get; private set; } = string.Empty;

    private WeatherCondition(string value)
    {
        Value = value;
    }

    private WeatherCondition() { }

    public static WeatherCondition Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Weather condition cannot be empty", nameof(value));

        if (value.Length > 100)
            throw new ArgumentException("Weather condition cannot exceed 100 characters", nameof(value));

        return new WeatherCondition(value);
    }

    public static WeatherCondition CreateFromTemperature(decimal temperature)
    {
        var condition = temperature switch
        {
            >= 15 and <= 30 => "ótimas condições",
            >= 10 and <= 14 => "agradável",
            _ => "impraticável"
        };

        return new WeatherCondition(condition);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(WeatherCondition condition) => condition.Value;
}
