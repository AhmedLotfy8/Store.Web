using AutoMapper;
using Store.Domain.Contracts;
using Store.Services.Abstractions;
using Store.Services.Abstractions.Baskets;
using Store.Services.Abstractions.Cache;
using Store.Services.Abstractions.Products;
using Store.Services.Baskets;
using Store.Services.Cache;
using Store.Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services {
    public class ServiceManager(
        IUnitOfWork _unitOfWork,
        IMapper _mapper,
        IBasketRepository _basketRepository,
        ICacheRepository _cacheRepository) : IServiceManager {
        public IProductService ProductService { get; } = new ProductService(_unitOfWork, _mapper);
        
        public IBasketService BasketService { get; } = new BasketService(_basketRepository, _mapper);

        public ICacheService CacheService { get; } = new CacheService(_cacheRepository);


    }
}
