namespace Marketplace.Domain;

public sealed class DigitalProduct : Product
{
    public DigitalProduct(string downloadUrl, int fileSizeMb) : base()
    {
        ArgumentNullException.ThrowIfNull(downloadUrl);

        if (string.IsNullOrWhiteSpace(downloadUrl))
            throw new ArgumentException("Ссылка для скачивания обязательна.", nameof(downloadUrl));

        if (fileSizeMb <= 0)
            throw new ArgumentOutOfRangeException(nameof(fileSizeMb), "Размер файла должен быть больше нуля.");

        DownloadUrl = downloadUrl.Trim();
        FileSizeMb = fileSizeMb;
        Console.WriteLine("2. Конструктор DigitalProduct");
    }

    public string DownloadUrl { get; }

    public int FileSizeMb { get; }

    public string GetFullInfo() =>
        $"{Title}: {base.GetDescription()}; файл {FileSizeMb} МБ; ссылка: {DownloadUrl}";
}
