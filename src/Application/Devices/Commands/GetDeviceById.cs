using Application.Devices.Models;
using MediatR;

namespace Application.Devices.Commands;

public class GetDeviceById(Guid id) : IRequest<DeviceDto>
{
    public Guid Id { get; set; } = id;
}