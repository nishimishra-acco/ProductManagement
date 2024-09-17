using ProductManagement.Data.Domain;

namespace ProductManagement.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly List<ProductRecord> products = new List<ProductRecord>();

    public async Task<IEnumerable<ProductRecord>> GetAll()
    {
        return await Task.FromResult(products);
    }

    public async Task<ProductRecord> GetById(Guid id)
    {
        var product = products.FirstOrDefault(product => product.Id == id);
        if(product == null)
        {
           throw new InvalidOperationException("Product is not available.");
        }
        return await Task.FromResult(product);
    }

    public async Task<ProductRecord> Create(ProductRecord productRecord)
    {
        await Task.Run(() => products.Add(productRecord));
        return productRecord;
    }

    public async Task Update(ProductRecord productRecord)
    {
        int index = -1;
        await Task.Run(() =>
        {
            index = products.FindIndex(x => x.Id == productRecord.Id);
            if (index == -1)
            {
                throw new InvalidOperationException("Product is not available.");
            }
            products[index] = productRecord;
        });
    }
}
