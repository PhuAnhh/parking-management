namespace Application.Repositories;

public interface IParkingUnitOfWork
{
    IDeviceRepository Devices { get; }
    Task CommitAsync();
    Task RollbackAsync();
    Task BeginTransactionAsync();
}