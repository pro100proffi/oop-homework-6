# Отчёт по домашнему заданию № 6

**Занятие:** 6  
**Тема:** `null`, псевдонимы типов, статический импорт и наследование  
**Учебная группа:** 251-337  
**Проект:** маркетплейс

Шестая работа продолжает проект из домашних заданий 2–5. В рабочем коде
выполнено групповое задание № 3. Индивидуальные задания № 1 и № 2 приведены в
разделе с исходным условием, но отдельно не реализовывались.

## Состав рабочей группы

| Участник | Роль | Выполненная часть |
|---|---|---|
| Гаврилов Артем Алексеевич | Лидер | Базовый `Product`, `DigitalProduct`, компоновка, точка входа, проверка и отчёт |
| Ладейнов Матвей | Разработчик предметной области | `PhysicalProduct`, nullable-профиль `CustomerProfile`, `ContactRules` |
| Чулков Егор | Разработчик сервисов | `PremiumCustomer`, `OrderService` |

## Исходное задание

### Постановка

1. Выполнить индивидуально задание 1.
2. Выполнить индивидуально задание 2.
3. Совместно в рабочей группе выполнить задание 3.
4. Сформулировать вопросы по заданиям и передать их лидеру.
5. Лидеру оформить отчёт, приложить вопросы участников и отправить работу.

### Задание 1

Реализовать класс `StudentContacts` с включённым nullable-контекстом:

1. Добавить обязательные свойства `FullName`, `Course`, `AverageGrade`.
2. Добавить nullable-свойства `Email`, `DormRoom`, `Scholarship`.
3. В `Print` безопасно вывести отсутствующие данные.
4. В `EnsureEmail` применить `??=`.
5. В `GetEmailOrDefault` применить `contacts?.Email ?? "нет данных"`.
6. Получать значение стипендии через `?? 0`.
7. Безопасно получать длину имени через `?.` и `??`.
8. Продемонстрировать действие null-forgiving оператора `!` и директив
   nullable-контекста.

Проверить предупреждения при присваивании `null`, различие `int` и `int?`,
безопасный вывод, работу `??=`, `?.`, `??` и поведение `!` во время выполнения.

### Задание 2

Ответить одной строкой кода на вопросы о подавлении nullable-предупреждения,
объявлении `int?`, проверке `HasValue`, безопасном получении `int`, операторах
`?.`, `??`, `??=`, псевдониме `StudentId`, статическом импорте `System.Math`,
вызове `base(fullName)` и порядке конструкторов.

### Задание 3 — групповое

Продолжить проект маркетплейса из занятий 2–5. Использовать по одному типу на
файл и только пространства имён `Marketplace.Domain` и
`Marketplace.Services`. Названия существующих типов не менять. Nullable-члены
помечать `?`, обязательные значения проверять null guard. В проекте должен быть
включён `<Nullable>enable</Nullable>`.

#### Часть 1 — Product и DigitalProduct

1. В `Product` использовать псевдоним `ProductId = System.Int32` для `Id`.
2. Добавить `string? Description`.
3. Добавить `GetDescription()`, возвращающий описание или
   `"описание не указано"`.
4. Сохранить общий статический счётчик и `required Title`.
5. Создать `DigitalProduct : Product`.
6. Добавить обязательную ссылку `DownloadUrl` и положительный `FileSizeMb`.
7. В конструкторе применить null guard.
8. В `GetFullInfo()` использовать `base.GetDescription()`.

Проверить `null` в описании, исключение для `null`-ссылки, наследование
описания и увеличение общего счётчика.

#### Часть 2 — PhysicalProduct

1. Создать `PhysicalProduct : Product`.
2. Добавить положительный `WeightKg` и nullable `WarehouseCode`.
3. Явно вызвать `: base()`.
4. Добавить `DeliveryInfo => WarehouseCode ?? "склад не назначен"`.
5. В `GetFullInfo()` использовать `base.GetDescription()`.
6. Вынести проверку веса в приватный метод.

Проверить отсутствие склада, отрицательный вес и общий счётчик товаров.

#### Часть 3 — CustomerProfile и ContactRules

