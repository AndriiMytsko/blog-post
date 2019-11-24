using AutoMapper;
using BlogPost.Bll.DTOs;
using BlogPost.Web.Models.Blogs;
using BlogPost.Web.Models.Comments;

namespace BlogPost.Web.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BlogDto, BlogViewModel>().ReverseMap();
            CreateMap<BlogDto, CreateBlogViewModel>().ReverseMap();
            CreateMap<BlogDto, UpdateBlogViewModel>().ReverseMap();
            CreateMap<CommentDto, CreateCommentViewModel>().ReverseMap();
            CreateMap<CommentDto, CommentViewModel>().ReverseMap();
            CreateMap<CommentDto, UpdateCommentViewModel>().ReverseMap();
        }
    }
}
