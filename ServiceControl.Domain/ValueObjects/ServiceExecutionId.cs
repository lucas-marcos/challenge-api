namespace ServiceControl.Domain.ValueObjects;

public class ServiceExecutionId : ValueObject
{
    public string Value { get; private set; } = string.Empty;

    private ServiceExecutionId(string value)
    {
        Value = value;
    }

    private ServiceExecutionId() { }

    public static ServiceExecutionId Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Service execution ID cannot be empty", nameof(value));

        return new ServiceExecutionId(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(ServiceExecutionId id) => id.Value;
}
