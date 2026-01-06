using Application.Devices.Commands;
using Application.Devices.Models;
using Domain.Entities;
using Domain.Enums;

namespace Application.Repositories;

public interface IDeviceRepository
{
    Task<Device?> GetByIdAsync(Guid id);
    Task AddAsync(Device entity);
    Task UpdateAsync(Device entity);
    Task<bool> RemoveAsync(Guid id);
    Task<bool> ExistsNameAsync(string name, DeviceType type);
    Task<bool> ExistsNameAsync(string name, DeviceType type, Guid id);
    Task<bool> ExistsCodeAsync(string code, DeviceType type);
    Task<bool> ExistsCodeAsync(string code, DeviceType type, Guid id);
    IQueryable<Device> AsQueryable();
}