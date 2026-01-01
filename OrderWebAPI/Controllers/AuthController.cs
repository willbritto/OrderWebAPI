using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _config;

    public AuthController(UserManager<ApplicationUser> useManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService, IConfiguration config, RoleManager<IdentityRole> roleManager)
    {
        _useManager = useManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _config = config;
        _roleManager = roleManager;
    }


    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterDTO registerDTO) 
    {
        var usersExists = await _useManager.FindByNameAsync(registerDTO.Username);
        if (usersExists != null)
            return BadRequest(new { Status = "Error", Message = "Usuario já existe" });

        ApplicationUser user = new()
        {
            Email = registerDTO.Email,
            UserName = registerDTO.Username,
            SecurityStamp = Guid.NewGuid().ToString()

        };

        var result = await _useManager.CreateAsync(user, registerDTO.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        if (!await _roleManager.RoleExistsAsync(registerDTO.Role))
            await _roleManager.CreateAsync(new IdentityRole(registerDTO.Role));

        if (!string.IsNullOrEmpty(registerDTO.Role))
            await _useManager.AddToRoleAsync(user, registerDTO.Role);

        return Ok(new { Status = "Success", Message = "Usuario registrado com sucesso ..." });

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
