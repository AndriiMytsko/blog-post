using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BlogPost.Bll.DTOs;
using BlogPost.Bll.Exceptions;
using BlogPost.Bll.Managers.Interfaces;
using BlogPost.Dal.Entities;
using BlogPost.Dal.Interfaces.Repositories;

namespace BlogPost.Bll.Managers
{
    public class PostManager : IPostManager
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;

        public PostManager(
          IMapper mapper,
          IPostRepository postRepository)
        {
            _mapper = mapper;
            _postRepository = postRepository;
        }

        public async Task CreatePost(PostDto dto)
        {
            var entity = _mapper.Map<PostEntity>(dto);
            entity.CreatedAt = DateTime.UtcNow;
            entity.BlogId = dto.BlogId;
            entity.UserId = dto.UserId;

            await _postRepository.AddAsync(entity);
        }

        public async Task<PostDto> GetPost(int id)
        {
            var entity = await _postRepository.GetAsync(id);
            var post = _mapper.Map<PostDto>(entity);

            return post;
        }

        public async Task<IList<PostDto>> GetAllPosts()
        {
            var entities = await _postRepository.GetAsync();
            var posts = _mapper.Map<IList<PostDto>>(entities);

            return posts;
        }

        public async Task UpdatePost(PostDto dto)
        {
            var entity = _mapper.Map<PostEntity>(dto);
            entity.UpdatedAt = DateTime.UtcNow;

            await _postRepository.UpdateAsync(entity);
        }

        public async Task DeletePost(int id)
        {
            var entity = await _postRepository.GetAsync(id);

            if (entity == null)
            {
                throw new NotFoundException($"post by id {id} doesn't exist");
            }

            await _postRepository.RemoveAsync(id);
        }

        public async Task<PostDto> GetPostWithComments(int id)
        {
            var entity = await _postRepository.GetPostWithCommentsAsync(id);
            var post = _mapper.Map<PostDto>(entity);

            return post;
        }

        public async Task<IList<PostDto>> GetPostsWithUsers()
        {
            var entity = await _postRepository.GetPostsWithUsersAsync();
            var posts = _mapper.Map<IList<PostDto>>(entity);

            return posts;
        }

    }
}
