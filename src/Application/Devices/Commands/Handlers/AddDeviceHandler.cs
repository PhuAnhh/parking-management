using Application.Devices.Models;
using Application.Services.Abstractions;
using MediatR;

namespace Application.Devices.Commands.Handlers;

public class AddDeviceHandler(IDeviceService service) : IRequestHandler<AddDevice, DeviceDto>
{
    public async Task<DeviceDto> Handle(AddDevice request, CancellationToken cancellationToken)
    {
        return await service.AddAsync(request);
    }
}