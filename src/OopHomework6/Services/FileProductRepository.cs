using System.Globalization;
using System.Text;
using Marketplace.Domain;

namespace Marketplace.Services;

/// <summary>
/// Формат строки: base64(название);base64(артикул);цена;base64(статус).
/// </summary>
internal sealed class FileProductRepository : IProductRepository
{
    private readonly string filePath;

    public FileProductRepository(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("Путь к файлу обязателен.", nameof(filePath));

        this.filePath = filePath;
    }

    public void Save(IEnumerable<Product> products)
    {
        ArgumentNullException.ThrowIfNull(products);

        var directory = Path.GetDirectoryName(Path.GetFullPath(filePath));
        if (!string.IsNullOrEmpty(directory))
            Directory.CreateDirectory(directory);

        File.WriteAllLines(filePath, products.Select(Serialize), new UTF8Encoding(false));
    }

    public List<Product> Load()
    {
        if (!File.Exists(filePath))
            return [];

        return File.ReadLines(filePath, Encoding.UTF8)
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(Deserialize)
            .ToList();
    }

    private static string Serialize(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        return string.Join(
            ';',
            Encode(product.Title),
            Encode(product.Article),
            product.Price.ToString(CultureInfo.InvariantCulture),
            Encode(product.Status));
    }

    private static Product Deserialize(string line)
    {
        var parts = line.Split(';');
        if (parts.Length != 4 ||
            !int.TryParse(parts[2], NumberStyles.None, CultureInfo.InvariantCulture, out var price))
        {
            throw new FormatException($"Некорректная строка товара: {line}");
        }

        try
        {
            var product = new Product
            {
                Title = Decode(parts[0]),
                Article = Decode(parts[1]),
                Price = price
            };

            var status = Decode(parts[3]);
            if (!string.Equals(status, product.Status, StringComparison.Ordinal))
                product.ChangeStatus(status);

            return product;
        }
        catch (Exception exception) when (exception is FormatException or ArgumentException)
        {
            throw new FormatException($"Некорректная строка товара: {line}", exception);
        }
    }

    private static string Encode(string value) =>
        Convert.ToBase64String(Encoding.UTF8.GetBytes(value));

    private static string Decode(string value) =>
        Encoding.UTF8.GetString(Convert.FromBase64String(value));
}

