using ProductManagement.Data.Domain;
using ProductManagement.Data.DTO;


namespace ProductManagement.Tests
{
    public static class Product
    {
        public static readonly Guid productId = Guid.Parse("a834dee5-d026-4042-b426-db087e55c38f");

        public static List<ProductRecord> ProductRecordList = new List<ProductRecord>
        {
            new ProductRecord { Id = new Guid("8279e90d-b9e5-478e-b499-904bb43d38e5"), Name = "Laptop", Price = 2, StockQuantity = 5 },
            new ProductRecord { Id = new Guid("a834dee5-d026-4042-b426-db087e55c38f"), Name = "Mouse", Price = 2, StockQuantity = 5 },
            new ProductRecord { Id = new Guid("a834dee5-d026-4042-b426-db087e55c38f"), Name = "NonExistentProduct", Price = 2, StockQuantity = 5  },
            new ProductRecord { Id = new Guid("8279e90d-b9e5-478e-b499-904bb43d38e5"), Name = "Updated Laptop", Price = 2, StockQuantity = 5 },
        };

        public static List<ProductDto> ProductDtoList = new List<ProductDto>
        {
            new ProductDto { Id = new Guid("8279e90d-b9e5-478e-b499-904bb43d38e5"), Name = "Laptop", Price = 2, StockQuantity = 5 },
            new ProductDto { Id = new Guid("a834dee5-d026-4042-b426-db087e55c38f"), Name = "Mouse",  Price = 2, StockQuantity = 5 },
            new ProductDto { Id = new Guid("a834dee5-d026-4042-b426-db087e55c38f"), Name = "Valid Product",  Price = 2, StockQuantity = 5 },
            new ProductDto { Id = new Guid("a834dee5-d026-4042-b426-db087e55c38f"), Name = string.Empty,  Price = 10.0m, StockQuantity = 5 },
            new ProductDto { Id = new Guid("8279e90d-b9e5-478e-b499-904bb43d38e5"), Name = "CPU",  Price = 0.5m, StockQuantity = 5 },
            new ProductDto { Id = new Guid("8279e90d-b9e5-478e-b499-904bb43d38e5"), Name = "Harddisk",  Price = 10.5m, StockQuantity = 0 }
        };
    }
}
