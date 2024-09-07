using GogoLibrary.Domain.Interfaces;

namespace GogoLibrary.Domain.Entities;

public class Book : IAuditable
{
    public long Id { get; set; }
    public string? Isbn { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string? YearOfPublication { get; set; }
    public string? Description { get; set; }
    public string? Publisher { get; set; }
    public string? ImageUrl { get; set; }
    public string? Link { get; set; }
    public List<User>? Users { get; set; }
    public DateTime CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
    public List<BookComment>? Comments { get; set; }
}