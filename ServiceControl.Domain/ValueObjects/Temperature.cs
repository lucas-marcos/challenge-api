namespace ServiceControl.Domain.ValueObjects;

public class Temperature : ValueObject
{
    public decimal Value { get; private set; }

    private Temperature(decimal value)
    {
        Value = value;
    }

    private Temperature() { }

    public static Temperature Create(decimal value)
    {
        if (value < -100 || value > 100)
            throw new ArgumentException("Temperature must be between -100 and 100 degrees Celsius", nameof(value));

        return new Temperature(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator decimal(Temperature temperature) => temperature.Value;
}
