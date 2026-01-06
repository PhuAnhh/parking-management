using System.Linq.Expressions;
using Application.Devices.Models;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Devices.Commands;

public class AddDevice : IRequest<DeviceDto>
{
    public string Name { get; set; }
    public string Code { get; set; }
    public DeviceType Type { get; set; }
    public bool Enabled { get; set; } = true;
    public Guid? ParentId { get; set; }
    public IEnumerable<DeviceAttributeDto> Attributes { get; set; } = new List<DeviceAttributeDto>();

    private static Expression<Func<AddDevice, Device>> Projection
    {
        get
        {
            return command => new Device
            {
                Id = Guid.NewGuid(),
                Name = command.Name.Trim(),
                Code = command.Code.Trim(),
                Type = command.Type,
                Enabled = command.Enabled,
                ParentId = command.ParentId,
                Attributes = command.Attributes.Select(x => new DeviceAttribute(x.Code, x.Value)).ToList()
            };
        }
    }

    public static Device Create(AddDevice model)
    {
        return Projection.Compile().Invoke(model);
    }
}