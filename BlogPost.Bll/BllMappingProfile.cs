using AutoMapper;
using BlogPost.Bll.DTOs;
using BlogPost.Dal.Entities;

namespace BlogPost.Bll
{
    public class BllMappingProfile : Profile
    {
        public BllMappingProfile()
        {
            CreateMap<BlogDto, BlogEntity>().ReverseMap();
            CreateMap<CommentDto, CommentEntity>().ReverseMap();
        }
    }
}