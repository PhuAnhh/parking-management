using System.Linq.Expressions;
using Application.Devices.Models;
using Domain.Entities;
using MediatR;

namespace Application.Devices.Commands;

public class UpdateDevice : IRequest<DeviceDto>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public bool Enabled { get; set; }
    public Guid? ParentId { get; set; }
    public IEnumerable<DeviceAttributeDto> Attributes { get; set; } = [];

    private static Expression<Func<UpdateDevice, Device>> Projection
    {
        get
        {
            return command => new Device
            {
                Id = command.Id,
                Name = command.Name.Trim(),
                Code = command.Code.Trim(),
                Enabled = command.Enabled,
                ParentId = command.ParentId
            };
        }
    }

    public static Device Create(UpdateDevice model)
    {
        return Projection.Compile().Invoke(model);
    }
}