1. Использовать псевдоним `CustomerId = System.Int32` для `Id`.
2. Оставить `FullName` обязательным.
3. Сделать `MiddleName`, `Email`, `Phone` nullable.
4. Проверять `Rating` в диапазоне 0–5.
5. Создать статический класс `ContactRules` с `UnknownEmail`,
   `NormalizeEmail` и `LooksLikeEmail`.
6. Подключить `using static Marketplace.Domain.ContactRules`.
7. Реализовать `GetDisplayName`, `GetEmail`, `EnsureEmail` и
   `TryNormalizeEmail` с операторами `??` и `??=`.

Проверить `null`-почту, однократное заполнение через `??=`, нормализацию и
ошибочный рейтинг.

#### Часть 4 — PremiumCustomer и OrderService

1. Создать `PremiumCustomer : CustomerProfile`.
2. Проверять `DiscountPercent` в диапазоне 0–50.
3. В `GetPremiumInfo()` использовать `base.GetDisplayName()`.
4. В `OrderService` безопасно обрабатывать nullable-покупателя.
5. `BuildGreeting(null)` должен возвращать `"гость"`.
6. `GetDiscount` должен распознавать `PremiumCustomer` через `is`.
7. `GetEmail` должен использовать `?.` и `??`.
8. `GetTotal(null)` должен выбрасывать `ArgumentNullException` через null guard.
9. Для `FormatOrderNumber` использовать псевдоним `OrderId = System.Int32`.

#### Компоновка лидера

Точка входа должна создать базовый, цифровой и физический товары; показать
общий счётчик и уникальные номера; продемонстрировать nullable-описание и склад;
создать обычного и премиального покупателей; показать `GetDisplayName`,
`GetEmail`, `EnsureEmail`, скидку и безопасную передачу `null`; обработать
`GetTotal(null)`; вывести порядок базового и производного конструкторов;
продемонстрировать псевдонимы и статический импорт.

#### Требования к отчёту

Отчёт Markdown или DOCX должен содержать титульную часть, состав группы,
исходное задание, описания и листинги решений, таблицу типов и nullable-членов,
доказательства, вопросы участников и выводы. Скриншоты не используются. Проект
должен собираться без исправлений и не содержать мёртвого кода.

## Выполнение группового задания

### Product и DigitalProduct

`Product` стал базовым классом. Существующие цена, артикул, статус, общий
счётчик и время создания сохранены. Название проверяется null guard, описание
может отсутствовать, а `Id` объявлен через локальный псевдоним `ProductId`.
`DigitalProduct` вызывает базовый конструктор, проверяет ссылку и размер файла,
после чего дополняет базовое описание.

Файлы: `src/OopHomework6/Domain/Product.cs` и
`src/OopHomework6/Domain/DigitalProduct.cs`.

```csharp
using ProductId = System.Int32;

public class Product
{
    private static int nextId;
    private string title = string.Empty;

    public Product()
    {
        Id = Interlocked.Increment(ref nextId);
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
                throw new ArgumentException("Название товара обязательно.");
            title = value.Trim();
        }
    }

    public string? Description { get; set; }

    public string GetDescription() =>
        Description ?? "описание не указано";
}
```

```csharp
public sealed class DigitalProduct : Product
{
    public DigitalProduct(string downloadUrl, int fileSizeMb) : base()
    {
        ArgumentNullException.ThrowIfNull(downloadUrl);
        if (string.IsNullOrWhiteSpace(downloadUrl))
            throw new ArgumentException("Ссылка обязательна.");
        if (fileSizeMb <= 0)
            throw new ArgumentOutOfRangeException(nameof(fileSizeMb));

        DownloadUrl = downloadUrl.Trim();
        FileSizeMb = fileSizeMb;
        Console.WriteLine("2. Конструктор DigitalProduct");
    }

    public string DownloadUrl { get; }
    public int FileSizeMb { get; }

    public string GetFullInfo() =>
        $"{Title}: {base.GetDescription()}; файл {FileSizeMb} МБ";
}
```

### PhysicalProduct

Физический товар использует тот же `Id` и обязательное название, но добавляет
вес и склад. Отсутствующий склад является ожидаемым состоянием и обрабатывается
через `??`. Проверка также отклоняет ноль, `NaN` и бесконечность.

Файл: `src/OopHomework6/Domain/PhysicalProduct.cs`.

