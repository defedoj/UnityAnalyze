using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UnityAnalyze.Server.Infrastructure.Http;
using UnityAnalyze.Server.Infrastructure.Repositories;
using UnityAnalyze.Server.Models;
using UnityAnalyze.Shared.ActionResult;
using UnityAnalyze.Shared.Api;
using UnityAnalyze.Shared.Dtos;
namespace UnityAnalyze.Server.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
	private readonly HttpUserRepository _httpRepository;
	private readonly UserRepository _repository;
	private readonly UserManager<User> _userManager;

	public UserController(HttpUserRepository httpRepository, UserRepository repository, UserManager<User> userManager)
	{
		_httpRepository = httpRepository;
		_repository = repository;
		_userManager = userManager;
	}

	[HttpPost]
    public async Task<CustomActionResult<UserDto>> ConnectToApi(ConnectToApiData data)
    {
	    var result =  await _httpRepository.ConnectToApiAsync(data);

	    if (result.Data != null)
	    {
		    await _repository.SetToken(_userManager.GetUserId(HttpContext.User), result.Data);
	    }

	    return result;
    }

    [HttpGet]
    public async Task<UserDto> GetUserAsync()
    {
	    return await _repository.GetUserAsync(_userManager.GetUserId(HttpContext.User));
    }
}
