namespace Marketplace.Domain;

public sealed class Catalog
{
    private readonly List<Product> products = [];

    public void Add(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        if (products.Any(existing => SameTitle(existing.Title, product.Title)))
            throw new InvalidOperationException($"Товар «{product.Title}» уже есть в каталоге.");

        products.Add(product);
    }

    public Product? Find(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            return null;

        var normalizedTitle = Normalize(title);
        return products.FirstOrDefault(product => Normalize(product.Title) == normalizedTitle);
    }

    public bool Remove(string title)
    {
        var product = Find(title);
        return product is not null && products.Remove(product);
    }

    public IReadOnlyList<Product> GetProducts() => products.ToArray();

    public int GetCount() => products.Count;

    private static string Normalize(string title) => title.Trim().ToUpperInvariant();

    private static bool SameTitle(string left, string right) => Normalize(left) == Normalize(right);
}

