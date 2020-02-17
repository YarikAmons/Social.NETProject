using AutoMapper;
using Social.NetWork.BLL.DTO;
using Social.NetWork.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Social.NetWork.WEB.App_Start {
    public class DtoToViewModelProfile :Profile{
        public DtoToViewModelProfile() {
            CreateMap<UserDTO, UserProfileModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FullName}"));
            //CreateMap<UserProfileModel, UserDTO>()
            //    .ForMember(dest=>dest.FullName,opt=>);
            //CreateMap
            CreateMap<LoginModel, UserDTO>();
            CreateMap<RegistrationModel, UserDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "user"));
            }
    }
}