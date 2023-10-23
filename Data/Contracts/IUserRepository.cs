using Entities.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Data.Contracts;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByCredential(string username, string password, CancellationToken cancellationToken);

    Task<EntityEntry<User>> AddAsync(User user, string password, CancellationToken cancellationToken);
}