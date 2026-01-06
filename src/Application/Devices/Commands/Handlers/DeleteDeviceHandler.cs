using Application.Contracts;
using Application.Services.Abstractions;
using MediatR;

namespace Application.Devices.Commands.Handlers;

public class DeleteDeviceHandler(IDeviceService service) : IRequestHandler<DeleteDevice, BaseResponse>
{
    public async Task<BaseResponse> Handle(DeleteDevice request, CancellationToken cancellationToken)
    {
        return await service.DeleteAsync(request);
    }
}