using AutoMapper;
using BlogPost.Bll.DTOs;
using BlogPost.Dal.Entities;
using BlogPost.Dal.Identities;

namespace BlogPost.Bll
{
    public class BllMappingProfile : Profile
    {
        public BllMappingProfile()
        {
            CreateMap<BlogDto, BlogEntity>().ReverseMap();
            CreateMap<CommentDto, CommentEntity>().ReverseMap();
            CreateMap<PostDto, PostEntity>().ReverseMap();
            CreateMap<UserDto, ApplicationUser>().ReverseMap();
        }
    }
}