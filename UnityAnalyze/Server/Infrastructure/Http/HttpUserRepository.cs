using UnityAnalyze.Server.Infrastructure.Http.Base;
using UnityAnalyze.Shared.ActionResult;
using UnityAnalyze.Shared.Api;
using UnityAnalyze.Shared.Dtos;
namespace UnityAnalyze.Server.Infrastructure.Http;

public class HttpUserRepository : HttpRepositoryBase
{
	public async Task<CustomActionResult<UserDto>> ConnectToApiAsync(ConnectToApiData data)
	{
		return await PostAndReadAsync<CustomActionResult<UserDto>, ConnectToApiData>("/api/user/connect", data);
	}
}
