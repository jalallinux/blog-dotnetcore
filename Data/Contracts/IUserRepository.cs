using Entities.Models;

namespace Data.Contracts;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByCredential(string username, string password, CancellationToken cancellationToken);

    Task AddAsync(User user, string password, CancellationToken cancellationToken);
}