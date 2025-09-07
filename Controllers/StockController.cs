using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks = _context.Stocks.ToList()
                .Select(s => s.ToStockDto());

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var stock = _context.Stocks.Find(id);
            if (stock == null)
                return NotFound();

            return Ok(stock.ToStockDto());

        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDto StockDto)
        {
            var stockModel = StockDto.ToStockFromCreateDto();

            _context.Stocks.Add(stockModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut("{id}")] // Another way to write: [HttpPut] // [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            // need a searching algorithm to find the first stock
            // check if it is existed
            var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);
            if (stockModel == null)
                return NotFound();
            
            stockModel.Symbol = updateDto.Symbol;
            stockModel.CompanyName = updateDto.CompanyName;
            stockModel.Purchase =  updateDto.Purchase;
            stockModel.LastDiv =  updateDto.LastDiv;
            stockModel.Industry = updateDto.Industry;
            stockModel.MarketCap = updateDto.MarketCap;

            _context.SaveChanges();
            
            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);
            if (stockModel == null)
                return NotFound();
            _context.Stocks.Remove(stockModel);
            _context.SaveChanges();
            return NoContent();
        }


    }

}