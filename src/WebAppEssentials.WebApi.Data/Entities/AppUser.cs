using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebAppEssentials.WebApi.Data.Entities;

public class AppUser : IdentityUser
{
    [StringLength(255)] public string? FirstName { get; set; }
    [StringLength(255)] public string? LastName { get; set; }
}