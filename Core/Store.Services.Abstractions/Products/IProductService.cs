using Store.Shared;
using Store.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Abstractions.Products {
    public interface IProductService {

        Task<PaginationRespone<ProductResponse>> GetAllProductsAsync(ProductQueryParameters parameters);
    
        Task<ProductResponse> GetProductByIdAsync(int id);

        Task<IEnumerable<BrandTypeRespone>> GetAllBrandsAsync();

        Task<IEnumerable<BrandTypeRespone>> GetAllTypesAsync();

    }
}
