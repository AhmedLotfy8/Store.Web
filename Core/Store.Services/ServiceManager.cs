using AutoMapper;
using Store.Domain.Contracts;
using Store.Services.Abstractions;
using Store.Services.Abstractions.Products;
using Store.Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services {
    public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper) : IServiceManager {
        public IProductService ProductService { get;  } = new ProductService(_unitOfWork, _mapper);
    }
}
