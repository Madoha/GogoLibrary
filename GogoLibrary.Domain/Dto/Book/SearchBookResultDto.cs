namespace GogoLibrary.Domain.Dto.Book;

public class SearchBookResultDto
{
    public long BookId { get; set; }
    public string BookTitle { get; set; }
    public string BookAuthor { get; set; }
    public string YearOfPublication { get; set; }
    public string Publisher { get; set; }
    public string ImageUrl { get; set; }
    public string Link { get; set; }
}