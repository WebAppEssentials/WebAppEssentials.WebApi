using Microsoft.EntityFrameworkCore;

namespace WebAppEssentials.WebApi.Data;

public class BaseDbContext(DbContextOptions options) : DbContext(options)
{
    
}