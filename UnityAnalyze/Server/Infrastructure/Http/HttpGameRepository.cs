using UnityAnalyze.Server.Infrastructure.Http.Base;
using UnityAnalyze.Shared.ActionResult;
using UnityAnalyze.Shared.Dtos;

namespace UnityAnalyze.Server.Infrastructure.Http;

public class HttpGameRepository : HttpRepositoryBase
{
	public async Task<CustomActionResult<IEnumerable<ReadGameDto>>> GetAllAsync(string token)
	{
		return await GetAndReadAsync<CustomActionResult<IEnumerable<ReadGameDto>>>("/api/game/all-by", token);
	}
	
	public async Task<CustomActionResult<GameDetailsDto>> GetGameDetailsAsync(string token, int gameId)
	{
		return await GetAndReadAsync<CustomActionResult<GameDetailsDto>>($"/api/game/details-by/{token}", gameId.ToString());
	}
}
