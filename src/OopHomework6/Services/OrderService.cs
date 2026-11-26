using OrderId = System.Int32;
using Marketplace.Domain;

namespace Marketplace.Services;

public sealed class OrderService
{
    public string BuildGreeting(CustomerProfile? customer) => customer?.FullName ?? "гость";

    public int GetDiscount(CustomerProfile? customer) =>
        customer is PremiumCustomer premium ? premium.DiscountPercent : 0;

    public string GetEmail(CustomerProfile? customer) =>
        customer?.Email ?? ContactRules.UnknownEmail;

    public int GetTotal(IEnumerable<CartItem>? items)
    {
        return new CheckoutService().GetTotal(EnsureItems(items));
    }

    public string FormatOrderNumber(OrderId id) => $"Заказ №{id}";

    private static IEnumerable<CartItem> EnsureItems(IEnumerable<CartItem>? items)
    {
        ArgumentNullException.ThrowIfNull(items);
        return items;
    }
}
