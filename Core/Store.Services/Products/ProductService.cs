using AutoMapper;
using Store.Domain.Contracts;
using Store.Domain.Entities.Products;
using Store.Services.Abstractions.Products;
using Store.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Products {
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService {


        public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync() {

            var products = await _unitOfWork.GetRepository<int, Product>().GetAllAsync();
            var result = _mapper.Map<IEnumerable<ProductResponse>>(products);

            return result;

        }

        public async Task<ProductResponse> GetProductByIdAsync(int id) {

            var product = await _unitOfWork.GetRepository<int, Product>().GetAsync(id);
            var result = _mapper.Map<ProductResponse>(product);

            return result;
        }

        public async Task<IEnumerable<BrandTypeRespone>> GetAllBrandsAsync() {
            var brands = await _unitOfWork.GetRepository<int, ProductBrand>().GetAllAsync();
            var result = _mapper.Map<IEnumerable<BrandTypeRespone>>(brands);

            return result;
        }

        public async Task<IEnumerable<BrandTypeRespone>> GetAllTypesAsync() {
            var types = await _unitOfWork.GetRepository<int, ProductType>().GetAllAsync();
            var result = _mapper.Map<IEnumerable<BrandTypeRespone>>(types);

            return result;
        }


    }
}
