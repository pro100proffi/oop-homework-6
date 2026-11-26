namespace Marketplace.Domain;

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
        $"{Title}: {base.GetDescription()}; вес {WeightKg:0.###} кг; {DeliveryInfo}";

    private static void EnsureWeight(double weightKg)
    {
        if (weightKg <= 0 || double.IsNaN(weightKg) || double.IsInfinity(weightKg))
        {
            throw new ArgumentOutOfRangeException(
                nameof(weightKg),
                "Вес должен быть конечным положительным числом.");
        }
    }
}
