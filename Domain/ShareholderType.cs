namespace Domain;

public class ShareholderType : BaseEntity
{
    public string Title { get; set; } = default!; // nimetus
    
    public ICollection<Shareholder>? Shareholders { get; set; }
}