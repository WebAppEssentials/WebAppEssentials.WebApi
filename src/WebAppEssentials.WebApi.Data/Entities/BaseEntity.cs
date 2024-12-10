using System.ComponentModel.DataAnnotations;

namespace WebAppEssentials.WebApi.Data.Entities;

/// <summary>
/// Represents a base entity with common properties such as Id, CreatedBy, Created, ModifiedBy, and Modified.
/// </summary>
/// <typeparam name="TKey">The type of the primary key for the entity.</typeparam>
public abstract class BaseEntity<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Gets or sets the primary key of the entity.
    /// </summary>
    /// <value>The primary key of the entity.</value>
    [Key]
    public required TKey Id { get; set; }

    /// <summary>
    /// Gets or sets the username of the user who created the entity.
    /// </summary>
    /// <value>The username of the user who created the entity.</value>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    /// <value>The date and time when the entity was created.</value>
    public DateTime Created { get; set; }
    
    /// <summary>
    /// Gets or sets the username of the user who last modified the entity.
    /// </summary>
    /// <value>The username of the user who last modified the entity.</value>
    public string? ModifiedBy { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was last modified.
    /// </summary>
    /// <value>The date and time when the entity was last modified.</value>
    public DateTime? Modified { get; set; }
}