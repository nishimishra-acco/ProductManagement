using ProductManagement.Data.Domain;
using ProductManagement.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Data.Mappers
{
    public interface IProductMapper
    {
        ProductDto ToProductDTO(ProductRecord productRecord);
        ProductRecord ToProductRecord(ProductDto productDto);
    }
}
