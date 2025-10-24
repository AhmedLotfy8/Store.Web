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

        public ProductsWithBrandAndTypeSpecifications(int? brandId, int? typeId, string? sort) : 
            
            base(p =>
            
            (!brandId.HasValue || p.BrandId == brandId)
            &&
            (!typeId.HasValue || p.TypeId == typeId)) {

            ApplySorting(sort);
            ApplyIncludes();

        }


        private void ApplySorting(string? sort) {

            if (!string.IsNullOrEmpty(sort)) {

                switch (sort.ToLower()) {

                    case "priceasc":
                        AddOrderBy(p => p.Price);
                        break;

                    case "pricedesc":
                        AddOrderByDescending(p => p.Price);
                        break;

                    default:
                        AddOrderBy(p => p.Name);
                        break;

                }
            }

            else {
                AddOrderBy(p => p.Name);
            }

        }

        private void ApplyIncludes() {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Type);
        }

    }
}
