using Marketplace.Domain;

namespace Marketplace.Services;

public sealed class CatalogReport
{
    public decimal GetAveragePrice(IEnumerable<Product> products)
    {
        var productList = GetProductList(products);
        return productList.Count == 0
            ? 0m
            : decimal.Round(productList.Average(product => (decimal)product.Price), 2);
    }

    public string GetSummary(IEnumerable<Product> products)
    {
        var productList = GetProductList(products);
        return $"Товаров: {productList.Count}; средняя цена: {GetAveragePrice(productList):N2} у.е.";
    }

    private static IReadOnlyList<Product> GetProductList(IEnumerable<Product> products)
    {
        ArgumentNullException.ThrowIfNull(products);
        return products.ToArray();
    }
}

