using System.Text;
using Marketplace.Domain;

namespace Marketplace.Services;

public static class ReceiptService
{
    public static string BuildReceipt(IEnumerable<CartItem> items)
    {
        ArgumentNullException.ThrowIfNull(items);
        var itemList = items.ToArray();
        var total = new CheckoutService().GetTotal(itemList);
        var totalWithVat = PriceRules.WithVat(total);
        var orderNumber = OrderNumberGenerator.Next();

        var receipt = new StringBuilder();
        receipt.AppendLine($"Заказ №{orderNumber}");

        foreach (var item in itemList)
            receipt.AppendLine(PrepareLine(item));

        receipt.AppendLine($"Итого без НДС: {FormatMoney(total)}");
        receipt.Append($"Итого с НДС: {FormatMoney(totalWithVat)}");
        return receipt.ToString();
    }

    private static string PrepareLine(CartItem item) =>
        $"Товар {item.ProductId}: {item.Quantity} × {FormatMoney(item.PricePerUnit)} = {FormatMoney(item.Total)}";

    private static string FormatMoney(int amount) => $"{amount} {PriceRules.DefaultCurrency}";
}
