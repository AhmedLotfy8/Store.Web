using AutoMapper;
using Store.Domain.Contracts;
using Store.Domain.Entities.Products;
using Store.Services.Abstractions.Products;
using Store.Services.Specifications;
using Store.Services.Specifications.Products;
using Store.Shared;
using Store.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Products {
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService {


        public async Task<PaginationRespone<ProductResponse>> GetAllProductsAsync(ProductQueryParameters parameters) {

            //var spec = new BaseSpecifications<int, Product>(null);
            //spec.Includes.Add(p => p.Brand);
            //spec.Includes.Add(p => p.Type);

            var spec = new ProductsWithBrandAndTypeSpecifications(parameters);

            var products = await _unitOfWork.GetRepository<int, Product>().GetAllAsync(spec);
            
            var result = _mapper.Map<IEnumerable<ProductResponse>>(products);


            var specCount = new ProductsCountSpecifications(parameters);

            var count = await _unitOfWork.GetRepository<int,Product>().CountAsync(specCount);

            return new PaginationRespone<ProductResponse>(parameters.PageIndex, parameters.PageSize, count, result);

        }


        public async Task<ProductResponse> GetProductByIdAsync(int id) {

            var spec = new ProductsWithBrandAndTypeSpecifications(id);

            var product = await _unitOfWork.GetRepository<int, Product>().GetAsync(spec);
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
