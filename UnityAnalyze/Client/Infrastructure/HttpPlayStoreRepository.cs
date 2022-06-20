using System.Net.Http.Json;
using UnityAnalyze.Client.Infrastructure.Base;
using UnityAnalyze.Shared.ActionResult;
using UnityAnalyze.Shared.Dtos;
using UnityApi.Shared.Dtos.PlayStore;
namespace UnityAnalyze.Client.Infrastructure;

public class HttpPlayStoreRepository : HttpBaseRepository
{
	public HttpPlayStoreRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
	{
	}

	public async Task<CustomActionResult<ReadPlayStoreDtoExtension>> GetPlayStoreAsync(int gameId)
	{
		return await HttpClient.GetFromJsonAsync<CustomActionResult<ReadPlayStoreDtoExtension>>($"/api/playstore/get-by/{gameId}");
	}
	
	public async Task<CustomActionResult> CreateResponse(CreateResponseDto data)
	{
		using var response = await HttpClient.PostAsJsonAsync($"/api/playstore/", data);
		
		return await response.Content.ReadFromJsonAsync<CustomActionResult>();
	}

	public async Task<CustomActionResult<PlayStoreAllGamesInfo>> GetFullPlayStoreAsync()
	{
		return await HttpClient.GetFromJsonAsync<CustomActionResult<PlayStoreAllGamesInfo>>($"/api/playstore/get-all/");
	}
}
