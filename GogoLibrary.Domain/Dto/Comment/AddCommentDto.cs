namespace GogoLibrary.Domain.Dto.Comment;

public class AddCommentDto
{
    public long BookId { get; set; }
    public long UserId { get; set; }
    public string Content { get; set; }
}