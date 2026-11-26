namespace Marketplace.Domain;

internal static class DomainValidation
{
    public static int EnsurePositiveId(int value, string parameterName)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException(parameterName, "Идентификатор должен быть больше нуля.");

        return value;
    }

    public static int EnsurePositiveQuantity(int value, string parameterName)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException(parameterName, "Количество должно быть больше нуля.");

        return value;
    }

    public static int EnsureNonNegative(int value, string parameterName, string message)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(parameterName, message);

        return value;
    }

    public static int EnsureRange(int value, int minimum, int maximum, string parameterName)
    {
        if (value < minimum || value > maximum)
            throw new ArgumentOutOfRangeException(
                parameterName,
                $"Значение должно быть от {minimum} до {maximum}.");

        return value;
    }

    public static decimal EnsurePercent(decimal value, string parameterName)
    {
        if (value is < 0m or > 100m)
            throw new ArgumentOutOfRangeException(parameterName, "Процент должен быть от 0 до 100.");

        return value;
    }
}

