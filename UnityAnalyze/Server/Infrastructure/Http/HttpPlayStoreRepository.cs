using AutoMapper;
using UnityAnalyze.Server.Infrastructure.Http.Base;
using UnityAnalyze.Shared.ActionResult;
using UnityAnalyze.Shared.Dtos;
using UnityApi.Shared.Dtos.PlayStore;
namespace UnityAnalyze.Server.Infrastructure.Http;

public class HttpPlayStoreRepository : HttpRepositoryBase
{
	private readonly IMapper _mapper;
	
	public HttpPlayStoreRepository(IMapper mapper)
	{
		_mapper = mapper;
	}
	
	public async Task<CustomActionResult<ReadPlayStoreDtoExtension>> GetPlayStoreInfoBy(string token, int gameId)
	{
		return _mapper.Map<CustomActionResult<ReadPlayStoreDtoExtension>>
			(await GetAndReadAsync<CustomActionResult<ReadPlayStoreDto>>($"/api/play-store-info/get/by/{token}", gameId.ToString()));
	}
	
	public async Task<CustomActionResult> CreateResponse(CreateResponseDto data)
	{
		return await PostAndReadAsync<CustomActionResult, CreateResponseDto>("/api/play-store-info/", data);
	}

	public async Task<CustomActionResult<PlayStoreAllGamesInfo>> GetAllPlayStoreInfoAsync(string token)
	{
		return await GetAndReadAsync<CustomActionResult<PlayStoreAllGamesInfo>>("/api/play-store-info/get/all/by", token);
	}
}
