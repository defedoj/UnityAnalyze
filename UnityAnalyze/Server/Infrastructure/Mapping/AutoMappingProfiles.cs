using AutoMapper;
using UnityAnalyze.Server.Models;
using UnityAnalyze.Shared.ActionResult;
using UnityAnalyze.Shared.Auth;
using UnityAnalyze.Shared.Dtos;
namespace UnityAnalyze.Server.Infrastructure.Mapping;

public class AutoMappingProfiles : Profile
{
	public AutoMappingProfiles()
	{
		MapUser();
		MapPlayStore();
	}
	
	private void MapPlayStore()
	{
		CreateMap<ReadPlayStoreDto, ReadPlayStoreDtoExtension>().AfterMap((src, dest) => {
			dest.DownloadsInfo.Downloads = src.Downloads;
			dest.RatesInfo.Rates = src.Rates;
		});
		
		CreateMap<CustomActionResult<ReadPlayStoreDto>, CustomActionResult<ReadPlayStoreDtoExtension>>();
	}

	private void MapUser()
	{
		CreateMap<User, UserDto>().ReverseMap();
		CreateMap<RegisterRequest, User>().ForMember(dest => dest.UserName, opt =>
			opt.MapFrom(src => src.Email));
	}
}
