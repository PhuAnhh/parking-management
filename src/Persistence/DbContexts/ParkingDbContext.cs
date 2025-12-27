using Microsoft.EntityFrameworkCore;

namespace Persistence.DbContexts;

public class ParkingDbContext(DbContextOptions<ParkingDbContext> options) : DbContext(options)
{
    
}