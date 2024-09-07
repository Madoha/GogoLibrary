namespace GogoLibrary.Domain.Dto.Comment;

public class CommentDto
{
    public long Id { get; set; }
    public long BookId { get; set; }
    public long UserId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
}