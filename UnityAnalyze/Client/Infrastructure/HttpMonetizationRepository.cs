using System.Net.Http.Json;
using UnityAnalyze.Client.Infrastructure.Base;
using UnityAnalyze.Shared.ActionResult;
using UnityAnalyze.Shared.Dtos;
using UnityApi.Shared.Dtos.Monetization;
namespace UnityAnalyze.Client.Infrastructure;

public class HttpMonetizationRepository : HttpBaseRepository
{
	public HttpMonetizationRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
	{
	}

	public async Task<CustomActionResult<MonetizationInfo>> GetMonetizationBy(int gameId)
	{
		return await HttpClient.GetFromJsonAsync<CustomActionResult<MonetizationInfo>>($"/api/monetization/get/{gameId}");
	}
	
	public async Task<CustomActionResult<MonetizationFullInfo>> GetFullMonetizationInfo()
	{
		return await HttpClient.GetFromJsonAsync<CustomActionResult<MonetizationFullInfo>>($"/api/monetization/get/full/");
	}
}
