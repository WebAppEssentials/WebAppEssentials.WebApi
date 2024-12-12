using Microsoft.EntityFrameworkCore;
using WebAppEssentials.WebApi.Data;

namespace WebAppEssentials.WebApi.Demo.Data;

public class DemoDbContext(DbContextOptions<DemoDbContext> options) : BaseDbContext(options)
{
    
}