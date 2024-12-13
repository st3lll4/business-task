namespace Domain;

public class Business : BaseEntity
{
    public string BusinessName { get; set; } = default!;
    public string RegistryCode { get; set; } = default!;
    public int TotalCapital { get; set; } 
    
    // todo: vaata ule fkd !!!!!!!
    // kui business on ise shareholder?
}