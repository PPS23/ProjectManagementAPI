using EFCoreAPI.Entities.DTOs;
using EFCoreAPI.Entities.DTOs.Projects;
using EFCoreAPI.Entities.DTOs.Users;
using EFCoreAPI.Repositories.Models;
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
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] UserRequestDto user)
        {
            ResponseDto dataResponse = new ResponseDto();
            try
            {
                await userService.Add(user);
                dataResponse.Data = null;
                dataResponse.Success = true;
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseDto dataResponse = new ResponseDto();
            try 
            {
                await userService.Delete(id);
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserRequestDto model)
        {
            ResponseDto dataResponse = new ResponseDto();
            try
            {
                await userService.Update(id, model);
                dataResponse.Message = "User updated.";
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

        [HttpGet("GetPaged")]
        public async Task<IActionResult> GetPaged(int page = 1, int pageSize = 10)
        {
            ResponseDto dataResponse = new ResponseDto();
            try
            {
                dataResponse.Data = await userService.GetPaged(true, page, pageSize);
                dataResponse.Success = true;
                dataResponse.Message = "All Paged Users";
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
                dataResponse.Data = await userService.GetAll(true);
                dataResponse.Success = true;
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
                var project = await userService.GetById(id, true);
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

        [AllowAnonymous]
        [HttpPost("GetByDomainNameAndPassword")]
        public async Task<IActionResult> GetByDomainNameAndPassword([FromBody] LoginDto loginDto)
        {
            ResponseDto dataResponse = new ResponseDto();
            try
            {
                var userDto = await userService.GetLoginByDomainNameAndPassword(loginDto);
                if (userDto == null) throw new Exception("User or Password incorrect.");

                dataResponse.Success = true;
                dataResponse.Data = userDto;
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


    }
}
