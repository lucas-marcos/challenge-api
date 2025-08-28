namespace ServiceControl.Domain.ValueObjects;

public class ServiceDescription : ValueObject
{
    public string Value { get; private set; } = string.Empty;

    private ServiceDescription(string value)
    {
        Value = value;
    }

    private ServiceDescription() { }

    public static ServiceDescription Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Service description cannot be empty", nameof(value));

        if (value.Length > 500)
            throw new ArgumentException("Service description cannot exceed 500 characters", nameof(value));

        return new ServiceDescription(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(ServiceDescription description) => description.Value;
}
