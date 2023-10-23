using Data.Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFramework.Api;

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
    public async Task<ApiResult<List<User>>> Index()
    {
        var users = await _userRepository.TableNoTracking.ToListAsync();
        return new ApiResult<List<User>>
        {
            Data = users, Message = "List successfully fetched."
        };
    }

    [HttpGet("{id:int}")]
    public async Task<ApiResult<User>> Show(int id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(cancellationToken, id);
        return new ApiResult<User>{
            Data = user, Message = "User details successfully fetched."
        };
    }

    [HttpPost]
    public async Task<ApiResult<User>> Store(User user, CancellationToken cancellationToken)
    {
        var newUser = await _userRepository.AddAsync(user, cancellationToken);
        return new ApiResult<User>{
            Data = newUser.Entity, Message = "User successfully created."
        };
    }

    [HttpPut("{id:int}")]
    public async Task<ApiResult<User>> Update(int id, User user, CancellationToken cancellationToken)
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
        return new ApiResult<User>{
            Data = editedEntity.Entity, Message = "User details successfully updated."
        };
    }

    [HttpDelete("{id:int}")]
    public async Task<ApiResult> Destroy(int id, CancellationToken cancellationToken)
    {
        var targetUser = await _userRepository.GetByIdAsync(cancellationToken, id);
        await _userRepository.DeleteAsync(targetUser, cancellationToken);
        
        return new ApiResult{
            Message = "User successfully deleted."
        };
    }
}