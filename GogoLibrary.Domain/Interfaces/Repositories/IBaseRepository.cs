using GogoLibrary.Domain.Interfaces.Databases;
using GogoLibrary.Domain.Result;

namespace GogoLibrary.Domain.Interfaces.Repositories;

public interface IBaseRepository<T> : IStateSaveChanges
{
    IQueryable<T> GetAll();
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    void DeleteAsync(T entity);
}