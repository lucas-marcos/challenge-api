namespace ServiceControl.Domain.ValueObjects;

public class ProcessedDate : ValueObject
{
    public DateTime Value { get; private set; }

    private ProcessedDate(DateTime value)
    {
        Value = value;
    }

    private ProcessedDate() { }

    public static ProcessedDate Create(DateTime value)
    {
        if (value == default)
            throw new ArgumentException("Processed date cannot be default", nameof(value));

        return new ProcessedDate(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator DateTime(ProcessedDate date) => date.Value;
}
