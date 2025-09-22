using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/comment")]

public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepo;
    private readonly IStockRepository _stockRepo;
    public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
    {
        _commentRepo = commentRepo;
        _stockRepo = stockRepo;
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

    [HttpPost("{stockId}")]
    public async Task<IActionResult> Create([FromRoute] int stockId, CreateCommentDto comment)
    {
        if (!await _stockRepo.CheckIfStockExists(stockId))
        {
            return BadRequest("Stock not found");
        }
        
        var commentModel = comment.ToCommentFromCreate(stockId);
        await _commentRepo.CreateAsync(commentModel);
        // return Ok(commentModel.ToCommentDto());
        return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
    }
}