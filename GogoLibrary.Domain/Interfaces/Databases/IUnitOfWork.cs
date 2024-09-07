using GogoLibrary.Domain.Entities;
using GogoLibrary.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace GogoLibrary.Domain.Interfaces.Databases;

public interface IUnitOfWork : IStateSaveChanges
{
    Task<IDbContextTransaction> BeginTransactionAsync();
    IBaseRepository<User> Users { get; set; }
    IBaseRepository<Role> Roles { get; set; }
    IBaseRepository<UserRole> UserRoles { get; set; }
}