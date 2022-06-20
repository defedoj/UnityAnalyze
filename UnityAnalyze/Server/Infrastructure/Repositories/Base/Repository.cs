using AutoMapper;
using UnityAnalyze.Server.Data;
namespace UnityAnalyze.Server.Infrastructure.Repositories.Base;

public abstract class Repository
{
	protected readonly ApplicationDbContext Ctx;
	protected readonly IMapper Mapper;

	protected Repository(ApplicationDbContext ctx, IMapper mapper)
	{
		Ctx = ctx;
		Mapper = mapper;
	}

	protected async Task SaveChangesAsync()
	{
		await Ctx.SaveChangesAsync();
	}
}
