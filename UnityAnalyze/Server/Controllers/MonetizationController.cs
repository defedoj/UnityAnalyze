using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UnityAnalyze.Server.Infrastructure.Http;
using UnityAnalyze.Server.Infrastructure.Repositories;
using UnityAnalyze.Server.Models;
using UnityAnalyze.Shared.ActionResult;
using UnityAnalyze.Shared.Dtos;
using UnityApi.Shared.Dtos.Monetization;
namespace UnityAnalyze.Server.Controllers;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class MonetizationController : ControllerBase
{
	private readonly UserRepository _userRepository;
	private readonly HttpMonetizationRepository _monetizationRepository;
	private readonly UserManager<User> _userManager;

	public MonetizationController(UserRepository userRepository, HttpMonetizationRepository monetizationRepository, UserManager<User> userManager)
	{
		_userRepository = userRepository;
		_monetizationRepository = monetizationRepository;
		_userManager = userManager;
	}

	[HttpGet]
	[Route("get/{gameId}")]
	public async Task<CustomActionResult<MonetizationInfo>> GetInfoAsync(int gameId)
	{
		return await _monetizationRepository.GetInfoAsync(await _userRepository.GetToken(_userManager.GetUserId(HttpContext.User)), gameId);
	}

	[HttpGet]
	[Route("get/full/")]
	public async Task<CustomActionResult<MonetizationFullInfo>> GetFullInfo()
	{
		return await _monetizationRepository.GetFullInfoAsync(await _userRepository.GetToken(_userManager.GetUserId(HttpContext.User)));
	}
}
