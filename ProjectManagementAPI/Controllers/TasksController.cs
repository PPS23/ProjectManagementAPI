using EFCoreAPI.Entities.DTOs;
using EFCoreAPI.Entities.DTOs.Projects;
using EFCoreAPI.Entities.DTOs.Tasks;
using EFCoreAPI.Services;
using EFCoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService taskService;
        public TasksController(ITaskService _taskService)
        {
            taskService = _taskService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] bool includeRelations)
        {
            ResponseDto dataResponse = new ResponseDto();
            try
            {
                dataResponse.Data = await taskService.GetAll(includeRelations);
                dataResponse.Success = true;
                dataResponse.Message = "All Tasks";

                return Ok(dataResponse);
            }
            catch (Exception ex)
            {
                dataResponse.Data = null;
                dataResponse.Success = false;
                dataResponse.Message = ex.Message;
                return BadRequest(dataResponse);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id, bool includeRelations)
        {
            ResponseDto dataResponse = new ResponseDto();
            try
            {
                var taskInformation = await taskService.GetById(id, includeRelations);
                dataResponse.Success = true;
                dataResponse.Message = "Tasks Information";
                return Ok(taskInformation);
            }
            catch (Exception ex)
            {
                dataResponse.Data = null;
                dataResponse.Success = false;
                dataResponse.Message = ex.Message;
                return BadRequest(dataResponse);
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] TaskRequestDto dto)
        {
            ResponseDto dataResponse = new ResponseDto();
            try
            {
                await taskService.Add(dto);
                dataResponse.Success = true;
                dataResponse.Message = "Tasks Added.";
                dataResponse.Data = null;
                return Ok(dataResponse);
            }
            catch (Exception ex)
            {
                dataResponse.Data = null;
                dataResponse.Success = false;
                dataResponse.Message = ex.Message;
                return BadRequest(dataResponse);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TaskRequestDto model)
        {
            ResponseDto dataResponse = new ResponseDto();
            try
            {
                await taskService.Update(id, model);
                dataResponse.Message = "Task updated.";
                dataResponse.Success = true;
                return Ok(dataResponse);
            }
            catch (Exception ex)
            {
                dataResponse.Data = null;
                dataResponse.Success = false;
                dataResponse.Message = ex.Message;
                return BadRequest(dataResponse);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseDto dataResponse = new ResponseDto();
            try
            {
                await taskService.Delete(id);
                dataResponse.Success = true;
                dataResponse.Message = "Task deleted.";
                return Ok(dataResponse);
            }
            catch (Exception ex)
            {
                dataResponse.Data = null;
                dataResponse.Success = false;
                dataResponse.Message = ex.Message;
                return BadRequest(dataResponse);
            }
        }

    }
}

