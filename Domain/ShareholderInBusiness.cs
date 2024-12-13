namespace Domain;

public class ShareholderInBusiness : BaseEntity
{
    public bool IsFounder { get; set; }
    public int ShareCapital { get; set; }
    
    public int BusinessId { get; set; }
    public Business? Business { get; set; }
    
    public int ShareholderId { get; set; }
    public Shareholder? Shareholder { get; set; }
}