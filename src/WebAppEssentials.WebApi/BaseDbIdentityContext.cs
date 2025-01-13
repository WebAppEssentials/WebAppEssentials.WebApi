using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebAppEssentials.Entities;

namespace WebAppEssentials;

public class BaseDbIdentityContext : IdentityDbContext<AppUser>
{
    
}