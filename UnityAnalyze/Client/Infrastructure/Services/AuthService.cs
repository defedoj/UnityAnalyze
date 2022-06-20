using System.Net.Http.Json;
using UnityAnalyze.Client.Infrastructure.Base;
using UnityAnalyze.Shared.Auth;
namespace UnityAnalyze.Client.Infrastructure.Services;

public class AuthService : HttpBaseRepository, IAuthService
{
	public AuthService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
	{
	}
	
	public async Task<CurrentUser> CurrentUserInfo()
	{
		var result = await HttpClient.GetFromJsonAsync<CurrentUser>("api/auth/currentuserinfo");
		return result;
	}
	
	public async Task Login(LoginRequest loginRequest)
	{
		var result = await HttpClient.PostAsJsonAsync("api/auth/login", loginRequest);
		if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
		result.EnsureSuccessStatusCode();
	}
	
	public async Task Logout()
	{
		var result = await HttpClient.PostAsync("api/auth/logout", null);
		result.EnsureSuccessStatusCode();
	}
	
	public async Task Register(RegisterRequest registerRequest)
	{
		var result = await HttpClient.PostAsJsonAsync("api/auth/register", registerRequest);
		if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
		result.EnsureSuccessStatusCode();
	}
}
