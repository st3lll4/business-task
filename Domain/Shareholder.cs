using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Shareholder : BaseEntity
{
    public bool IsFounder { get; set; }
    
    public int? PersonId { get; set; } 
    public Person? Person { get; set; }
    
    public int? BusinessId { get; set; }
    public Business? Business { get; set; }
    
    public int ShareholderTypeId { get; set; }
    public ShareholderType? ShareholderType { get; set; }
}