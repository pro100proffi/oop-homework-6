namespace Marketplace.Domain;

public static class PriceRules
{
    public const decimal VatRate = 0.2m;
    public const int MaxDiscountPercent = 70;

    public static readonly string DefaultCurrency = "RUB";

    public static int WithVat(int price)
    {
        EnsurePrice(price);
        return checked((int)decimal.Round(price * (1m + VatRate), 0, MidpointRounding.AwayFromZero));
    }

    public static int ApplyDiscount(int price, int percent)
    {
        EnsurePrice(price);
        EnsurePercent(percent);
        return checked((int)decimal.Round(
            price * (100m - percent) / 100m,
            0,
            MidpointRounding.AwayFromZero));
    }

    private static void EnsurePrice(int price)
    {
        if (price < 0)
            throw new ArgumentOutOfRangeException(nameof(price), "Цена не может быть отрицательной.");
    }

    private static void EnsurePercent(int percent)
    {
        if (percent is < 0 or > MaxDiscountPercent)
        {
            throw new ArgumentOutOfRangeException(
                nameof(percent),
                $"Скидка должна быть от 0 до {MaxDiscountPercent} процентов.");
        }
    }
}
