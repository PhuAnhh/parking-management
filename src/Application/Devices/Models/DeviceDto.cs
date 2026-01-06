using System.Linq.Expressions;
using Domain.Enums;

namespace Application.Devices.Models;

public class DeviceDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public DeviceType Type { get; set; }
    public bool Enabled { get; set; }
    public DeviceDto Parent { get; set; }
    public IEnumerable<DeviceAttributeDto> Attributes { get; set; }

    private static Expression<Func<Domain.Entities.Device, DeviceDto>> Projection
    {
        get
        {
            return entity => new DeviceDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Code = entity.Code,
                Type = entity.Type,
                Enabled = entity.Enabled,
                Parent = entity.ParentId.HasValue ? Create(entity.Parent) : null,
                Attributes = entity.Attributes != null && entity.Attributes.Count != 0
                    ? entity.Attributes.Select(DeviceAttributeDto.Create)
                    : null
            };
        }
    }

    public static DeviceDto Create(Domain.Entities.Device entity)
    {
        return entity == null ? null : Projection.Compile().Invoke(entity);
    }
}