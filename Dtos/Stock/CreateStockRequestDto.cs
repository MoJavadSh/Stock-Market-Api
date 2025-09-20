namespace api.Dtos.Stock;

public class CreateStockRequestDto
{
    // Fluent Validation
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public int Purchase { get; set; }
    public int LastDiv { get; set; }
    public string? Industry { get; set; } 
    public long MarketCap { get; set; } 
}