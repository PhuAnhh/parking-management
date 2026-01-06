using Application.Devices.Models;
using Application.Services.Abstractions;
using MediatR;

namespace Application.Devices.Commands.Handlers;

public class UpdateDeviceHandler(IDeviceService service) : IRequestHandler<UpdateDevice, DeviceDto>
{
    public async Task<DeviceDto> Handle(UpdateDevice request, CancellationToken cancellationToken)
    {
        return await service.UpdateAsync(request);
    }
}