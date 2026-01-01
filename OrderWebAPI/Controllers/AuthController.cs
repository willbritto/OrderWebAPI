using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrderWebAPI.DTOs;
using OrderWebAPI.Models;
using OrderWebAPI.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OrderWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{

    private readonly UserManager<ApplicationUser> _useManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _config;

    public AuthController(UserManager<ApplicationUser> useManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService, IConfiguration config)
    {
        _useManager = useManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _config = config;
    }


    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDTO loginDTO)
    {
        var user = await _useManager.FindByNameAsync(loginDTO.Username);
        if (user == null)  
            return Unauthorized("Usuario não encontrado ...");

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
        if (!result.Succeeded)
            return Unauthorized("Senha Invalida");

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
            new Claim("id" , user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        //adicionar roles
        var userRoles = await _useManager.GetRolesAsync(user);
        foreach (var role in userRoles)
            authClaims.Add(new Claim(ClaimTypes.Role, role));

        //Gerar token
        var token = _tokenService.GenerateAccessToken(authClaims, _config);
        var refreshToken = _tokenService.GenerateRefreshToken();

        //atualizar refresh token no usuario
        int.TryParse(_config["JWT:RefreshTokenValidityInMinutes"], out int refreshTokenValidityInMinute);
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(refreshTokenValidityInMinute);
        await _useManager.UpdateAsync(user);

        return Ok(new
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken = refreshToken,
            Expiration = token.ValidTo
        });

    }

              

}
