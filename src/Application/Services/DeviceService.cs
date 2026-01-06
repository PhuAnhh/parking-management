using Application.Contracts;
using Application.Devices.Commands;
using Application.Devices.Models;
using Application.Repositories;
using Application.Services.Abstractions;
using Domain.Entities;

namespace Application.Services;

public class DeviceService(IParkingUnitOfWork unitOfWork) : IDeviceService
{
    public async Task<DeviceDto> AddAsync(AddDevice command)
    {
        await unitOfWork.BeginTransactionAsync();
        try
        {
            if (await unitOfWork.Devices.ExistsNameAsync(command.Name, command.Type))
            {
                throw new InvalidOperationException($"Device name '{command.Name}' already exists.");
            }

            if (await unitOfWork.Devices.ExistsCodeAsync(command.Code, command.Type))
            {
                throw new InvalidOperationException($"Device code '{command.Code}' already exists.");
            }

            if (command.ParentId.HasValue)
            {
                var parent = await unitOfWork.Devices.GetByIdAsync(command.ParentId.Value);
                if (parent == null)
                    throw new KeyNotFoundException("Parent device does not exist.");
            }

            var entity = AddDevice.Create(command);

            await unitOfWork.Devices.AddAsync(entity);
            await unitOfWork.CommitAsync();

            return DeviceDto.Create(entity);
        }
        catch
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task<DeviceDto> UpdateAsync(UpdateDevice command)
    {
        await unitOfWork.BeginTransactionAsync();
        try
        {
            var existingEntity = await unitOfWork.Devices.GetByIdAsync(command.Id)
                                 ?? throw new KeyNotFoundException("Device not found.");

            if (await unitOfWork.Devices.ExistsNameAsync(
                    command.Name, existingEntity.Type, command.Id))
            {
                throw new InvalidOperationException($"Device name '{command.Name}' already exists.");
            }

            if (await unitOfWork.Devices.ExistsCodeAsync(
                    command.Code, existingEntity.Type, command.Id))
            {
                throw new InvalidOperationException($"Device code '{command.Code}' already exists.");
            }

            if (command.ParentId.HasValue)
            {
                var parent = await unitOfWork.Devices.GetByIdAsync(command.ParentId.Value);
                if (parent == null)
                    throw new KeyNotFoundException("Parent device does not exist.");
            }

            var entity = UpdateDevice.Create(command);

            UpsertAttributes(existingEntity, command.Attributes);

            await unitOfWork.Devices.UpdateAsync(entity);
            await unitOfWork.CommitAsync();

            return DeviceDto.Create(entity);
        }
        catch
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task<BaseResponse> DeleteAsync(DeleteDevice command)
    {
        await unitOfWork.BeginTransactionAsync();
        try
        {
            var deleted = await unitOfWork.Devices.RemoveAsync(command.Id);
            if (!deleted)
            {
                throw new KeyNotFoundException($"Device with ID '{command.Id}' was not found.");
            }

            await unitOfWork.CommitAsync();

            return BaseResponse.Success;
        }
        catch
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task<DeviceDto> FindByIdAsync(GetDeviceById command)
    {
        var entity = await unitOfWork.Devices.GetByIdAsync(command.Id);
        if (entity == null)
            throw new KeyNotFoundException("Device not found.");

        return DeviceDto.Create(entity);
    }

    public async Task<BaseResponse> PatchAsync(PatchDevice command)
    {
        await unitOfWork.BeginTransactionAsync();
        try
        {
            var entity = await unitOfWork.Devices.GetByIdAsync(command.Id);
            if (entity == null)
                throw new KeyNotFoundException("Device not found.");

            command.JsonPatchDocument.ApplyTo(entity);
            entity.UpdatedUtc = DateTime.UtcNow;

            await unitOfWork.CommitAsync();

            return BaseResponse.Success;
        }
        catch
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }

    private static void UpsertAttributes(Device entity, IEnumerable<DeviceAttributeDto> newAttributes)
    {
        var existingAttributes = entity.Attributes
            .ToDictionary(x => x.Code);

        foreach (var attribute in newAttributes)
        {
            if (existingAttributes.TryGetValue(attribute.Code, out var current))
            {
                current.Value = attribute.Value;
                current.UpdatedUtc = DateTime.UtcNow;
            }
            else
            {
                entity.Attributes.Add(
                    new DeviceAttribute(attribute.Code, attribute.Value));
            }
        }
    }
}