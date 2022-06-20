using UnityAnalyze.Server.Infrastructure.Http.Base;
using UnityAnalyze.Shared.ActionResult;
using UnityAnalyze.Shared.Dtos;
using UnityApi.Shared.Dtos.Monetization;
namespace UnityAnalyze.Server.Infrastructure.Http;

public class HttpMonetizationRepository : HttpRepositoryBase
{
	public async Task<CustomActionResult<MonetizationInfo>> GetInfoAsync(string token, int gameId)
	{
		return await GetAndReadAsync<CustomActionResult<MonetizationInfo>>($"/api/monetization/get/by/{token}", gameId.ToString());
	}
	
	public async Task<CustomActionResult<MonetizationFullInfo>> GetFullInfoAsync(string token)
	{
		return await GetAndReadAsync<CustomActionResult<MonetizationFullInfo>>($"/api/monetization/get/all/by", token);
	}
}
