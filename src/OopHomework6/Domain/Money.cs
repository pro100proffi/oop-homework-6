namespace Marketplace.Domain;

public readonly struct Money
{
    public const decimal MaxAmount = 10_000_000m;

    public static readonly Money Zero = new(0m, "RUB");

    public Money(decimal amount, string currency)
    {
        EnsureAmount(amount);
        EnsureCurrency(currency);

        Amount = amount;
        Currency = currency.Trim().ToUpperInvariant();
    }

    public decimal Amount { get; }

    public string Currency { get; }

    public static Money FromRubles(decimal amount) => new(amount, "RUB");

    public Money Add(Money other)
    {
        EnsureCurrency(Currency);
        EnsureCurrency(other.Currency);

        if (!string.Equals(Currency, other.Currency, StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Нельзя складывать суммы в разных валютах.");

        return new Money(checked(Amount + other.Amount), Currency);
    }

    public override string ToString() => $"{Amount:0.##} {Currency}";

    private static void EnsureAmount(decimal amount)
    {
        if (amount is < 0m or > MaxAmount)
        {
            throw new ArgumentOutOfRangeException(
                nameof(amount),
                $"Сумма должна быть от 0 до {MaxAmount}.");
        }
    }

    private static void EnsureCurrency(string? currency)
    {
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Код валюты обязателен.", nameof(currency));
    }
}
