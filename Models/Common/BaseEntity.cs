namespace pustokApp.Models.Common;

public class BaseEntity
{
    public Guid Id { get; set; }
    protected BaseEntity()
    {
        Id = Guid.NewGuid();
    }
}