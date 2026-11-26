using CustomerId = System.Int32;
using static Marketplace.Domain.ContactRules;

namespace Marketplace.Domain;

public class CustomerProfile
{
    private string fullName = string.Empty;
    private int rating;

    public CustomerId Id { get; init; }

    public required string FullName
    {
        get => fullName;
        init
        {
            ArgumentNullException.ThrowIfNull(value);
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Имя покупателя обязательно.", nameof(value));

            fullName = value.Trim();
        }
    }

    public string? MiddleName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public int? PreferredPickupPointId { get; set; }

    public int Rating
    {
        get => rating;
        set
        {
            EnsureRating(value);
            rating = value;
        }
    }

    public bool IsTrusted => Rating >= 4;

    public string GetDisplayName() => $"{FullName} {MiddleName ?? string.Empty}".Trim();

    public string GetEmail() => Email ?? UnknownEmail;

    public void EnsureEmail() => Email ??= UnknownEmail;

    public string TryNormalizeEmail() => NormalizeEmail(Email);

    public string GetInfo() => GetInfo(true);

    public string GetInfo(bool includeRating)
    {
        var mainPart = $"{GetDisplayName()}, email: {GetEmail()}";
        return includeRating ? $"{mainPart}, рейтинг: {Rating}" : mainPart;
    }

    public string GetContactInfo(bool includePhone)
    {
        var normalizedPhone = NormalizePhone(Phone);
        return includePhone && normalizedPhone.Length > 0
            ? $"{GetEmail()}; телефон: {normalizedPhone}"
            : GetEmail();
    }

    private static void EnsureRating(int value)
    {
        if (value is < 0 or > 5)
            throw new ArgumentOutOfRangeException(nameof(value), "Рейтинг должен быть от 0 до 5.");
    }

    private static string NormalizePhone(string? value) =>
        string.IsNullOrWhiteSpace(value)
            ? string.Empty
            : new string(value.Where(character => char.IsDigit(character) || character == '+').ToArray());
}
