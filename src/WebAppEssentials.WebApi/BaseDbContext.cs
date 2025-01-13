using Microsoft.EntityFrameworkCore;

namespace WebAppEssentials;

public class BaseDbContext(DbContextOptions options) : DbContext(options)
{
    
}