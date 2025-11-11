using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Store.Domain.Contracts;
using Store.Domain.Entities.Identity;
using Store.Services.Abstractions;
using Store.Services.Abstractions.Auth;
using Store.Services.Abstractions.Baskets;
using Store.Services.Abstractions.Cache;
using Store.Services.Abstractions.Orders;
using Store.Services.Abstractions.Products;
using Store.Services.Auth;
using Store.Services.Baskets;
using Store.Services.Cache;
using Store.Services.Orders;
using Store.Services.Products;
using Store.Shared;
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
        ICacheRepository _cacheRepository,
        UserManager<AppUser> _userManager,
        IOptions<JwtOptions> options) : IServiceManager {
        public IProductService ProductService { get; } = new ProductService(_unitOfWork, _mapper);
        
        public IBasketService BasketService { get; } = new BasketService(_basketRepository, _mapper);

        public ICacheService CacheService { get; } = new CacheService(_cacheRepository);

        public IAuthService AuthService { get; } = new AuthService(_userManager, options, _mapper);

        public IOrderService OrderService { get; } = new OrderService(_unitOfWork, _mapper, _basketRepository);
    }
}
