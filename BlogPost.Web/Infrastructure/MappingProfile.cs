using AutoMapper;
using BlogPost.Bll.DTOs;
using BlogPost.Web.Models.Blogs;
using BlogPost.Web.Models.Posts;
using BlogPost.Web.Models.Comments;
using BlogPost.Web.Models.Account;
using Microsoft.AspNetCore.Http;
using BlogPost.Web.Infrastructure.Extensions;
using BlogPost.Web.Models;

namespace BlogPost.Web.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BlogDto, BlogViewModel>();
            CreateMap<BlogDto, CreateBlogViewModel>().ReverseMap();
            CreateMap<BlogDto, UpdateBlogViewModel>().ReverseMap();

            CreateMap<CommentDto, CommentViewModel>().ReverseMap();
            CreateMap<CommentDto, CreateCommentViewModel>().ReverseMap();
            CreateMap<CommentDto, UpdateCommentViewModel>().ReverseMap();

            CreateMap<PostDto, PostViewModel>().ReverseMap();
            CreateMap<PostDto , CreatePostViewModel>().ReverseMap();
            CreateMap<PostDto, UpdatePostViewModel>().ReverseMap();
            CreateMap<PostDto, PostDetailsViewModel>().ReverseMap();

            CreateMap<UserDto, LoginViewModel>();
            CreateMap<UserDto, RegisterViewModel>().ReverseMap();
            CreateMap<UserDto, UserDetailsViewModel>().ReverseMap();
            CreateMap<UserDto, UserNameViewModel>().ReverseMap();

            CreateMap<IFormFile, ImageDto>()
                .ForMember(dest => dest.Content, opt => opt.MapFrom(x => x.ToByteArray()))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(x => x.ContentType));

            CreateMap<ImageDto, ImageModel>().ReverseMap();
        }
    }
}
