namespace Marketplace.Domain;

public sealed class PremiumCustomer : CustomerProfile
{
    public PremiumCustomer(int discountPercent) : base()
    {
        if (discountPercent is < 0 or > 50)
        {
            throw new ArgumentOutOfRangeException(
                nameof(discountPercent),
                "Скидка премиального покупателя должна быть от 0 до 50 процентов.");
        }

        DiscountPercent = discountPercent;
    }

    public int DiscountPercent { get; }

    public string GetPremiumInfo() =>
        $"{base.GetDisplayName()}, премиальная скидка: {DiscountPercent}%";
}
