using Application.Contracts;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Devices.Commands;

public class PatchDevice(Guid id, JsonPatchDocument jsonPatchDocument) : IRequest<BaseResponse>
{
    public Guid Id { get; set; } = id;
    public JsonPatchDocument JsonPatchDocument { get; set; } = jsonPatchDocument;
}