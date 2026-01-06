using Application.Repositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContexts;

namespace Persistence.Repositories;

public class DeviceRepository(ParkingDbContext context) : IDeviceRepository
{
    public IQueryable<Device> AsQueryable()
    {
        return context.Devices
            .Include(x => x.Parent)
            .Include(x => x.Attributes);
    }

    public async Task<Device?> GetByIdAsync(Guid id)
    {
        return await AsQueryable().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Device entity)
    {
        await context.Devices.AddAsync(entity);
    }

    public async Task UpdateAsync(Device requestObject)
    {
        var targetObject = await context.Devices.FindAsync(requestObject.Id);

        if (targetObject != null)
        {
            targetObject.Name = requestObject.Name;
            targetObject.Code = requestObject.Code;
            targetObject.ParentId = requestObject.ParentId;
            targetObject.Enabled = requestObject.Enabled;
            targetObject.UpdatedUtc = DateTime.UtcNow;
        }
    }
    
    public async Task<bool> RemoveAsync(Guid key)
    {
        var entity = await context.Devices
            .Where(p => p.Id == key)
            .Include(i => i.Attributes)
            .FirstOrDefaultAsync();

        if (entity == null) return false;

        entity.Deleted = true;
        entity.Attributes.Clear();
        entity.UpdatedUtc = DateTime.UtcNow;

        context.Devices.Update(entity);

        return true;
    }
    
    public async Task<bool> ExistsNameAsync(string name, DeviceType type)
        => await context.Devices.AsNoTracking().AnyAsync(x => x.Name == name && x.Type == type);

    public async Task<bool> ExistsNameAsync(string name, DeviceType type, Guid id)
        => await context.Devices.AsNoTracking().AnyAsync(x => x.Name == name && x.Type == type && x.Id != id);

    public async Task<bool> ExistsCodeAsync(string code, DeviceType type)
        => await context.Devices.AsNoTracking().AnyAsync(x => x.Code == code && x.Type == type);

    public async Task<bool> ExistsCodeAsync(string code, DeviceType type, Guid id)
        => await context.Devices.AsNoTracking().AnyAsync(x => x.Code == code && x.Type == type && x.Id != id);
}