namespace GogoLibrary.Domain.Entities;

public class UserBookRecommendation
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long BookId { get; set; }
    public Book Book { get; set; }
}