namespace Marketplace.Domain;

public sealed class Discount
{
    private decimal percent;
    private int minSum;

    public required string Name { get; init; }

    public decimal Percent
    {
        get => percent;
        set => percent = EnsurePercent(value);
    }

    public int MinSum
    {
        get => minSum;
        set => minSum = DomainValidation.EnsureNonNegative(
            value,
            nameof(value),
            "Минимальная сумма не может быть отрицательной.");
    }

    public DateTime ValidUntil { get; init; }

    public bool IsActive => ValidUntil > DateTime.Now;

    public int Calculate(int total)
    {
        EnsureTotal(total);
        return IsActive && total >= MinSum ? ApplyDiscount(total, Percent) : total;
    }

    public int Calculate(int total, decimal customPercent)
    {
        EnsureTotal(total);
        return ApplyDiscount(total, EnsurePercent(customPercent));
    }

    private static int ApplyDiscount(int total, decimal percent) =>
        checked((int)decimal.Round(total * (1m - percent / 100m), 0, MidpointRounding.AwayFromZero));

    private static decimal EnsurePercent(decimal value) =>
        DomainValidation.EnsurePercent(value, nameof(value));

    private static void EnsureTotal(int total) =>
        DomainValidation.EnsureNonNegative(total, nameof(total), "Сумма заказа не может быть отрицательной.");
}

