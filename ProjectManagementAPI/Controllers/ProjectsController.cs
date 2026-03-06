using EFCoreAPI.Entities.DTOs;
using EFCoreAPI.Entities.DTOs.Projects;
using EFCoreAPI.Entities.DTOs.Status;
using EFCoreAPI.Services;
using EFCoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace EFCoreAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService projectService;
        public ProjectsController(IProjectService _projectService)
        {
            projectService = _projectService;
        }

        [HttpGet("GetPaged")]
        public async Task<IActionResult> GetPaged(int page = 1, int pageSize = 10)
        {
            ResponseDto dataResponse = new ResponseDto();
            try
            {
                dataResponse.Data = await projectService.GetPaged(false, page, pageSize);
                dataResponse.Success = true;
                dataResponse.Message = "All Paged Projects";
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

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            ResponseDto dataResponse = new ResponseDto();
            try
            {
                var projects = await projectService.GetAll(false);
                dataResponse.Success = true;
                dataResponse.Data = projects;
                dataResponse.Message = "Correct";
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
        public async Task<IActionResult> GetById(int id)
        {
            ResponseDto dataResponse = new ResponseDto();
            try
            {
                var project = await projectService.GetById(id, false);
                dataResponse.Success = true;
                dataResponse.Data = project;
                dataResponse.Message = "Correct";
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

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] ProjectRequestDto model)
        {
            ResponseDto dataResponse = new ResponseDto();
            try
            {
                await projectService.Add(model);
                dataResponse.Success = true;
                dataResponse.Message = "Project added";
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
        public async Task<IActionResult> Update(int id, [FromBody] ProjectRequestDto model)
        {
            ResponseDto dataResponse = new ResponseDto();
            try
            {
                await projectService.Update(id, model);
                dataResponse.Message = "Project updated.";
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
                await projectService.Delete(id);
                dataResponse.Success = true;
                dataResponse.Message = "Project deleted.";
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
