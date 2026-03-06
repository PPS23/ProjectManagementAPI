using EFCoreAPI.Entities.DTOs.Tasks;
using EFCoreAPI.Repositories.Interfaces;
using EFCoreAPI.Repositories.Models;
using EFCoreAPI.Services.Interfaces;
using ProjectManagementAPI.Entities;

namespace EFCoreAPI.Services
{
    public class TaskUserRelationService : ITaskUserRelationService
    {
        private readonly ITaskUserRelationRepository taskUserRelationRepository;
        private readonly IUserRepository userRepository;
        public TaskUserRelationService(ITaskUserRelationRepository _taskUserRelationRepository, IUserRepository _userRepository) 
        {
            taskUserRelationRepository = _taskUserRelationRepository;
            userRepository = _userRepository;
        }

        public async Task Add(TaskUserRelationRequestDto dto)
        {
            var userModel = await userRepository.GetById(dto.UserId, false);
            var taskUserRelationModel = new TaskUserRelation() 
            {
                TaskId = dto.TaskId,
                UserId = dto.UserId,
                User = userModel
            };

            await taskUserRelationRepository.Add(taskUserRelationModel);
        }

        public async Task Delete(int id)
        {
            if (id > 0)
            {
                var taskModel = await taskUserRelationRepository.GetById(id, false);
                if (taskModel != null)
                {
                    await taskUserRelationRepository.Delete(taskModel.Id);
                }
                else
                {
                    throw new Exception("Id is required.");
                }
            }
        }

        public async Task<List<TaskUserRelationResponseDto>> GetAll(bool includeRelations)
        {
            var tasksRelation = await taskUserRelationRepository.GetAll(includeRelations);
            return tasksRelation.Select(x => new TaskUserRelationResponseDto() 
            {
                Id = x.Id,
                TaskId = x.TaskId,
                UserId = x.UserId
            }).ToList();
        }

        public async Task<TaskUserRelationResponseDto> GetById(int id, bool includeRelations)
        {
            if (id > 0)
            {
                var tasksRelation = await taskUserRelationRepository.GetById(id, false);
                if (tasksRelation != null)
                {
                    return new TaskUserRelationResponseDto()
                    {
                        Id = tasksRelation.Id,
                        TaskId = tasksRelation.TaskId,
                        UserId = tasksRelation.UserId
                    };
                }
                else
                {
                    throw new Exception("Relation doesn't find.");
                }
            }
            else
            {
                throw new Exception("Id is required.");
            }
        }

        public Task<PagedResult<TaskUserRelationResponseDto>> GetPaged(bool includeRelations, int page = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public async Task Update(int id, TaskUserRelationRequestDto model)
        {
            if (id > 0)
            {
                var tasksRelation = await taskUserRelationRepository.GetById(id, false);
                if (tasksRelation != null)
                {
                    tasksRelation.UserId = model.UserId;
                    tasksRelation.TaskId = model.TaskId;
                    await taskUserRelationRepository.Update(tasksRelation);
                }
                else
                {
                    throw new Exception("Relation doesn't find.");
                }
            }
            else
            {
                throw new Exception("Id is required.");
            }
        }
    }
}
