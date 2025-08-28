namespace ServiceControl.Domain.ValueObjects;

public class ResponsiblePerson : ValueObject
{
    public string Value { get; private set; } = string.Empty;

    private ResponsiblePerson(string value)
    {
        Value = value;
    }

    private ResponsiblePerson() { }

    public static ResponsiblePerson Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Responsible person cannot be empty", nameof(value));

        if (value.Length > 200)
            throw new ArgumentException("Responsible person cannot exceed 200 characters", nameof(value));

        return new ResponsiblePerson(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(ResponsiblePerson person) => person.Value;
}
