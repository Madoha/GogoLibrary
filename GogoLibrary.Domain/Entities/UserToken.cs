namespace GogoLibrary.Domain.Entities;

public class UserToken
{
    public long Id { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    
}