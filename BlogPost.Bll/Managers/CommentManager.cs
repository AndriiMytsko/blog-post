using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogPost.Bll.Managers.Interfaces;
using BlogPost.Bll.Exceptions;
using BlogPost.Dal.Entities;
using BlogPost.Dal.Interfaces.Repositories;
using BlogPost.Bll.DTOs;
using AutoMapper;

namespace BlogPost.Bll.Managers
{
    public class CommentManager : ICommentManager
    {
        private readonly IMapper _mapper;
        private readonly ICommnentRepository _commnentRepository;

        public CommentManager(
          IMapper mapper,
          ICommnentRepository commnentRepository)
        {
            _mapper = mapper;
            _commnentRepository = commnentRepository;
        }
        public async Task CreateComment(CommentDto dto)
        {
            var entity = _mapper.Map<CommentEntity>(dto);
            entity.CreatedAt = DateTime.UtcNow;
            entity.PostId = dto.PostId;
            await _commnentRepository.AddAsync(entity);
        }

        public async Task<CommentDto> GetComment(int id)
        {
            var entity = await _commnentRepository.GetAsync(id);
            var comment = _mapper.Map<CommentDto>(entity);

            return comment;
        }

        public async Task<IList<CommentDto>> GetAllComments()
        {
            var entities = await _commnentRepository.GetAsync();
            var comments = _mapper.Map<IList<CommentDto>>(entities);

            return comments;
        }

        public async Task UpdateComment(CommentDto dto)
        {
            var entity = _mapper.Map<CommentEntity>(dto);
            await _commnentRepository.UpdateAsync(entity);
        }

        public async Task DeleteComment(int id)
        {
            var entity = await _commnentRepository.GetAsync(id);

            if (entity == null)
            {
                throw new NotFoundException($"comment by id {id} doesn't exist");
            }

            await _commnentRepository.RemoveAsync(id);
        }
    }
}
