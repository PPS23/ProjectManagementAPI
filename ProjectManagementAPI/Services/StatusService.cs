using EFCoreAPI.Entities.DTOs.Status;
using EFCoreAPI.Repositories.Interfaces;
using EFCoreAPI.Repositories.Models;
using EFCoreAPI.Services.Interfaces;

namespace EFCoreAPI.Services
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository statusRepository;
        public StatusService(IStatusRepository _statusRepository) 
        {
            statusRepository = _statusRepository;
        }

        public async Task Add(StatusRequestDto model)
        {
            if (model != null)
            {
                if (string.IsNullOrEmpty(model.Description))
                {
                    throw new Exception("Description is required.");    
                }

                Status status = new Status();
                status.Id = model.Id;
                status.Description = model.Description;
                status.IsActive = true;
                await statusRepository.Add(status);
            }
            else
            {
                throw new Exception("All fields are required.");
            }
        }

        public async Task Delete(int id)
        {
            var statusModel = await statusRepository.GetById(id, false);
            if (statusModel != null)
            {
                statusModel.IsActive = false;
                await statusRepository.Update(statusModel);
            }
            else
            {
                throw new Exception("Status not found.");
            }
        }


        public async Task<List<StatusResponseDto>> GetAll(bool includeRelations)
        {
            var statuses = await statusRepository.GetAll(includeRelations);

            return statuses.Select(x => new StatusResponseDto
            {
                Id = x.Id,
                Description = x.Description,
                IsActive = x.IsActive
            }).ToList();
        }


        public async Task<StatusResponseDto> GetById(int id, bool includeRelations)
        {
            var status = await statusRepository.GetById(id, includeRelations);

            return new StatusResponseDto()
            {
                Id = status.Id,
                Description = status.Description,
                IsActive = status.IsActive
            };
        }

        public async Task Update(int id, StatusRequestDto model)
        {
            if (id > 0)
            {
                var statusModel = await statusRepository.GetById(id, false);
                if (statusModel != null)
                {
                    if (model != null)
                    {
                        if (string.IsNullOrEmpty(model.Description))
                        {
                            throw new Exception("Description is required.");
                        }

                        statusModel.Description = model.Description;
                        await statusRepository.Update(statusModel);
                    }
                    else
                    {
                        throw new Exception("This status information doesn't exists.");
                    }
                }
                else
                {
                    throw new Exception("This status information doesn't exists.");
                }
            }
        }
    }
}
