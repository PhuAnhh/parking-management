using Application.Contracts;
using Application.Devices.Commands;
using Application.Devices.Models;

namespace Application.Services.Abstractions;

public interface IDeviceService
{
    // Task<IEnumerable<DeviceDto>> SearchAsync(SearchDevice query);

    Task<DeviceDto> AddAsync(AddDevice command);
    Task<DeviceDto> UpdateAsync(UpdateDevice command);
    Task<BaseResponse> DeleteAsync(DeleteDevice command);
    Task<DeviceDto> FindByIdAsync(GetDeviceById command);
    Task<BaseResponse> PatchAsync(PatchDevice command);
}