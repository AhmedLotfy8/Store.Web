using Store.Domain.Entities.Products;
using Store.Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Specifications.Products {
    public class ProductsCountSpecifications : BaseSpecifications<int, Product> {

        public ProductsCountSpecifications(ProductQueryParameters parameters) : base(

            p =>
            (!parameters.BrandId.HasValue || p.BrandId == parameters.BrandId)
            &&
            (!parameters.TypeId.HasValue || p.TypeId == parameters.TypeId)
            &&
            (string.IsNullOrEmpty(parameters.Search) || p.Name.ToLower().Contains(parameters.Search.ToLower()))


            ) {




        }

    }
}
