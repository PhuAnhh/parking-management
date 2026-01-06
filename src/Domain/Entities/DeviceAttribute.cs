namespace Domain.Entities;

public class DeviceAttribute(string code, string value) : EntityAttribute(code, value)
{
    public Guid DeviceId { get; set; }

    public Device Device { get; set; }
}