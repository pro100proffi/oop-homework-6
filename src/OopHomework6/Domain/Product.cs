using ProductId = System.Int32;

namespace Marketplace.Domain;

public class Product
{
    public const int MaxPrice = 1_000_000;

    private static readonly HashSet<string> AllowedStatuses =
    [
        "В наличии",
        "Нет в наличии",
        "Под заказ"
    ];

    private static int nextId;

    public static int CreatedCount => Volatile.Read(ref nextId);

    public static readonly DateTime CatalogStartedAt;

    private string article = string.Empty;
    private string title = string.Empty;
    private int price;
    private readonly DateTime createdAt;

    static Product()
    {
        CatalogStartedAt = DateTime.UtcNow;
    }

    public Product()
    {
        Id = Interlocked.Increment(ref nextId);
        createdAt = DateTime.Now;
        Console.WriteLine("1. Конструктор Product");
    }

    public ProductId Id { get; }

    public required string Title
    {
        get => title;
        init
        {
            ArgumentNullException.ThrowIfNull(value);
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Название товара обязательно.", nameof(value));

            title = value.Trim();
        }
    }

    public string? Description { get; set; }

    public string Article
    {
        get => article;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Артикул обязателен.", nameof(value));

            article = value.Trim();
        }
    }

    public int Price
    {
        get => price;
        set
        {
            if (value is < 0 or > MaxPrice)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value),
                    $"Цена должна быть от 0 до {MaxPrice}.");
            }

            price = value;
        }
    }

    public DateTime CreatedAt => createdAt;

    public bool IsBudget => Price < 25;

    public string Status { get; private set; } = "В наличии";

    public string GetDescription() => Description ?? "описание не указано";

    public string GetInfo() => GetInfo(true);

    public string GetInfo(bool includePrice)
    {
        var mainPart = $"{Title} [{Article}], статус: {Status}";
        return includePrice ? $"{mainPart}, цена: {Price} у.е." : mainPart;
    }

    public void ChangeStatus(string newStatus)
    {
        EnsureStatus(newStatus);
        Status = newStatus.Trim();
    }

    private static void EnsureStatus(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !AllowedStatuses.Contains(value.Trim()))
        {
            throw new ArgumentException(
                "Допустимые статусы: «В наличии», «Нет в наличии», «Под заказ».",
                nameof(value));
        }
    }
}

