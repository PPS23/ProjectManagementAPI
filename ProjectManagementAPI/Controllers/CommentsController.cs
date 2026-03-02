using EFCoreAPI.Entities.DTOs;
using EFCoreAPI.Entities.DTOs.Comments;
using EFCoreAPI.Entities.DTOs.Status;
using EFCoreAPI.Repositories;
using EFCoreAPI.Repositories.Interfaces;
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
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService commentService;
        public CommentsController(ICommentService _commentService)
        {
            commentService = _commentService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            ResponseDto dataResponse = new ResponseDto();
            try
            {
                dataResponse.Data = await commentService.GetAll(false);
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
                dataResponse.Success = true;
                dataResponse.Message = "Comment By Id";
                dataResponse.Data = await commentService.GetById(id, true);
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
        public async Task<IActionResult> Add([FromBody] CommentRequestDto model)
        {
            ResponseDto dataResponse = new ResponseDto();
            try
            {
                await commentService.Add(model);
                dataResponse.Success = true;
                dataResponse.Message = "Comment added";
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
        public async Task<IActionResult> Update(int id, [FromBody] CommentRequestDto model)
        {
            ResponseDto dataResponse = new ResponseDto();
            try
            {
                await commentService.Update(id, model);
                dataResponse.Message = "Comment updated.";
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
                await commentService.Delete(id);
                dataResponse.Success = true;
                dataResponse.Message = "Comment deleted.";
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
