namespace Domain.Entities;

public class EntityAttribute
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Value { get; set; }
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedUtc { get; set; } = DateTime.UtcNow;

    protected EntityAttribute(string code, string value)
    {
        Code = code;
        Value = value;
    }

    public EntityAttribute()
    {
    }
}