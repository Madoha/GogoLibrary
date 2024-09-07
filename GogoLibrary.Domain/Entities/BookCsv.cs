
namespace GogoLibrary.Domain.Entities;

public class BookCsv
{
    // [Name("isbn")]
    public string? Isbn { get; set; }
    // [Name("book_title")]
    public string? Book_title { get; set; }
    // [Name("book_author")]
    public string? Book_author { get; set; }
    // [Name("year_of_publication")]
    public string? Year_of_publication { get; set; }
    // [Name("publisher")]
    public string? Publisher { get; set; }
    // [Name("image_url_l")]
    public string? Image_url_l { get; set; }
    // [Name("link")]
    public string? Link { get; set; }
}