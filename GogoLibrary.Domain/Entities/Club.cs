using GogoLibrary.Domain.Interfaces;

namespace GogoLibrary.Domain.Entities;

public class Club : IAuditable
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string BookTitle { get; set; }
    public List<User> Users { get; set; }
    public DateTime CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public long? ModifiedBy { get; set; }
}