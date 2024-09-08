namespace GogoLibrary.Domain.Dto.Book;

public class CreateBookDto
{
    public string? Isbn { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string? YearOfPublication { get; set; }
    public string? Description { get; set; }
    public string? Publisher { get; set; }
    public string? ImageUrl { get; set; }
    public string? Link { get; set; }
}