using Application.Contracts;
using MediatR;

namespace Application.Devices.Commands;

public class DeleteDevice(Guid id) : IRequest<BaseResponse>
{
    public Guid Id { get; set; } = id;
}