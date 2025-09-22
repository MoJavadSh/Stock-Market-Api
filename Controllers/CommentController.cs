using api.Data;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/comment")]

public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepo;
    public CommentController(ICommentRepository commentRepo)
    {
        _commentRepo = commentRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var Comments = (await _commentRepo.GetAllAsync())
            .Select(c => c.ToCommentDto());
        return Ok(Comments);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetById([FromRoute] int id)
    {
        var Comment = await _commentRepo.GetByIdAsync(id);
        if (Comment == null)
            {
            return NotFound();
            }
        return Ok(Comment.ToCommentDto());
    }
}