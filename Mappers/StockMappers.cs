using api.Dtos.Stock;
using api.Models;

namespace api.Mappers;

public static class StockMappers
{
    public static StockDto ToStockDto(this Stock stockModel)
    {
        return new StockDto
        {
            Id = stockModel.Id,
            Symbol = stockModel.Symbol,
            CompanyName = stockModel.CompanyName,
            Purchase = stockModel.Purchase,
            LastDiv = stockModel.LastDiv,
            Industry = stockModel.Industry,
            MarketCap = stockModel.MarketCap,
            Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList() // in this line, select comments in comment model one by one then convert it to commentDto and it returns Enumerable and we should convert it to list to mach the variable type
        };
    }
    public static Stock ToStockFromCreateDto(this CreateStockRequestDto stockDto)
    {
        if (stockDto.Purchase <= 0)
        {
            throw new ArgumentException("Purchase must be greater than zero");
        }
        
        return new Stock
        {
            Symbol = stockDto.Symbol.ToLower(),
            CompanyName = stockDto.CompanyName,
            Purchase = stockDto.Purchase,
            LastDiv = stockDto.LastDiv,
            Industry = stockDto.Industry,
            MarketCap = stockDto.MarketCap
        };
    }
}