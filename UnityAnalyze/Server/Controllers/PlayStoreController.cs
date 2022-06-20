using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UnityAnalyze.Server.Infrastructure.Http;
using UnityAnalyze.Server.Infrastructure.Repositories;
using UnityAnalyze.Server.Models;
using UnityAnalyze.Shared.ActionResult;
using UnityAnalyze.Shared.Dtos;
using UnityApi.Shared.Dtos.PlayStore;
namespace UnityAnalyze.Server.Controllers;

[AllowAnonymous]
[Route("api/playstore")]
[ApiController]
public class PlayStoreController : ControllerBase
{
	private readonly HttpPlayStoreRepository _repository;
	private readonly UserRepository _userRepository;
	private readonly UserManager<User> _userManager;

	public PlayStoreController(HttpPlayStoreRepository repository, UserRepository userRepository, UserManager<User> userManager)
	{
		_repository = repository;
		_userRepository = userRepository;
		_userManager = userManager;
	}

	[HttpGet]
	[Route("get-by/{gameId}")]
	public async Task<CustomActionResult<ReadPlayStoreDtoExtension>> GetPlayStoreDataAsync(int gameId)
	{
		return await _repository.GetPlayStoreInfoBy(await _userRepository.GetToken(_userManager.GetUserId(HttpContext.User)), gameId);
	}

	[HttpGet]
	[Route("get-all/")]
	public async Task<CustomActionResult<PlayStoreAllGamesInfo>> GetAllPlayStoreInfo()
	{
		return await _repository.GetAllPlayStoreInfoAsync(await _userRepository.GetToken(_userManager.GetUserId(HttpContext.User)));
	}

	[HttpPost]
	public async Task<CustomActionResult> CreateResponse(CreateResponseDto data)
	{
		data.Token = await _userRepository.GetToken(_userManager.GetUserId(HttpContext.User));
		return await _repository.CreateResponse(data);
	}
}
