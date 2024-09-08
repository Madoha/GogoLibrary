using GogoLibrary.Domain.Interfaces;

namespace GogoLibrary.Domain.Entities;

public class User : IAuditable
{
    public long Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
    public UserToken UserToken { get; set; }
    public List<Role> Roles { get; set; }
    public List<Book>? Books { get; set; }
    public List<BookComment>? Comments { get; set; }
    public List<UserFavoriteBook>? FavoriteBooks { get; set; }
    public List<Club> Clubs { get; set; }
    public List<UserBookRecommendation>? UserBookRecommendations { get; set; }
}