namespace GogoLibrary.Domain.Dto.Book;

public class SearchBookResultDto
{
    public long Id { get; set; }
    public string BookTitle { get; set; }
    public string BookAuthor { get; set; }
    public string Description { get; set; }
    public int RecommendCount { get; set; }
    public string YearOfPublication { get; set; }
    public string Publisher { get; set; }
    public string ImageUrl { get; set; }
    public string Link { get; set; }
}