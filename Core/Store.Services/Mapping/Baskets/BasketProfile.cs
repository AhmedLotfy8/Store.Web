using AutoMapper;
using Store.Domain.Entities.Baskets;
using Store.Shared.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Mapping.Baskets {
    public class BasketProfile : Profile {


        public BasketProfile() {
            CreateMap<CustomerBasket, BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
        }

    }
}
