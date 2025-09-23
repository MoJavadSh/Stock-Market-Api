using api.Dtos.Stock;
using api.Helpers;
using api.Models;

namespace api.Interfaces;

public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync(StockQueryObject query);
    Task<Stock?> GetStockByIdAsync(Guid id);
    Task<Stock> CreateAsync(Stock stock);
    Task<Stock> UpdateAsync(Guid id, UpdateStockRequestDto stockDto);
    Task<Stock?> DeleteAsync(Guid id);
    Task<Boolean> CheckIfStockExists(Guid stockId);
}