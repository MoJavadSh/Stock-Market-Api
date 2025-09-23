using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository;

public class StockRepository : IStockRepository
{
    private readonly ApplicationDBContext _context;

    public StockRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<List<Stock>> GetAllAsync(StockQueryObject query)
    {
        // Make the query flexible
        var stocks = _context.Stocks
            .Include(c => c.Comments)
            .AsQueryable();

        // Apply Filters
        if (!string.IsNullOrWhiteSpace(query.Symbol))
        {
            stocks = stocks.Where(c => c.Symbol == query.Symbol);
        }

        if (!string.IsNullOrWhiteSpace(query.CompanyName))
        {
            stocks = stocks.Where(c => c.CompanyName == query.CompanyName);
        }

        return await stocks.ToListAsync();
    }

    public async Task<Stock?> GetStockByIdAsync(Guid id)
    {
        return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Stock> CreateAsync(Stock stock)
    {
        await _context.Stocks.AddAsync(stock);
        await _context.SaveChangesAsync();
        return stock;
    }

    public async Task<Stock> UpdateAsync(Guid id, UpdateStockRequestDto stockDto)
    {
        var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        if (stock == null)
            return null;

        stock.Symbol = stockDto.Symbol;
        stock.CompanyName = stockDto.CompanyName;
        stock.Purchase = stockDto.Purchase;
        stock.LastDiv = stockDto.LastDiv;
        stock.Industry = stockDto.Industry;
        stock.MarketCap = stockDto.MarketCap;

        await _context.SaveChangesAsync();
        return stock;
    }

    public async Task<Stock?> DeleteAsync(Guid id)
    {
        var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        if (stockModel == null)
            return null;
        _context.Stocks.Remove(stockModel);
        await _context.SaveChangesAsync();
        return stockModel;
    }

    public async Task<bool> CheckIfStockExists(Guid stockId)
    {
        return await _context.Stocks.AnyAsync(x => x.Id == stockId);
    }
}