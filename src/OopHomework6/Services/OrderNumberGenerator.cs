namespace Marketplace.Services;

public static class OrderNumberGenerator
{
    private static int lastNumber;

    static OrderNumberGenerator()
    {
        lastNumber = 1000;
    }

    public static int LastNumber => Volatile.Read(ref lastNumber);

    public static int Next() => Interlocked.Increment(ref lastNumber);
}
