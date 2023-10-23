using Common.Utilities;
using Data.Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Data.Repositories;

public class UserRepository: Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public Task<User?> GetByCredential(string username, string password, CancellationToken cancellationToken)
    {
        var passwordHash = SecurityHelper.GetSha256Hash(password);
        return Table.Where(u => u.UserName == username && u.PasswordHash == passwordHash).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<EntityEntry<User>> AddAsync(User user, string password, CancellationToken cancellationToken)
    {
        var exists = await TableNoTracking.AnyAsync(p => p.UserName == user.UserName);
        if (exists)
            throw new Exception("The username has already been taken.");

        var passwordHash = SecurityHelper.GetSha256Hash(password);
        user.PasswordHash = passwordHash;
        return await base.AddAsync(user, cancellationToken);
    }
}