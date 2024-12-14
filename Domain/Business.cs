namespace Domain;

public class Business : BaseEntity
{
    public string BusinessName { get; set; } = default!;
    public string RegistryCode { get; set; } = default!;
    public int TotalCapital { get; set; } 
    public DateTime FoundingDate { get; set; }
    
    public ICollection<ShareholderInBusiness>? ShareholdersInBusiness { get; set; } // this businesses' shareholders
    
    public ICollection<Shareholder>? ShareholderPlaceInBusiness { get; set; } // where this business is a shareholder
}