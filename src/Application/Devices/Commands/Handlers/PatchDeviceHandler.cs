using Application.Contracts;
using Application.Services.Abstractions;
using MediatR;

namespace Application.Devices.Commands.Handlers;

public class PatchDeviceHandler(IDeviceService service) : IRequestHandler<PatchDevice, BaseResponse>
{
    public async Task<BaseResponse> Handle(PatchDevice request, CancellationToken cancellationToken)
    {
        return await service.PatchAsync(request);
    }
}