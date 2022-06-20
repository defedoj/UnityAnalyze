using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UnityAnalyze.Server.Models;
using UnityAnalyze.Shared.Auth;
namespace UnityAnalyze.Server.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController : ControllerBase
{
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;
	private readonly IMapper _mapper;

	public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
	{
		_userManager = userManager;
		_signInManager = signInManager;
		_mapper = mapper;
	}
	
	[HttpPost]
	public async Task<IActionResult> Login(LoginRequest request)
	{
		var user = await _userManager.FindByNameAsync(request.Email);
		if (user == null) return BadRequest("User does not exist");
		var singInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
		if (!singInResult.Succeeded) return BadRequest("Invalid password");
		await _signInManager.SignInAsync(user, request.RememberMe);
		return Ok();
	}
	
	[HttpPost]
	public async Task<IActionResult> Register(RegisterRequest parameters)
	{
		var user = _mapper.Map<User>(parameters);
		user.EmailConfirmed = true;
		
		var result = await _userManager.CreateAsync(user, parameters.Password);
		if (!result.Succeeded) return BadRequest(result.Errors.FirstOrDefault()?.Description);
		
		return await Login(new LoginRequest
		{
			Email = parameters.Email,
			Password = parameters.Password
		});
	}
	
	[Authorize]
	[HttpPost]
	public async Task<IActionResult> Logout()
	{
		await _signInManager.SignOutAsync();
		return Ok();
	}
	
	[HttpGet]
	public CurrentUser CurrentUserInfo()
	{
		return new CurrentUser
		{
			IsAuthenticated = User.Identity.IsAuthenticated,
			UserName = User.Identity.Name,
			Claims = User.Claims
				.ToDictionary(c => c.Type, c => c.Value)
		};
	}
}
