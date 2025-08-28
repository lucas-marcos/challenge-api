namespace ServiceControl.Domain.ValueObjects;

public class OrderDescription : ValueObject
{
    public string Value { get; private set; } = string.Empty;

    private OrderDescription(string value)
    {
        Value = value;
    }

    private OrderDescription() { }

    public static OrderDescription Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Order description cannot be empty", nameof(value));

        if (value.Length > 500)
            throw new ArgumentException("Order description cannot exceed 500 characters", nameof(value));

        return new OrderDescription(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(OrderDescription description) => description.Value;
}
