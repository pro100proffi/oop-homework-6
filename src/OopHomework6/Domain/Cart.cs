namespace Marketplace.Domain;

public sealed class Cart
{
    private readonly List<CartItem> items = [];

    public IReadOnlyList<CartItem> Items => items.ToArray();

    public int Count => items.Count;

    public int Total => checked(items.Sum(item => item.Total));

    public void Add(CartItem item) => items.Add(item);

    public void Clear() => items.Clear();
}

