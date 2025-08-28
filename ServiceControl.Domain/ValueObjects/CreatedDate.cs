namespace ServiceControl.Domain.ValueObjects;

public class CreatedDate : ValueObject
{
    public DateTime Value { get; private set; }

    private CreatedDate(DateTime value)
    {
        Value = value;
    }

    private CreatedDate() { }

    public static CreatedDate Create(DateTime value)
    {
        if (value == default)
            throw new ArgumentException("Created date cannot be default", nameof(value));

        return new CreatedDate(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator DateTime(CreatedDate date) => date.Value;
}
