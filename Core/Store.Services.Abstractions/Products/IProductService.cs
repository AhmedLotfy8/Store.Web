using Store.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Abstractions.Products {
    public interface IProductService {

        Task<IEnumerable<ProductResponse>> GetAllProductsAsync(int? brandId, int? typeId, string? sort, string? search, int? pageIndex, int? pageSize);
    
        Task<ProductResponse> GetProductByIdAsync(int id);

        Task<IEnumerable<BrandTypeRespone>> GetAllBrandsAsync();

        Task<IEnumerable<BrandTypeRespone>> GetAllTypesAsync();

    }
}
