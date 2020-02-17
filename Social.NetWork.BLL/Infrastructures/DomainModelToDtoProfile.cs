using AutoMapper;
using Social.NetWork.BLL.DTO;
using Social.NetWork.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.NetWork.BLL.Infrastructures {
    public class DomainModelToDtoProfile :Profile{
        public DomainModelToDtoProfile() {
            CreateMap<ApplicationUser, UserDTO>();
            CreateMap<UserProfile, UserDTO>();
            CreateMap<UserProfile, FriendDTO>();
            CreateMap<UserDTO, ApplicationUser>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src =>src.Email));
            CreateMap<Friend, FriendDTO>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest=>dest.FriendID,opt=>opt.MapFrom(src=>src.IdFriend))
                .ForMember(dest => dest.Role, opt => opt.Ignore());
            

            CreateMap<UserDTO, Friend>()
                .ForMember(dest=>dest.Id,opt=>opt.Ignore())
                .ForMember(dest=>dest.IdFriend,opt=>opt.MapFrom(src=>src.Id));
            CreateMap<UserFriendDTO, UserFriend>();
            CreateMap<Message, MessageDTO>()
                .ForMember(dest=>dest.UserPhoto,opt=>opt.MapFrom(src=>src.UserPhoto))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));
            CreateMap<UserDTO, UserProfile>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.BirthDate, opt => opt.Condition(src => src.BirthDate != null)) // TODO: fix birthDate problem
                    .ForMember(dest => dest.UserPhoto, opt => opt.Condition(src => src.UserPhoto != null));
            
        }
        
    }
}
