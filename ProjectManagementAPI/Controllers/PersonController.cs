using EFCoreAPI.Entities.DTOs;
using EFCoreAPI.Entities.DTOs.Persons;
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
    public class PersonController : ControllerBase
    {
        private readonly IPersonService personService;
        public PersonController(IPersonService _personService) 
        {
            personService = _personService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] PersonRequestDto person)
        {
            ResponseDto dataResponse = new ResponseDto();
            try
            {
                await personService.Add(person);
                dataResponse.Message = "Person updated.";
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PersonRequestDto model)
        {
            ResponseDto dataResponse = new ResponseDto();
            try
            {
                await personService.Update(id, model);
                dataResponse.Message = "Person updated.";
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
                await personService.Delete(id);
                dataResponse.Message = "Person Deleted.";
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

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var persons = await personService.GetAll(false);
                return Ok(persons);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id, bool includeRelations)
        {
            ResponseDto dataResponse = new ResponseDto();
            try
            {
                var taskInformation = await personService.GetById(id, includeRelations);
                dataResponse.Success = true;
                dataResponse.Message = "Person Information";
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
    }
}
