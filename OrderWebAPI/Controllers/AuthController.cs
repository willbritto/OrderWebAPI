using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.RateLimiting;
using OrderWebAPI.DTOs.Autentications;
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
    private readonly ILogger<AuthController> _logger;

    public AuthController(UserManager<ApplicationUser> useManager, SignInManager<ApplicationUser> signInManager,
        ITokenService tokenService, IConfiguration config, RoleManager<IdentityRole> roleManager, ILogger<AuthController> logger)
    {
        _useManager = useManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _config = config;
        _roleManager = roleManager;
        _logger = logger;
    }

    /// <summary>
    /// Registers a new user account with the specified registration details.
    /// </summary>
    /// <param name="registerDTO"></param>
    /// <returns></returns>
    [EnableRateLimiting("fixedRL")]    
    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterDTO registerDTO)
    {
        _logger.LogInformation("\n =============================");
        _logger.LogInformation("\n == Register new user /Register == \n");
        _logger.LogInformation(" ============================= \n");

        var usersExists = await _useManager.FindByNameAsync(registerDTO.Username);
        if (usersExists != null)
            return BadRequest(new { Status = "Error", Message = " User already exists .." });

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

        return Ok(new { Status = "Success", Message = "User successfully registered ..." });

    }

    /// <summary>
    /// Authenticates a user with the provided credentials and issues a new access token and refresh token upon successful
    /// login.
    /// </summary>
    /// <param name="loginDTO"></param>
    /// <returns></returns>

    [EnableCors]
    [EnableRateLimiting("fixedRL")]    
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDTO loginDTO)
    {

        _logger.LogInformation("\n =============================");
        _logger.LogInformation(" == Login user /Login == ");
        _logger.LogInformation(" ============================= \n");
        var user = await _useManager.FindByNameAsync(loginDTO.Username);
        if (user == null)
            return Unauthorized("User not found ...");

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
        if (!result.Succeeded)
            return Unauthorized("Invalid password ..");

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
