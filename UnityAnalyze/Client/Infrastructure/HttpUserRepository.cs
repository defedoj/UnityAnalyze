using System.Net.Http.Json;
using UnityAnalyze.Client.Infrastructure.Base;
using UnityAnalyze.Shared.ActionResult;
using UnityAnalyze.Shared.Api;
using UnityAnalyze.Shared.Dtos;
namespace UnityAnalyze.Client.Infrastructure;

public class HttpUserRepository : HttpBaseRepository
{
	public HttpUserRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
	{
	}

	public async Task<CustomActionResult<UserDto>> ConnectToApi(ConnectToApiData data)
	{
		using var response = await HttpClient.PostAsJsonAsync($"/api/user/", data);
		
		return await response.Content.ReadFromJsonAsync<CustomActionResult<UserDto>>();
	}
	
	public async Task<UserDto> GetUserAsync()
	{
		return await HttpClient.GetFromJsonAsync<UserDto>($"/api/user/");
	}
}
