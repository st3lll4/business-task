using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Person : BaseEntity
{
    [MaxLength(128)] public string FirstName { get; set; } = default!;

    [MaxLength(128)] public string LastName { get; set; } = default!;
    
    [MaxLength(11)] public string IdCode { get; set; } = default!;
    
    public ICollection<Shareholder>? Shareholders { get; set; }
}