using AutoMapper;
using UnityAnalyze.Server.Data;
using UnityAnalyze.Server.Infrastructure.Repositories.Base;
using UnityAnalyze.Shared.Dtos;
namespace UnityAnalyze.Server.Infrastructure.Repositories;

public class UserRepository : Repository
{
	public UserRepository(ApplicationDbContext ctx, IMapper mapper) : base(ctx, mapper)
	{
	}

	public async Task SetToken(string userId, UserDto dto)
	{
		var user = await Ctx.Users.FindAsync(userId);

		user.Token = dto.Token;
		user.ConnectedUsername = dto.UserName;

		Ctx.Users.Update(user);
		await Ctx.SaveChangesAsync();
	}

	public async Task<string> GetToken(string userId)
	{
		return (await Ctx.Users.FindAsync(userId)).Token;
	}

	public async Task<UserDto> GetUserAsync(string userId)
	{
		return Mapper.Map<UserDto>(await Ctx.Users.FindAsync(userId));
	}
}
