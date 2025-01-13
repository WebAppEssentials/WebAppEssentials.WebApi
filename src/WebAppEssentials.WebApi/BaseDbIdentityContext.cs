using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppEssentials.Entities;

namespace WebAppEssentials;

public class BaseDbIdentityContext(DbContextOptions options) : IdentityDbContext<AppUser>(options)
{
    
}