```csharp
public sealed class PhysicalProduct : Product
{
    public PhysicalProduct(double weightKg, string? warehouseCode) : base()
    {
        EnsureWeight(weightKg);
        WeightKg = weightKg;
        WarehouseCode = warehouseCode;
    }

    public double WeightKg { get; }
    public string? WarehouseCode { get; set; }
    public string DeliveryInfo => WarehouseCode ?? "склад не назначен";

    public string GetFullInfo() =>
        $"{Title}: {base.GetDescription()}; вес {WeightKg} кг; {DeliveryInfo}";

    private static void EnsureWeight(double weightKg)
    {
        if (weightKg <= 0 || double.IsNaN(weightKg) || double.IsInfinity(weightKg))
            throw new ArgumentOutOfRangeException(nameof(weightKg));
    }
}
```

### CustomerProfile и ContactRules

Профиль допускает отсутствие необязательных контактов и отчества. Для
демонстрации nullable-значимого типа добавлен `int? PreferredPickupPointId`.
Обязательное имя проверяется во время выполнения. Статический импорт позволяет
вызывать `UnknownEmail` и `NormalizeEmail` без имени класса.

Файлы: `src/OopHomework6/Domain/CustomerProfile.cs` и
`src/OopHomework6/Domain/ContactRules.cs`.

```csharp
using CustomerId = System.Int32;
using static Marketplace.Domain.ContactRules;

public class CustomerProfile
{
    private string fullName = string.Empty;

    public CustomerId Id { get; init; }
    public required string FullName
    {
        get => fullName;
        init
        {
            ArgumentNullException.ThrowIfNull(value);
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Имя покупателя обязательно.");
            fullName = value.Trim();
        }
    }
    public string? MiddleName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public int? PreferredPickupPointId { get; set; }

    public string GetDisplayName() =>
        $"{FullName} {MiddleName ?? string.Empty}".Trim();

    public string GetEmail() => Email ?? UnknownEmail;

    public void EnsureEmail() => Email ??= UnknownEmail;

    public string TryNormalizeEmail() => NormalizeEmail(Email);
}
```

```csharp
public static class ContactRules
{
    public const string UnknownEmail = "почта не указана";

    public static string NormalizeEmail(string? value) =>
        value is null ? UnknownEmail : value.Trim().ToLowerInvariant();

    public static bool LooksLikeEmail(string? value) =>
        value?.Contains('@') == true;
}
```

### PremiumCustomer и OrderService

Премиальный покупатель расширяет профиль и переиспользует базовое отображаемое
имя. `OrderService` принимает nullable-профиль там, где отсутствие покупателя
означает гостя, но отклоняет `null` вместо коллекции позиций, потому что без
позиций невозможно корректно рассчитать переданный заказ.

Файлы: `src/OopHomework6/Domain/PremiumCustomer.cs` и
`src/OopHomework6/Services/OrderService.cs`.

```csharp
public sealed class PremiumCustomer : CustomerProfile
{
    public PremiumCustomer(int discountPercent) : base()
    {
        if (discountPercent is < 0 or > 50)
            throw new ArgumentOutOfRangeException(nameof(discountPercent));
        DiscountPercent = discountPercent;
    }

    public int DiscountPercent { get; }

    public string GetPremiumInfo() =>
        $"{base.GetDisplayName()}, премиальная скидка: {DiscountPercent}%";
}
```

```csharp
using OrderId = System.Int32;

public sealed class OrderService
{
    public string BuildGreeting(CustomerProfile? customer) =>
        customer?.FullName ?? "гость";

    public int GetDiscount(CustomerProfile? customer) =>
        customer is PremiumCustomer premium ? premium.DiscountPercent : 0;

    public string GetEmail(CustomerProfile? customer) =>
        customer?.Email ?? ContactRules.UnknownEmail;

    public int GetTotal(IEnumerable<CartItem>? items) =>
        new CheckoutService().GetTotal(EnsureItems(items));

    public string FormatOrderNumber(OrderId id) => $"Заказ №{id}";

    private static IEnumerable<CartItem> EnsureItems(
        IEnumerable<CartItem>? items)
    {
        ArgumentNullException.ThrowIfNull(items);
        return items;
    }
}
```

## Таблица проекта

