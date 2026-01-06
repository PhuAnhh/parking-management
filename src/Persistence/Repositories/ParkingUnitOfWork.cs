using Application.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using Persistence.DbContexts;

namespace Persistence.Repositories;

public class ParkingUnitOfWork(
    ParkingDbContext context,
    IDeviceRepository devices
) : IParkingUnitOfWork
{
    private IDbContextTransaction _transaction;
    public IDeviceRepository Devices { get; } = devices;

    public async Task BeginTransactionAsync()
    {
        _transaction = await context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        await context.SaveChangesAsync();
        if (_transaction == null) return;
        await _transaction.CommitAsync();
        _transaction.Dispose();
    }

    public async Task RollbackAsync()
    {
        if (_transaction == null) return;
        await _transaction.RollbackAsync();
        _transaction.Dispose();
    }
}