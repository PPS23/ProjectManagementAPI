using EFCoreAPI.Entities.DTOs.Projects;
using EFCoreAPI.Repositories.Interfaces;
using EFCoreAPI.Repositories.Models;
using EFCoreAPI.Services.Interfaces;

namespace EFCoreAPI.Services
{
    public class ProjectService: IProjectService
    {
        private readonly IProjectRepository projectRepository;
        public ProjectService(IProjectRepository _projectRepository) 
        {
            projectRepository = _projectRepository;
        }

        public async Task Add(ProjectRequestDto model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new Exception("Project Name is required.");
            }

            Project projectModel = new Project();
            projectModel.Name = model.Name;
            projectModel.CreatedDate = DateTime.Now;
            projectModel.IsActive = true;
            await projectRepository.Add(projectModel);
        }

        public async Task Delete(int id)
        {
            var projectModel = await projectRepository.GetById(id, false);
            if (projectModel != null)
            {
                projectModel.IsActive = false;
                await projectRepository.Update(projectModel);
            }
        }

        public async Task<List<ProjectResponseDto>> GetAll(bool includeRelations)
        {
            List<ProjectResponseDto> projectsList = new List<ProjectResponseDto>();
            var projectModel = await projectRepository.GetAll(includeRelations);
            if (projectModel != null)
            {
                projectsList = projectModel.Select(x => new ProjectResponseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedDate = DateTime.Now,
                    IsActive = x.IsActive
                }).ToList();
            }

            return projectsList;
        }

        public async Task<ProjectResponseDto> GetById(int id, bool includeRelations)
        {
            if (id > 0)
            {
                var projectModel = await projectRepository.GetById(id, includeRelations);
                if (projectModel != null)
                {
                    return new ProjectResponseDto
                    {
                        Id = projectModel.Id,
                        Name = projectModel.Name,
                        CreatedDate = projectModel.CreatedDate,
                        IsActive = projectModel.IsActive
                    };
                }
                else
                {
                    throw new Exception("Project not found.");
                }
            }
            else
            {
                throw new Exception("Project Id is required.");
            }
        }

        public async Task Update(int id, ProjectRequestDto model)
        {
            if (id > 0)
            {
                if (!string.IsNullOrEmpty(model.Name))
                {
                    var projectModel = await projectRepository.GetById(id, false);
                    if (projectModel != null)
                    {
                        projectModel.Name = model.Name;
                        projectModel.IsActive = model.IsActive;
                        await projectRepository.Update(projectModel);
                    }
                    else
                    {
                        throw new Exception("This project can't be updated, because was not found.");
                    }
                }
                else
                {
                    
                }
            }
            else
            {
                throw new Exception("This project can't be updated, because Project Id is required.");
            }
        }
    }
}
