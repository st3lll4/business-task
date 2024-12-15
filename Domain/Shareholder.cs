using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Shareholder : BaseEntity
{
    public int? PersonId { get; set; } 
    public Person? Person { get; set; }
    
    public int? ShareholderBusinessId { get; set; }
    public Business? ShareholderBusiness { get; set; }
    
    public ICollection<ShareholderInBusiness>? ShareholdersInBusiness { get; set; }
}