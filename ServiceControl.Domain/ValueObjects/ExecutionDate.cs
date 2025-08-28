namespace ServiceControl.Domain.ValueObjects;

public class ExecutionDate : ValueObject
{
    public DateTime Value { get; private set; }

    private ExecutionDate(DateTime value)
    {
        Value = value;
    }

    private ExecutionDate() { }

    public static ExecutionDate Create(DateTime value)
    {
        if (value == default)
            throw new ArgumentException("Execution date cannot be default", nameof(value));

        return new ExecutionDate(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator DateTime(ExecutionDate date) => date.Value;
}
