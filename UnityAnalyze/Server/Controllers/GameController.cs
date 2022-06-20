using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UnityAnalyze.Server.Infrastructure.Http;
using UnityAnalyze.Server.Infrastructure.Repositories;
using UnityAnalyze.Server.Models;
using UnityAnalyze.Shared.ActionResult;
using UnityAnalyze.Shared.Dtos;
namespace UnityAnalyze.Server.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
	private readonly HttpGameRepository _repository;
	private readonly UserRepository _userRepository;
	private readonly UserManager<User> _userManager;

	public GameController(HttpGameRepository httpRepository, UserRepository userRepository, UserManager<User> userManager)
	{
		_repository = httpRepository;
		_userRepository = userRepository;
		_userManager = userManager;
	}

	[HttpGet]
	[Route("details/{id}")]
	public async Task<CustomActionResult<GameDetailsDto>> GetDetailsBy(int id)
	{
		return await _repository.GetGameDetailsAsync(await _userRepository.GetToken(_userManager.GetUserId(HttpContext.User)), id);
	}
	
	[HttpGet]
	public async Task<CustomActionResult<IEnumerable<ReadGameDto>>> GetGamesByToken()
	{
		string token = await _userRepository.GetToken(_userManager.GetUserId(HttpContext.User));
		return await _repository.GetAllAsync(token);
	}
}
