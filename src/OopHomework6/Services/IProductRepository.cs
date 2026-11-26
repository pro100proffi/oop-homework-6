using Marketplace.Domain;

namespace Marketplace.Services;

public interface IProductRepository
{
    void Save(IEnumerable<Product> products);

    List<Product> Load();
}

