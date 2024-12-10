using WebAppEssentials.Models;

namespace WebAppEssentials.WebApi.Data.Repositories;

/// <summary>
/// Defines a generic repository interface for CRUD operations on entities of type T with a unique identifier of type TKey.
/// </summary>
/// <typeparam name="T">The type of entity to be managed.</typeparam>
/// <typeparam name="TKey">The type of the unique identifier for the entity.</typeparam>
public interface IGenericRepository<T, in TKey> where T : class where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Retrieves an entity by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to retrieve.</param>
    /// <returns>The entity with the specified unique identifier.</returns>
    Task<T> GetAsync(TKey id);

    /// <summary>
    /// Retrieves an entity by its unique identifier asynchronously and maps it to a different type.
    /// </summary>
    /// <typeparam name="TResult">The type to map the entity to.</typeparam>
    /// <param name="id">The unique identifier of the entity to retrieve.</param>
    /// <returns>The entity with the specified unique identifier, mapped to the specified type.</returns>
    Task<TResult> GetAsync<TResult>(TKey id);

    /// <summary>
    /// Retrieves all entities of type T asynchronously.
    /// </summary>
    /// <returns>A list of all entities of type T.</returns>
    Task<List<T>> GetAllAsync();

    /// <summary>
    /// Retrieves a paged list of entities of type T asynchronously, based on the provided query parameters.
    /// </summary>
    /// <typeparam name="TResult">The type to map the entities to.</typeparam>
    /// <param name="queryParameters">The query parameters for pagination and filtering.</param>
    /// <returns>A paged response containing the requested entities, mapped to the specified type.</returns>
    Task<PagedResponse<TResult>> GetAllAsync<TResult>(QueryParameters queryParameters) where TResult : class;

    /// <summary>
    /// Adds a new entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>The added entity.</returns>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Updates an existing entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    Task UpdateAsync(T entity);

    /// <summary>
    /// Deletes an entity by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete.</param>
    Task DeleteAsync(TKey id);

    /// <summary>
    /// Checks if an entity with the specified unique identifier exists asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to check.</param>
    /// <returns>True if the entity exists, otherwise false.</returns>
    Task<bool> ExistsAsync(TKey id);
}