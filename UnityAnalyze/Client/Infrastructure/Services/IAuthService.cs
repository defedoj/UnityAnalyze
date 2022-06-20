using UnityAnalyze.Shared.Auth;
namespace UnityAnalyze.Client.Infrastructure.Services;

public interface IAuthService
{
	Task Login(LoginRequest loginRequest);
	Task Register(RegisterRequest registerRequest);
	Task Logout();
	Task<CurrentUser> CurrentUserInfo();
}