using AutoMapper;
using Store.Domain.Entities.Identity;
using Store.Shared.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Mapping.Auth {
    public class AuthProfile : Profile {

        public AuthProfile() {
            CreateMap<Address, AddressDto>().ReverseMap();
        }


    }
}
