using System.Text.Json.Serialization;
using Domain.Enums;

namespace Domain.Entities;

public class Device
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public DeviceType Type { get; set; }
    public Guid? ParentId { get; set; }
    public bool Enabled { get; set; }
    public bool Deleted { get; set; }
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedUtc { get; set; } = DateTime.UtcNow;

    [JsonIgnore] public Device Parent { get; set; }
    public virtual ICollection<DeviceAttribute> Attributes { get; set; }
}