using Data.Contracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route(("api/user"))]
public class UserController
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<List<User>> Index()
    {
        var users = await _userRepository.TableNoTracking.ToListAsync();
        return users;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> Show(FromRouteAttribute id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(cancellationToken, id);
        return user;
    }

    [HttpPost]
    public async Task Store(User user, CancellationToken cancellationToken)
    {
        await _userRepository.AddAsync(user, cancellationToken);
    }

    [HttpPut("{id:int}")]
    public async Task Update(int id, User user, CancellationToken cancellationToken)
    {
        var targetUser = await _userRepository.GetByIdAsync(cancellationToken, id);

        targetUser.UserName = user.UserName;
        targetUser.PasswordHash = user.PasswordHash;
        targetUser.FullName = user.FullName;
        targetUser.Age = user.Age;
        targetUser.Gender = user.Gender;
        targetUser.IsActive = user.IsActive;
        targetUser.LastLoginDate = user.LastLoginDate;

        await _userRepository.UpdateAsync(targetUser, cancellationToken);
        
        // return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task Destroy(int id, CancellationToken cancellationToken)
    {
        var targetUser = await _userRepository.GetByIdAsync(cancellationToken, id);
        await _userRepository.DeleteAsync(targetUser, cancellationToken);

        // return Ok();
    }
}