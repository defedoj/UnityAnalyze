namespace UnityAnalyze.Shared.Dtos;

public partial class MonetizationInfo
{
	public string GameName { get; set; }

	public ICollection<ReadMonetizationDto> Monetizations { get; set; }

	public IEnumerable<CountryInfo> DayCountryInfos
	{
		get {
			var todayMon = Monetizations.Where(m => m.CheckDate.Date == DateTime.Now.Date).ToList();

			if (todayMon.Count > 0)
			{
				for (int i = 0; i < todayMon[0].CountryProfits.Count; i++)
				{
					yield return new CountryInfo(todayMon[0].CountryProfits.ElementAt(i).CountryName,
						todayMon.Sum(t => t.CountryProfits.ElementAt(i).FromBanners),
						todayMon.Sum(t => t.CountryProfits.ElementAt(i).FromClicks),
						todayMon.Sum(t => t.CountryProfits.ElementAt(i).FromRewardedVideo));
				}
			} else yield break;

			
		}
	}
	
	public IEnumerable<CountryInfo> AllDaysCountryInfos
	{
		get {
			var todayMon = Monetizations.GroupBy(m => m.CheckDate.Date);
			
			foreach (IGrouping<DateTime, ReadMonetizationDto> readMonetizationDtos in todayMon)
			{
				for (int i = 0; i < readMonetizationDtos.ElementAt(0).CountryProfits.Count; i++)
				{
					yield return new CountryInfo(readMonetizationDtos.ElementAt(0).CountryProfits.ElementAt(i).CountryName,
						readMonetizationDtos.Sum(t => t.CountryProfits.ElementAt(i).FromBanners),
						readMonetizationDtos.Sum(t => t.CountryProfits.ElementAt(i).FromClicks),
						readMonetizationDtos.Sum(t => t.CountryProfits.ElementAt(i).FromRewardedVideo)) 
						{Date = readMonetizationDtos.Key};
				}
			}
		}
	}

	public IEnumerable<CountryInfo> AllTimeCountryInfo
	{
		get {
			for (int i = 0; i < Monetizations.ElementAt(0).CountryProfits.Count; i++)
			{
				yield return new CountryInfo(Monetizations.ElementAt(0).CountryProfits.ElementAt(i).CountryName,
					Monetizations.Sum(t => t.CountryProfits.ElementAt(i).FromBanners),
					Monetizations.Sum(t => t.CountryProfits.ElementAt(i).FromClicks),
					Monetizations.Sum(t => t.CountryProfits.ElementAt(i).FromRewardedVideo));
			}
		}
	}

	public IEnumerable<DayTotalInfo> DayTotalInfos
	{
		get {
			var monByDays = Monetizations.GroupBy(m => m.CheckDate.Date);

			foreach (IGrouping<DateTime, ReadMonetizationDto> monByDay in monByDays)
			{
				yield return new DayTotalInfo()
				{
					Date = monByDay.Key,
					Total = monByDay.Sum(m => m.AllCountriesSum),
					TotalFromBanners = monByDay.Sum(m => m.AllCountriesBannersSum),
					TotalFromClicks = monByDay.Sum(m => m.AllCountriesClicksSum),
					TotalFromRewardedVideos = monByDay.Sum(m => m.AllCountriesRewardedVideoSum)
				};
			}
		}
	}
}

public class CountryInfo
{
	public string Country;
	
	public DateTime Date { get; set; }
	
	public string StrDate => Date.ToString("dd.MM.yyyy");

	public double FromBanners { get; set; }

	public double FromClicks { get; set; }

	public double FromRewardedVideos { get; set; }
	
	public double Total => FromBanners + FromClicks + FromRewardedVideos;
	
	public CountryInfo(string country, double fromBanners, double fromClicks, double fromRewardedVideos)
	{
		Country = country;
		FromBanners = fromBanners;
		FromClicks = fromClicks;
		FromRewardedVideos = fromRewardedVideos;
	}
}

public class DayTotalInfo
{
	public DateTime Date { get; set; }
	
	public double Total { get; set; }
	
	public double TotalFromBanners { get; set; }
	
	public double TotalFromClicks { get; set; }
	
	public double TotalFromRewardedVideos { get; set; }

	public string DateStr => Date.ToString("dd.MM.yyyy");
}

public partial class MonetizationInfo
{
	public double Total => Monetizations.Sum(m => m.AllCountriesSum);
	
	public double TotalToday => Monetizations.Where(m => m.CheckDate.Date == DateTime.Now.Date).Sum(d => d.AllCountriesSum);
	public double TotalTodayFromBanners => Monetizations.Where(m => m.CheckDate.Date == DateTime.Now.Date).Sum(d => d.AllCountriesBannersSum);
	public double TotalTodayFromClicks => Monetizations.Where(m => m.CheckDate.Date == DateTime.Now.Date).Sum(d => d.AllCountriesClicksSum);
	public double TotalTodayFromVideo => Monetizations.Where(m => m.CheckDate.Date == DateTime.Now.Date).Sum(d => d.AllCountriesRewardedVideoSum);
	
	public double TotalFromClicks => Monetizations.Sum(m => m.AllCountriesClicksSum);
	public double TotalFromBanners => Monetizations.Sum(m => m.AllCountriesBannersSum);
	public double TotalFromRewardedVideos => Monetizations.Sum(m => m.AllCountriesRewardedVideoSum);

	public double DayDelta
	{
		get {
			var todaySum = TotalToday;
			var yesterdaySum = Monetizations.Where(m => m.CheckDate.Date == DateTime.Now.AddDays(-1).Date)
				.Sum(d => d.AllCountriesSum);
			
			return ((todaySum - yesterdaySum) / yesterdaySum) * 100;
		}
	}
}

public partial class ReadMonetizationDto
{
	public DateTime CheckDate { get; set; }

	public ICollection<ReadCountryProfitDto> CountryProfits { get; set; }
}

public partial class ReadMonetizationDto
{
	public double AllCountriesSum => CountryProfits.Sum(x => x.AllSum);
	
	public double AllCountriesBannersSum => CountryProfits.Sum(x => x.FromBanners);
	public double AllCountriesRewardedVideoSum => CountryProfits.Sum(x => x.FromRewardedVideo);
	public double AllCountriesClicksSum => CountryProfits.Sum(x => x.FromClicks);
}

public partial class ReadCountryProfitDto
{
	public string CountryName { get; set; }
	
	public double FromBanners { get; set; }
	
	public double FromRewardedVideo { get; set; }
	
	public double FromClicks { get; set; }
}

public partial class ReadCountryProfitDto
{
	public double AllSum => FromBanners + FromRewardedVideo + FromClicks;
}
