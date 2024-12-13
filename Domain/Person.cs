using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Person : BaseEntity
{
    [Required] [MaxLength(128)] public string FirstName { get; set; } = default!;

    [Required] [MaxLength(128)] public string LastName { get; set; } = default!;

    public ICollection<Shareholder>? Shareholders { get; set; }
}