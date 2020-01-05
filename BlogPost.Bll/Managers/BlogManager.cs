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
    public class BlogManager : IBlogManager
    {
        private readonly IMapper _mapper;
        private readonly IBlogRepository _blogRepository;

        public BlogManager(
          IMapper mapper,
          IBlogRepository blogRepository)
        {
            _mapper = mapper;
            _blogRepository = blogRepository;
        }

        public async Task CreateBlog(BlogDto dto)
        {
            var entity = _mapper.Map<BlogEntity>(dto);
            entity.CreatedAt = DateTime.UtcNow;

            await _blogRepository.AddAsync(entity);
        }

        public async Task<BlogDto> GetBlog(int id)
        {
            var entity = await _blogRepository.GetAsync(id);
            var blog = _mapper.Map<BlogDto>(entity);
            
            return blog;
        }

        public async Task<IList<BlogDto>> GetAllBlogs()
        {
            var entities = await _blogRepository.GetAsync();
            var blogs = _mapper.Map<IList<BlogDto>>(entities);

            return blogs;
        }

        public async Task UpdateBlog(BlogDto dto)
        {
            var dbEntity = await _blogRepository.GetAsync(dto.Id);

            dbEntity.Title = dto.Title;
            dbEntity.UpdatedAt = DateTime.UtcNow;

            await _blogRepository.UpdateAsync(dbEntity);
        }

        public async Task DeleteBlog(int id)
        {
            var blogEntity = await _blogRepository.GetAsync(id);

            if (blogEntity == null)
            {
                throw new NotFoundException($"blog by id {id} doesn't exist");
            }

            await _blogRepository.RemoveAsync(id);
        }
    }
}