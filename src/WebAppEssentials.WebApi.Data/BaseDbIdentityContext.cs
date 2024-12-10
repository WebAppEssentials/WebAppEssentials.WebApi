using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebAppEssentials.WebApi.Data.Entities;

namespace WebAppEssentials.WebApi.Data;

public class BaseDbIdentityContext : IdentityDbContext<AppUser>
{
    
}