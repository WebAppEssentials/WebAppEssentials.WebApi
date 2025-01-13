using Microsoft.EntityFrameworkCore;

namespace WebAppEssentials.WebApi.Demo.Data;

public class DemoDbContext(DbContextOptions<DemoDbContext> options) : BaseDbContext(options)
{
    
}