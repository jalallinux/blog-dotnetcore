using Api.Requests.User;
using Data.Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFramework.Api;
using WebFramework.Filters;

namespace Api.Controllers;

[ApiController, ApiResultFilter, Route("api/user")]
public class UserController : ControllerBase
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
    public async Task<ApiResult<User>> Show([FromRoute] int id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(cancellationToken, id);
        return user == null ? NotFound() : Ok(user);
    }

    [HttpPost]
    public async Task<ApiResult<User>> Store([FromBody] UserStoreRequest request, CancellationToken cancellationToken)
    {
        var exist = await _userRepository.TableNoTracking.AnyAsync(u => u.UserName == request.UserName);
        if (exist)
        {
            return BadRequest("Username is invalid");
        }

        var user = new User
        {
            Age = request.Age,
            FullName = request.FullName,
            UserName = request.UserName,
            Gender = request.Gender
        };
        var newUser = await _userRepository.AddAsync(user, request.Password, cancellationToken);
        return Ok(newUser.Entity);
    }

    [HttpPut("{id:int}")]
    public async Task<ApiResult<User>> Update([FromRoute] int id, [FromBody] User user,
        CancellationToken cancellationToken)
    {
        var targetUser = await _userRepository.GetByIdAsync(cancellationToken, id);

        if (targetUser == null)
            return NotFound();

        targetUser.UserName = user.UserName;
        targetUser.PasswordHash = user.PasswordHash;
        targetUser.FullName = user.FullName;
        targetUser.Age = user.Age;
        targetUser.Gender = user.Gender;
        targetUser.IsActive = user.IsActive;
        targetUser.LastLoginDate = user.LastLoginDate;

        var editedEntity = await _userRepository.UpdateAsync(targetUser, cancellationToken);
        return Ok(editedEntity.Entity);
    }

    [HttpDelete("{id:int}")]
    public async Task<ApiResult> Destroy([FromRoute] int id, CancellationToken cancellationToken)
    {
        var targetUser = await _userRepository.GetByIdAsync(cancellationToken, id);
        if (targetUser == null)
            return NotFound();

        await _userRepository.DeleteAsync(targetUser, cancellationToken);
        return Content("User successfully deleted.");
    }
}