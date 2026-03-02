using EFCoreAPI.Entities.DTOs.Comments;
using EFCoreAPI.Repositories.Interfaces;
using EFCoreAPI.Repositories.Models;
using EFCoreAPI.Services.Interfaces;
using System.Runtime.CompilerServices;

namespace EFCoreAPI.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository commentRepository;
        public CommentService(ICommentRepository _commentRepository) 
        {
            commentRepository = _commentRepository;
        }

        public async Task Add(CommentRequestDto model)
        {
            if (model != null)
            {
                if (!string.IsNullOrEmpty(model.Description))
                {
                    if (model.TaskId > 0)
                    {
                        if (model.UserId > 0)
                        {
                            Comment commentModel = new Comment();
                            commentModel.UserId = model.UserId;
                            commentModel.TaskId = model.TaskId;
                            commentModel.Description = model.Description;
                            commentModel.CreatedDate = DateTime.Now;
                            commentModel.IsActive = true;
                            await commentRepository.Add(commentModel);
                        }
                    }
                }
            }
        }

        public async Task Delete(int id)
        {
            if (id > 0)
            {
                var commentary = await commentRepository.GetById(id, false);
                if (commentary != null)
                {
                    commentary.IsActive = false;
                    await commentRepository.Update(commentary);
                }
                else
                {
                    throw new Exception("Commentary doesn't found.");
                }
            }
            else
            {
                throw new Exception("Id is required.");
            }
        }

        public async Task<List<CommentResponseDto>> GetAll(bool includeRelations)
        {
            var comments = await commentRepository.GetAll(includeRelations);
            if (comments != null)
            {
                return comments.Select(x => new CommentResponseDto() 
                {
                    Id = x.Id,
                    TaskId = x.TaskId,
                    UserId = x.UserId,
                    Description = x.Description,
                    CreatedDate = x.CreatedDate,
                    User = new Entities.DTOs.Users.UserResponseDto() 
                    {
                        Id = x.User.Id,
                        DomainName = x.User.DomainName
                    }
                }).ToList();
            }
            else
            {
                throw new Exception("No comments found.");
            }
        }

        public async Task<CommentResponseDto> GetById(int id, bool includeRelations)
        {
            var commentModel = await commentRepository.GetById(id, includeRelations);
            if (commentModel != null)
            {
                return new CommentResponseDto
                {
                    Id = commentModel.Id,
                    Description = commentModel.Description,
                    CreatedDate = commentModel.CreatedDate,
                    User = new Entities.DTOs.Users.UserResponseDto
                    {
                        Id = commentModel.User.Id,
                        DomainName = commentModel.User.DomainName
                    }
                };
            }
            else
            {
                throw new Exception("Comment not found.");
            }
        }

        public async Task Update(int id, CommentRequestDto model)
        {
            if (id > 0)
            {
                var commentModel = await commentRepository.GetById(id, false);
                if (commentModel != null)
                {
                    commentModel.Description = model.Description;
                    commentModel.IsActive = model.IsActive;
                    commentModel.UserId = model.UserId;
                    commentModel.TaskId = model.TaskId;
                    await commentRepository.Update(commentModel);
                }
                else
                {
                    throw new Exception("Commentary doesn't found.");
                }
            }
            else
            {
                throw new Exception("Id is required.");
            }
        }
    }
}
