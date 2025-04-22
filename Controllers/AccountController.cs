using BlogApi.Data;
using BlogApi.Extensions;
using BlogApi.Models;
using BlogApi.Services;
using BlogApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace BlogApi.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    private readonly TokenServices _tokenServices;
    
    public AccountController(TokenServices tokenServices)
    {
        _tokenServices = tokenServices;
    }

    [HttpPost("v1/accounts")]
    public async Task<IActionResult> Post(
        [FromServices] DataContext dataContext,
        [FromBody] RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        var user = new User
        {
            Name = model.Name,
            Email = model.Email,
            Slug = model.Email.Replace("@", "-").Replace(".", "-"),
        };

        var password = PasswordGenerator.Generate(length: 25);
        user.PasswordHash = PasswordHasher.Hash(password);


        try
        {
            await dataContext.Users.AddAsync(user);
            await dataContext.SaveChangesAsync();

            return Ok(new ResultViewModel<dynamic>(new
            {
                user = user.Email, 
                password
            }));
        }
        catch (DbUpdateException)
        {
            return BadRequest(new ResultViewModel<string>($"Email {model.Email} is invalid"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>($"Error internal server."));
        }
    }
    
    
    [HttpPost("v1/accounts/login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginViewModel model,
        [FromServices] DataContext dataContext,
        [FromServices] TokenServices tokenServices
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));
        
        var user = await dataContext
            .Users
            .AsNoTracking()
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Email == model.Email);

        if (user == null)
            return StatusCode(401, new ResultViewModel<string>($"user or password is incorrect."));
        
        if (!PasswordHasher.Verify(user.PasswordHash, model.Password))
            return StatusCode(401, new ResultViewModel<string>("user or password is incorrect."));
            
        try
        {
            var token = tokenServices.GenereateToken(user);
            return Ok(new ResultViewModel<string>(token, null));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>("05X04 - Falha interna no servidor"));
        }
    }
}