| Тип | Базовый тип | Nullable-члены | Операторы и приёмы | Исполнитель |
|---|---|---|---|---|
| `Product` | `object` | `Description` | `??`, null guard, `ProductId` | Гаврилов |
| `DigitalProduct` | `Product` | нет | `base()`, `base.GetDescription()`, `!`, null guard | Гаврилов |
| `PhysicalProduct` | `Product` | `WarehouseCode` | `??`, `base()` | Ладейнов |
| `CustomerProfile` | `object` | `MiddleName`, `Email`, `Phone`, `PreferredPickupPointId` | `??`, `??=`, `CustomerId`, `using static` | Ладейнов |
| `ContactRules` | `object` | nullable-параметры | `?.`, `??`-логика | Ладейнов |
| `PremiumCustomer` | `CustomerProfile` | унаследованные | `base()`, `base.GetDisplayName()` | Чулков |
| `OrderService` | `object` | nullable-покупатель и позиции | `?.`, `??`, `is`, null guard, `OrderId` | Чулков |

## Доказательства выполнения

Порядок конструкторов цифрового товара:

```text
1. Конструктор Product
2. Конструктор DigitalProduct
```

Основной вывод для товаров:

```text
ProductId=1; Кружка; описание: Керамическая кружка
ProductId=2; Электронная книга; описание: описание не указано
ProductId=3; Стол; описание: Складной стол
Доставка до назначения склада: склад не назначен
Доставка после назначения склада: A-17
Общий Product.CreatedCount: 3
```

Nullable-профиль и премиальный покупатель:

```text
Email действительно null до EnsureEmail: True
Email до EnsureEmail: почта не указана
Email действительно null после EnsureEmail: False
Email после EnsureEmail: почта не указана
Пункт выдачи: 0
Иван Соколов Сергеевич, премиальная скидка: 10%
Нормализованный email: ivan@example.com
```

Безопасная работа сервиса:

```text
Приветствие для null: гость
Email для null: почта не указана
Скидка для null: 0%
Скидка PremiumCustomer: 10%
Заказ №42
Итог заказа: 6000
GetTotal(null): ArgumentNullException — Value cannot be null.
```

Отдельная проверочная сборка подтвердила:

| Попытка | Результат компиляции |
|---|---|
| `product.Id = 10` | `CS0200`: свойство только для чтения |
| `new DigitalProduct(...)` без `Title` | `CS9035`: обязательное свойство не задано |
| `new CustomerProfile()` без `FullName` | `CS9035`: обязательное свойство не задано |
| nullable `Email` присвоить обычной `string` | `CS8600`: возможно значение `null` |
| `int value = null` | `CS0037`: обычный значимый тип не принимает `null` |

Помимо основного запуска выполнено 62 автоматических проверки. Проверены
границы размеров и скидок, `NaN` и бесконечный вес, пустые и `null`-строки,
повторный `EnsureEmail`, метаданные nullable, общий счётчик наследников и
неизменность данных после неуспешных присваиваний.

## Вопросы участников

**Гаврилов Артем:** почему `null!` не защищает программу от исключения?  
`!` влияет только на анализ компилятора и не добавляет проверку во время
выполнения, поэтому в конструкторе всё равно нужен null guard.

**Ладейнов Матвей:** почему отсутствие склада допустимо, а отсутствие ссылки на
цифровой файл — ошибка?  
Склад можно назначить позже, поэтому это ожидаемое состояние. Цифровой товар без
ссылки невозможно получить, поэтому конструктор сразу отклоняет такой объект.

**Чулков Егор:** почему `OrderService.GetTotal(null)` не возвращает ноль?  
Пустая коллекция означает корректный заказ без позиций, а `null` означает, что
вызывающий код вообще не передал требуемые данные. Эти ситуации полезно
различать.

## Выводы

`null` используется как ожидаемое состояние для описания, отчества, контактов,
склада и номера пункта выдачи. Для обязательного названия, имени, ссылки на файл
и коллекции заказа `null` считается ошибкой и отклоняется null guard.

Наследование позволило сохранить общий идентификатор, название, описание, цену
и статус товара, не копируя их в цифровой и физический классы. Псевдонимы
`ProductId`, `CustomerId`, `OrderId` уточняют смысл обычного `int`, а статический
импорт сокращает обращения к правилам контактов внутри профиля.

## Запуск

Из корня проекта:

```powershell
dotnet run --project src/OopHomework6/OopHomework6.csproj
```
