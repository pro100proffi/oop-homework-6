using Marketplace.Domain;

namespace Marketplace.Services;

public sealed class CheckoutService
{
    public int GetTotal(IEnumerable<CartItem> items)
    {
        var itemList = GetItems(items);
        return checked(itemList.Where(IsAvailable).Sum(item => item.Total));
    }

    public IReadOnlyList<CartItem> FindCheaperThan(IEnumerable<CartItem> items, int maxPrice)
    {
        if (maxPrice < 0)
            throw new ArgumentOutOfRangeException(nameof(maxPrice), "Цена не может быть отрицательной.");

        return GetItems(items)
            .Where(item => IsAvailable(item) && item.PricePerUnit < maxPrice)
            .ToArray();
    }

    public string GetReport(IEnumerable<CartItem> items) => GetReport(items, false);

    public string GetReport(IEnumerable<CartItem> items, bool includeLines)
    {
        var itemList = GetItems(items).Where(IsAvailable).ToArray();
        var summary = $"Позиций: {itemList.Length}; итог: {GetTotal(itemList)} у.е.";

        if (!includeLines || itemList.Length == 0)
            return summary;

        return string.Join(Environment.NewLine, new[] { summary }.Concat(itemList.Select(PrepareLine)));
    }

    private static bool IsAvailable(CartItem item) => item.Quantity > 0 && item.PricePerUnit >= 0;

    private static string PrepareLine(CartItem item) =>
        $"Товар {item.ProductId}: {item.Quantity} × {item.PricePerUnit} = {item.Total} у.е.";

    private static IReadOnlyList<CartItem> GetItems(IEnumerable<CartItem> items)
    {
        ArgumentNullException.ThrowIfNull(items);
        return items.ToArray();
    }
}

