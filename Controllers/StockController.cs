using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepo;

        public StockController(ApplicationDBContext context,  IStockRepository stockRepo)
        {
            _context = context;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = (await _stockRepo.GetAllAsync())
                .Select(s => s.ToStockDto());

            return Ok(stocks);
        }

        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetAllCommentByStockId([FromRoute] int id)
        // {
        //     var listofComments = (await _context.Comments
        //         .Where(p => p.StockId == id)
        //         .ToListAsync());
        //     var dto = new List<GetAllCommentByStockIdDTO>();
        //
        //     dto = listofComments.Select(p => new GetAllCommentByStockIdDTO()
        //     {
        //         Content = p.Content,
        //         Title = p.Title
        //     }).ToList();
        //     
        //     // var stocks = await _context.Comments
        //     //     .Where(p => p.StockId == id)
        //     //     .Select(p => new { p.Content, p.Title })
        //     //     .ToListAsync();
        //
        //     return Ok(dto);
        // }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var stock = await _stockRepo.GetStockByIdAsync(id);
            if (stock == null)
                return NotFound();

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto StockDto)
        {
            var stockModel = StockDto.ToStockFromCreateDto();
            await _stockRepo.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }
            // try
            // {
            //     var stockModel = StockDto.ToStockFromCreateDto();
            //     var duplicateStock = await _context.Stocks.AnyAsync(s => s.Symbol == stockModel.Symbol);
            //     if (duplicateStock)
            //     {
            //         return BadRequest();
            //     }
            //
            //     await _context.Stocks.AddAsync(stockModel);
            //     return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
            // }
            // catch (Exception e)
            // {
            //     return ValidationProblem();
            // }
        

        [HttpPut("{id}")] // Another way to write: [HttpPut] // [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateStockRequestDto updateDto)
        {
            // need a searching algorithm to find the first stock
            // check if it is existed
            var stockModel = await _stockRepo.UpdateAsync(id,updateDto);
            if (stockModel == null)
                return NotFound();
            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var stockModel = await _stockRepo.DeleteAsync(id);
            if (stockModel == null)
                return NotFound();
            return NoContent();
        }
    }
}