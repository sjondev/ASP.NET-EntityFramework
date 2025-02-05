using BlogApi.Models;
using BlogApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    private readonly TokenServices _tokenServices;
    
    public AccountController(TokenServices tokenServices)
    {
        _tokenServices = tokenServices;
    }
    
    [HttpPost("v1/login")]
    public IActionResult Login([FromServices] TokenServices tokenServices)
    {
        var token = tokenServices.GenereateToken(null);
        return Ok(token);
    }
    
    [Authorize(Roles = "user")]
    [HttpGet("v1/user")]
    public IActionResult GetUser()
        => Ok(User.Identity.Name);
    
    [Authorize(Roles =  "author")] // -> pedi para o jwt token se tiver o author ele vai acessar o conteudo dele.
    [HttpGet("v1/author")]
    public IActionResult GetAuthor()
        => Ok(User.Identity.Name);
    
    [Authorize(Roles = "admin")]
    [HttpGet("v1/admin")]
    public IActionResult GetAdmin()
        => Ok(User.Identity.Name);
}