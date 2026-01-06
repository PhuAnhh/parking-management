using Application.Devices.Models;
using Application.Services.Abstractions;
using MediatR;

namespace Application.Devices.Commands.Handlers;

public class GetDeviceByIdHandler(IDeviceService service) : IRequestHandler<GetDeviceById, DeviceDto>
{
    public async Task<DeviceDto> Handle(GetDeviceById request, CancellationToken cancellationToken)
    {
        return await service.FindByIdAsync(request);
    }
}