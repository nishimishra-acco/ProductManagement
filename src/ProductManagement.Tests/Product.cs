using ProductManagement.Data.Domain;
using ProductManagement.Data.DTO;


namespace ProductManagement.Tests
{
    public class Product
    {
        public readonly Guid productId = Guid.Parse("a834dee5-d026-4042-b426-db087e55c38f");
        public List<ProductRecord> ProductRecordList()
        {
            return new List<ProductRecord>
            {
                new ProductRecord { Id = new Guid("8279e90d-b9e5-478e-b499-904bb43d38e5"), Name = "Laptop", Price = 2, StockQuantity = 5 },
                new ProductRecord { Id = new Guid("a834dee5-d026-4042-b426-db087e55c38f"), Name = "Mouse", Price = 2, StockQuantity = 5 }
            };

        }
        public List<ProductDto> ProductDto()
        {
            return new List<ProductDto>
            {
                new ProductDto { Id = new Guid("8279e90d-b9e5-478e-b499-904bb43d38e5"), Name = "Laptop", Price = 2, StockQuantity = 5 },
                new ProductDto { Id = new Guid("a834dee5-d026-4042-b426-db087e55c38f"), Name = "Mouse",  Price = 2, StockQuantity = 5 }
            };
        }
    }
}
