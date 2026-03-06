using EFCoreAPI.Entities.DTOs.Comments;
using EFCoreAPI.Entities.DTOs.Projects;
using EFCoreAPI.Entities.DTOs.Status;
using EFCoreAPI.Entities.DTOs.Tasks;
using EFCoreAPI.Entities.DTOs.Users;
using EFCoreAPI.Repositories.Interfaces;
using EFCoreAPI.Repositories.Models;
using EFCoreAPI.Services.Interfaces;
using ProjectManagementAPI.Entities;
using System.Threading.Tasks;

namespace EFCoreAPI.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository taskRepository;
        private readonly IStatusRepository statusRepository;
        private readonly ITaskUserRelationRepository taskUserRelationRepository;

        public TaskService(ITaskRepository _taskRepository, IStatusRepository _statusRepository, ITaskUserRelationRepository _taskUserRelationRepository) 
        {
            taskRepository = _taskRepository;
            statusRepository = _statusRepository;
            taskUserRelationRepository = _taskUserRelationRepository;
        }

        public async Task Add(TaskRequestDto task)
        {
            if (task == null) throw new Exception("All Tasks fields are required.");
            if (task.ProjectId == 0) throw new Exception("Project is required.");
            if (string.IsNullOrEmpty(task.Title)) throw new Exception("Title is required.");
            if (string.IsNullOrEmpty(task.Description)) throw new Exception("Description is required.");

            string statusDescription = string.Empty;
            if (task.ResponsableUsers.Count > 0)
            {
                statusDescription = "Not Started";
            }
            else
            {
                statusDescription = "Not Assigned";
            }
            
            var statusModel = await statusRepository.GetByDescription(statusDescription);
            if (statusModel == null) statusModel = new Status() { Id = 1 };

            var model = new TaskModel() 
            {
                ProjectId = task.ProjectId,
                StatusId = statusModel.Id,
                Title = task.Title,
                Description = task.Description,
                CreatedDate = DateTime.Now,
                IsActive = true
            };

            await taskRepository.Add(model);

            if (task.ResponsableUsers.Count > 0)
            {
                foreach (var user in task.ResponsableUsers)
                {
                    var taskUserRelationModel = new TaskUserRelation()
                    {
                        TaskId = model.Id,
                        UserId = user.UserId
                    };

                    await taskUserRelationRepository.Add(taskUserRelationModel);
                }
            }
        }

        public async Task Delete(int id)
        {
            if (id > 0)
            {
                var taskModel = await taskRepository.GetById(id, false);
                if (taskModel != null)
                {
                    taskModel.IsActive = false;
                    await taskRepository.Update(taskModel);
                }
                else
                {
                    throw new Exception("Id is required.");
                }
            }
        }

        public async Task<List<TaskResponseDto>> GetAll(bool includeRelations)
        {
            var tasks = await taskRepository.GetAll(includeRelations);
            var tasksDto = tasks.Select(t => new TaskResponseDto
            {
                Id = t.Id,
                ProjectId = t.ProjectId,
                Project = new ProjectResponseDto()
                {
                    Id = t.Project.Id,
                    Name = t.Project.Name,
                    CreatedDate = t.Project.CreatedDate
                },
                Status = new StatusResponseDto()
                {
                    Id = t.Status.Id,
                    Description = t.Status.Description,
                    IsActive = t.Status.IsActive
                },
                CreatedDate = t.CreatedDate,
                Title = t.Title,
                Description = t.Description,
                TaskUsers = t.TaskUsers.Select(u => new UserResponseDto()
                {
                    Id = u.User.Id,
                    DomainName = u.User.DomainName,
                    Email = u.User.Email,
                    Person = null
                }).ToList()
            }).ToList();

            return tasksDto;
        }

        public async Task<TaskResponseDto> GetById(int id, bool includeRelations)
        {
            try
            {
                var task = await taskRepository.GetById(id, includeRelations);
                var taskDto = new TaskResponseDto
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    ProjectId = task.ProjectId,
                    CreatedDate = task.CreatedDate,
                    Status = new StatusResponseDto
                    {
                        Id = task.Status.Id,
                        Description = task.Status.Description
                    },
                    Project = new ProjectResponseDto
                    {
                        Id = task.ProjectId,
                        Name = task.Project.Name
                    },
                    TaskUsers = task.TaskUsers.Select(u => new UserResponseDto()
                    {
                        Id = u.Id,
                        DomainName = u.User.DomainName,
                        Email = u.User.Email
                    }).ToList(),
                    Comments = task.Comments.Select(c => new CommentResponseDto
                        {
                            Id = c.Id,
                            Description = c.Description,
                            CreatedDate = c.CreatedDate,
                            User = new UserResponseDto
                            {
                                Id = c.User.Id,
                                DomainName = c.User.DomainName
                            }
                        }).ToList()
                };

                return taskDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PagedResult<TaskResponseDto>> GetPaged(bool includeRelations = true, int page = 1, int pageSize = 10)
        {
            var tasks = await taskRepository.GetAll(includeRelations);
            if (tasks != null)
            {
                int totalCount = tasks.Count;
                var items = tasks.OrderBy(x => x.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList().Select(t => new TaskResponseDto()
                    {
                        Id = t.Id,
                        ProjectId = t.ProjectId,
                        Project = new ProjectResponseDto()
                        {
                            Id = t.Project.Id,
                            Name = t.Project.Name,
                            CreatedDate = t.Project.CreatedDate
                        },
                        Status = new StatusResponseDto()
                        {
                            Id = t.Status.Id,
                            Description = t.Status.Description,
                            IsActive = t.Status.IsActive
                        },
                        CreatedDate = t.CreatedDate,
                        Title = t.Title,
                        Description = t.Description
                    });
                int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                return new PagedResult<TaskResponseDto>
                {
                    Items = items,
                    TotalCount = totalCount,
                    Page = page,
                    PageSize = pageSize,
                    TotalPages = totalPages
                };
            }
            else
            {
                throw new Exception("There are not Tasks.");
            }
            
        }

        public async Task Update(int id, TaskRequestDto model)
        {
            if (id > 0)
            {
                if (model != null)
                {
                    var taskModel = await taskRepository.GetById(id, false);
                    if (taskModel != null)
                    {
                        taskModel.ProjectId = model.ProjectId;
                        taskModel.StatusId = model.StatusId;
                        taskModel.Title = model.Title;
                        taskModel.Description = model.Description;
                        taskModel.UpdatedDate = DateTime.Now;
                        taskModel.IsActive = false;
                        await taskRepository.Update(taskModel);
                    }
                    else
                    {
                        throw new Exception("Task doesn't find.");
                    }
                }
                else
                {
                    throw new Exception("All the fields are required.");
                }
            }
            else
            {
                throw new Exception("Task Id is required.");
            }
        }

    }
}
