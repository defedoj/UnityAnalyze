using System.Net.Http.Json;
using UnityAnalyze.Client.Infrastructure.Base;
using UnityAnalyze.Shared.ActionResult;
using UnityAnalyze.Shared.Dtos;
namespace UnityAnalyze.Client.Infrastructure;

public class HttpGameRepository : HttpBaseRepository
{
	public HttpGameRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
	{
	}

	public async Task<CustomActionResult<IEnumerable<ReadGameDto>>> GetByTokenAsync()
	{
		return await HttpClient.GetFromJsonAsync<CustomActionResult<IEnumerable<ReadGameDto>>>($"/api/game/");
	}
	
	public async Task<CustomActionResult<GameDetailsDto>> GetGameDetails(int id)
	{
		return await HttpClient.GetFromJsonAsync<CustomActionResult<GameDetailsDto>>($"/api/game/details/{id}");
	}
}
