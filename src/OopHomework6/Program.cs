using Marketplace.Domain;

namespace Marketplace.Services;

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("=== Порядок конструкторов ===");
        var products = CreateProducts();
        Console.WriteLine();

        DemonstrateProducts(products);

        var customers = CreateCustomers();
        DemonstrateCustomers(customers.Regular, customers.Premium);

        var cart = CreateCart(products);
        DemonstrateOrderService(cart, customers.Premium);
        DemonstratePreviousFeatures(products, cart);
        DemonstrateValidation();
    }

    private static List<Product> CreateProducts() =>
    [
        new Product
        {
            Title = "Кружка",
            Article = "MUG-01",
            Price = 450,
            Description = "Керамическая кружка"
        },
        new DigitalProduct("https://example.com/book", 12)
        {
            Title = "Электронная книга",
            Article = "BOOK-02",
            Price = 600,
            Description = null
        },
        new PhysicalProduct(1.5, null)
        {
            Title = "Стол",
            Article = "TABLE-03",
            Price = 4_500,
            Description = "Складной стол"
        }
    ];

    private static void DemonstrateProducts(IReadOnlyList<Product> products)
    {
        Console.WriteLine("=== Наследование и nullable-данные товаров ===");
        foreach (var product in products)
        {
            Console.WriteLine(
                $"ProductId={product.Id}; {product.Title}; описание: {product.GetDescription()}");
        }

        var digital = (DigitalProduct)products[1];
        var physical = (PhysicalProduct)products[2];
        Console.WriteLine(digital.GetFullInfo());
        Console.WriteLine(physical.GetFullInfo());
        products[0].ChangeStatus("Под заказ");
        Console.WriteLine(products[0].GetInfo());
        Console.WriteLine(products[0].GetInfo(false));
        Console.WriteLine($"Время создания первого товара: {products[0].CreatedAt:O}");
        Console.WriteLine($"Доставка до назначения склада: {physical.DeliveryInfo}");
        physical.WarehouseCode = "A-17";
        Console.WriteLine($"Доставка после назначения склада: {physical.DeliveryInfo}");
        Console.WriteLine($"Общий Product.CreatedCount: {Product.CreatedCount}");
        Console.WriteLine($"Product.MaxPrice: {Product.MaxPrice}");
        Console.WriteLine($"Product.CatalogStartedAt: {Product.CatalogStartedAt:O}");
        Console.WriteLine($"Длина проверенной ссылки: {digital.DownloadUrl!.Length}");
        Console.WriteLine();
    }

    private static (CustomerProfile Regular, PremiumCustomer Premium) CreateCustomers()
    {
        var regular = new CustomerProfile
        {
            Id = 1,
            FullName = "Анна Петрова",
            MiddleName = null,
            Email = null,
            Phone = null,
            PreferredPickupPointId = null,
            Rating = 4
        };

        var premium = new PremiumCustomer(10)
        {
            Id = 2,
            FullName = "Иван Соколов",
            MiddleName = "Сергеевич",
            Email = "  IVAN@EXAMPLE.COM  ",
            Phone = "+7 (900) 555-10-20",
            PreferredPickupPointId = 12,
            Rating = 5
        };

        return (regular, premium);
    }

    private static void DemonstrateCustomers(CustomerProfile regular, PremiumCustomer premium)
    {
        Console.WriteLine("=== Покупатели и операторы null ===");
        Console.WriteLine($"Обычный покупатель: {regular.GetDisplayName()}");
        Console.WriteLine($"Email действительно null до EnsureEmail: {regular.Email is null}");
        Console.WriteLine($"Email до EnsureEmail: {regular.GetEmail()}");
        regular.EnsureEmail();
        Console.WriteLine($"Email действительно null после EnsureEmail: {regular.Email is null}");
        Console.WriteLine($"Email после EnsureEmail: {regular.GetEmail()}");
        regular.EnsureEmail();
        Console.WriteLine($"Повторный EnsureEmail не изменил значение: {regular.GetEmail()}");
        Console.WriteLine($"Пункт выдачи: {regular.PreferredPickupPointId ?? 0}");
        Console.WriteLine(premium.GetPremiumInfo());
        Console.WriteLine($"Нормализованный email: {premium.TryNormalizeEmail()}");
        Console.WriteLine($"Email похож на корректный: {ContactRules.LooksLikeEmail(premium.Email)}");
        Console.WriteLine(premium.GetInfo());
        Console.WriteLine(premium.GetInfo(false));
        Console.WriteLine($"Надёжный покупатель: {premium.IsTrusted}");
        Console.WriteLine($"Контакты: {premium.GetContactInfo(true)}");
        Console.WriteLine();
    }

    private static Cart CreateCart(IReadOnlyList<Product> products)
    {
        var cart = new Cart();
        cart.Add(new CartItem(products[0].Id, 2, products[0].Price));
        cart.Add(new CartItem(products[1].Id, 1, products[1].Price));
        cart.Add(new CartItem(products[2].Id, 1, products[2].Price));
        return cart;
    }

    private static void DemonstrateOrderService(Cart cart, PremiumCustomer premium)
    {
        Console.WriteLine("=== OrderService ===");
        var service = new OrderService();
        Console.WriteLine($"Приветствие для null: {service.BuildGreeting(null)}");
        Console.WriteLine($"Email для null: {service.GetEmail(null)}");
        Console.WriteLine($"Скидка для null: {service.GetDiscount(null)}%");
        Console.WriteLine($"Скидка PremiumCustomer: {service.GetDiscount(premium)}%");
        Console.WriteLine($"Приветствие: {service.BuildGreeting(premium)}");
        Console.WriteLine(service.FormatOrderNumber(42));
        Console.WriteLine($"Итог заказа: {service.GetTotal(cart.Items)}");
        TryAction("GetTotal(null)", () => service.GetTotal(null));
        Console.WriteLine();
    }

    private static void DemonstratePreviousFeatures(IReadOnlyList<Product> products, Cart cart)
    {
        Console.WriteLine("=== Возможности предыдущих работ ===");
        var money = Money.FromRubles(cart.Total).Add(Money.Zero);
        Console.WriteLine($"Money: {money}");
        Console.WriteLine($"Money.MaxAmount: {Money.MaxAmount}");
        Console.WriteLine($"Ставка НДС: {PriceRules.VatRate:P0}");
        Console.WriteLine($"Максимальная скидка: {PriceRules.MaxDiscountPercent}%");
        Console.WriteLine($"С НДС: {PriceRules.WithVat(cart.Total)} {PriceRules.DefaultCurrency}");
        Console.WriteLine($"Через PriceRules со скидкой 10%: {PriceRules.ApplyDiscount(cart.Total, 10)}");

        var discount = new Discount
        {
            Name = "Постоянный покупатель",
            Percent = 10m,
            MinSum = 500,
            ValidUntil = DateTime.Now.AddDays(7)
        };
        Console.WriteLine($"Скидка активна: {discount.IsActive}");
        Console.WriteLine($"Со скидкой: {discount.Calculate(cart.Total)} у.е.");
        Console.WriteLine($"С разовой скидкой 15%: {discount.Calculate(cart.Total, 15m)} у.е.");
        var checkout = new CheckoutService();
        Console.WriteLine(checkout.GetReport(cart.Items));
        Console.WriteLine(checkout.GetReport(cart.Items, true));
        Console.WriteLine($"Позиций дешевле 1000: {checkout.FindCheaperThan(cart.Items, 1_000).Count}");
        Console.WriteLine(ReceiptService.BuildReceipt(cart.Items));
        Console.WriteLine($"Последний номер чека: {OrderNumberGenerator.LastNumber}");
        Console.WriteLine($"Позиций в корзине: {cart.Count}");

        var temporaryCart = new Cart();
        temporaryCart.Add(cart.Items[0]);
        temporaryCart.Clear();
        Console.WriteLine($"Позиций после Clear во временной корзине: {temporaryCart.Count}");

        var catalog = new Catalog();
        foreach (var product in products)
            catalog.Add(product);

        Console.WriteLine($"Поиск товара: {catalog.Find("Кружка")?.Title ?? "не найден"}");
        Console.WriteLine($"Удаление отсутствующего товара: {catalog.Remove("Несуществующий")}");
        Console.WriteLine($"Количество товаров: {catalog.GetCount()}");

        var dataFile = Path.Combine(AppContext.BaseDirectory, "Data", "products.csv");
        IProductRepository repository = new FileProductRepository(dataFile);
        repository.Save(catalog.GetProducts());
        var loadedProducts = repository.Load();
        new CatalogPrinter().PrintAll(loadedProducts);
        new CatalogPrinter().PrintTop(loadedProducts, 2);
        var report = new CatalogReport();
        Console.WriteLine(report.GetSummary(loadedProducts));
        Console.WriteLine($"Средняя цена: {report.GetAveragePrice(loadedProducts):0.##}");
        Console.WriteLine();
    }

    private static void DemonstrateValidation()
    {
        Console.WriteLine("=== Проверка ограничений ===");
        TryAction(
            "null-ссылка цифрового товара",
            () => _ = new DigitalProduct(null!, 12) { Title = "Книга" });
        TryAction(
            "нулевой размер файла",
            () => _ = new DigitalProduct("https://example.com", 0) { Title = "Книга" });
        TryAction(
            "отрицательный вес",
            () => _ = new PhysicalProduct(-1, "A-1") { Title = "Стол" });
        TryAction(
            "некорректный рейтинг",
            () => _ = new CustomerProfile { FullName = "Анна", Rating = 7 });
        TryAction(
            "некорректная премиальная скидка",
            () => _ = new PremiumCustomer(51) { FullName = "Анна" });
    }

    private static void TryAction(string situation, Action action)
    {
        try
        {
            action();
        }
        catch (Exception exception) when (exception is ArgumentException or OverflowException)
        {
            Console.WriteLine($"{situation}: {exception.GetType().Name} — {exception.Message}");
        }
    }
}
