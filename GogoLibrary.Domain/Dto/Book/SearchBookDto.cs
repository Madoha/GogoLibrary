namespace GogoLibrary.Domain.Dto.Book;

public class SearchBookDto
{
    public string? BookTitle { get; set; }
    public string? BookAuthor { get; set; }
    public string? YearOfPublication { get; set; }
    public string? Publisher { get; set; }
}