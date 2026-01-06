using System.Linq.Expressions;

namespace Application.Devices.Models;

public class DeviceAttributeDto
{
    public Guid Id { get; set; }
    public Guid DeviceId { get; set; }
    public string Code { get; set; }
    public string Value { get; set; }

    private static Expression<Func<Domain.Entities.DeviceAttribute, DeviceAttributeDto>> Projection
    {
        get
        {
            return entity => new DeviceAttributeDto
            {
                Id = entity.Id,
                DeviceId = entity.DeviceId,
                Code = entity.Code,
                Value = entity.Value
            };
        }
    }

    public static DeviceAttributeDto Create(Domain.Entities.DeviceAttribute entity)
    {
        return entity == null ? null : Projection.Compile().Invoke(entity);
    }
}