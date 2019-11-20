using AutoMapper;
using BlogPost.Bll.DTOs;
using BlogPost.Web.Models.Blogs;

namespace BlogPost.Web.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BlogDto, BlogViewModel>().ReverseMap();
            CreateMap<BlogDto, CreateBlogViewModel>().ReverseMap();
            CreateMap<BlogDto, UpdateBlogViewModel>().ReverseMap();
        }
    }
}
