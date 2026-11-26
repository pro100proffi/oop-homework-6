using Marketplace.Domain;

namespace Marketplace.Services;

public sealed class CatalogPrinter
{
    public void PrintAll(IEnumerable<Product> products)
    {
        ArgumentNullException.ThrowIfNull(products);

        foreach (var product in products)
            Console.WriteLine(PrepareLine(product));
    }

    public void PrintTop(IEnumerable<Product> products, int count)
    {
        ArgumentNullException.ThrowIfNull(products);
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count), "Количество не может быть отрицательным.");

        foreach (var product in products.OrderByDescending(product => product.Price).Take(count))
            Console.WriteLine(PrepareLine(product));
    }

    private static string PrepareLine(Product product) =>
        $"{product.Title,-20} | {product.Article,-8} | {product.Price,6} у.е. | {product.Status}";
}

