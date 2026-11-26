namespace Marketplace.Domain;

public readonly struct CartItem
{
    public CartItem(int productId, int quantity, int pricePerUnit)
    {
        ProductId = DomainValidation.EnsurePositiveId(productId, nameof(productId));
        Quantity = DomainValidation.EnsurePositiveQuantity(quantity, nameof(quantity));
        PricePerUnit = DomainValidation.EnsureNonNegative(
            pricePerUnit,
            nameof(pricePerUnit),
            "Цена не может быть отрицательной.");
    }

    public int ProductId { get; }

    public int Quantity { get; }

    public int PricePerUnit { get; }

    public int Total => checked(PricePerUnit * Quantity);
}

