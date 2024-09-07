using GogoLibrary.Domain.Interfaces;

namespace GogoLibrary.Domain.Entities;

public class BookComment : IAuditable
{
    public long Id { get; set; }
    public long BookId { get; set; }
    public Book Book { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}