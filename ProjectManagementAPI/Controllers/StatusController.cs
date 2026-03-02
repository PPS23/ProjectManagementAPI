using EFCoreAPI.Entities.DTOs;
using EFCoreAPI.Entities.DTOs.Status;
using EFCoreAPI.Entities.DTOs.Users;
using EFCoreAPI.Services;
using EFCoreAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static EFCoreAPI.Services.Interfaces.IPersonService;

namespace EFCoreAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService statusService;
        public StatusController(IStatusService _statusService)
        {
            statusService = _statusService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() 
        {
            ResponseDto dataResponse = new ResponseDto();
            try
            {
                dataResponse.Data = await statusService.GetAll(false);
                dataResponse.Success = true;
                dataResponse.Message = "All Status";

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
                dataResponse.Data = await statusService.GetById(id, false);
                dataResponse.Success = true;
                dataResponse.Message = "Status by id";

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
        public async Task<IActionResult> Add([FromBody] StatusRequestDto model)
        {
            ResponseDto dataResponse = new ResponseDto();
            try
            {
                await statusService.Add(model);
                dataResponse.Success = true;
                dataResponse.Message = "Status added";
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
        public async Task<IActionResult> PatchStatus(int id, [FromBody] StatusRequestDto model)
        {
            ResponseDto dataResponse = new ResponseDto();
            try
            {
                await statusService.Update(id, model);
                dataResponse.Message = "Status updated.";
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
                await statusService.Delete(id);
                dataResponse.Success = true;
                dataResponse.Message = "Status deleted.";
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
