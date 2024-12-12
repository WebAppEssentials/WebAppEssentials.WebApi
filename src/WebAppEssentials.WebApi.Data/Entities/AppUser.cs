using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebAppEssentials.WebApi.Data.Entities;

/// <summary>
/// Represents a user in the application.
/// Inherits from <see cref="Microsoft.AspNetCore.Identity.IdentityUser"/>, which provides properties for user identification.
/// </summary>
public class AppUser : IdentityUser
{
    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    /// <value>
    /// A string representing the first name. The maximum length is 255 characters.
    /// </value>
    [StringLength(255)]
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    /// <value>
    /// A string representing the last name. The maximum length is 255 characters.
    /// </value>
    [StringLength(255)]
    public string? LastName { get; set; }
}