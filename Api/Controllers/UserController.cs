using Data.Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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
    public async Task<ActionResult<User>> Show(int id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(cancellationToken, id);
        return user;
    }

    [HttpPost]
    public async Task<User> Store(User user, CancellationToken cancellationToken)
    {
        var newUser = await _userRepository.AddAsync(user, cancellationToken);
        return newUser.Entity;
    }

    [HttpPut("{id:int}")]
    public async Task<User> Update(int id, User user, CancellationToken cancellationToken)
    {
        var targetUser = await _userRepository.GetByIdAsync(cancellationToken, id);

        targetUser.UserName = user.UserName;
        targetUser.PasswordHash = user.PasswordHash;
        targetUser.FullName = user.FullName;
        targetUser.Age = user.Age;
        targetUser.Gender = user.Gender;
        targetUser.IsActive = user.IsActive;
        targetUser.LastLoginDate = user.LastLoginDate;

        var editedEntity = await _userRepository.UpdateAsync(targetUser, cancellationToken);
        return editedEntity.Entity;
    }

    [HttpDelete("{id:int}")]
    public async Task Destroy(int id, CancellationToken cancellationToken)
    {
        var targetUser = await _userRepository.GetByIdAsync(cancellationToken, id);
        await _userRepository.DeleteAsync(targetUser, cancellationToken);

        // return Ok();
    }
}