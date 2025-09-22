namespace api.Dtos.Comment;

public class CommentDto
{
    //copy and paste the model
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public Guid? StockId { get; set; }
    // DELETE -> public Stock? Stock { get; set; } (don't want navigation prop)
}