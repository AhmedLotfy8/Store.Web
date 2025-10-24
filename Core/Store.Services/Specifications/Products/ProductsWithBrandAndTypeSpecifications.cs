using Store.Domain.Contracts;
using Store.Domain.Entities;
using Store.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Specifications.Products {
    public class ProductsWithBrandAndTypeSpecifications : BaseSpecifications<int, Product> {


        public ProductsWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id) {
            ApplyIncludes();
        }

        public ProductsWithBrandAndTypeSpecifications(int? brandId, int? typeId) : base(
            p => 
            (!brandId.HasValue || p.BrandId == brandId) 
            && 
            (!typeId.HasValue  || p.TypeId == typeId) ) {
            
            ApplyIncludes();

        }


        private void ApplyIncludes() {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Type);
        }

    }
}
