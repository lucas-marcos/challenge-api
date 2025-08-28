namespace ServiceControl.Domain.ValueObjects;

public class OrderId : ValueObject
{
    public string Value { get; private set; } = string.Empty;

    private OrderId(string value)
    {
        Value = value;
    }

    private OrderId() { }

    public static OrderId Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Order ID cannot be empty", nameof(value));

        return new OrderId(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(OrderId id) => id.Value;
}
