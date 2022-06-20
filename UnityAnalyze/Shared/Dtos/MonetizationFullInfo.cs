namespace UnityApi.Shared.Dtos.Monetization;

public class MonetizationFullInfo
{
	public ICollection<MonetizationGameInfo> Games { get; set; } = new List<MonetizationGameInfo>();
	
	public double TotalProfit => Games.Sum(x => x.TotalProfit);
	
	public double TotalProfitFromClicks => Games.Sum(x => x.TotalProfitFromClicks);
	public double TotalProfitFromBanners => Games.Sum(x => x.TotalProfitFromBanners);
	public double TotalProfitFromRewardedVideo => Games.Sum(x => x.TotalProfitFromRewardedVideo);
	
	public double ProfitToday => Games.Sum(x => x.ProfitToday);
	
	public double ProfitYesterday => Games.Sum(x => x.ProfitYesterday);

	public double ProfitDelta => ((ProfitToday - ProfitYesterday) / ProfitYesterday) * 100;
	
	public double TodayFromClicks => Games.Sum(x => x.TodayProfitFromClicks);
	
	public double TodayFromBanners => Games.Sum(x => x.TodayProfitFromBanners);
	
	public double TodayFromRewardedVideo => Games.Sum(x => x.TodayProfitFromRewardedVideo);

	public IEnumerable<AllGamesDayInfo> TotalFromGames
	{
		get {
			foreach (IGrouping<DateTime, MonetizationDayInfo> monetizationDayInfos in Games.SelectMany(g => g.Days).GroupBy(d => d.Date))
			{
				yield return new AllGamesDayInfo()
				{
					Date = monetizationDayInfos.Key,
					FromBanners = monetizationDayInfos.Sum(m => m.FromBanners),
					FromClicks = monetizationDayInfos.Sum(m => m.FromClicks),
					FromRewardedVideo = monetizationDayInfos.Sum(m => m.FromRewardedVideo),
					Total = monetizationDayInfos.Sum(m => m.Total)
				};
			}
		}
	}
}

public class MonetizationGameInfo
{
	public string Name { get; set; }

	public ICollection<MonetizationDayInfo> Days { get; set; } = new List<MonetizationDayInfo>();
	
	public double TotalProfit => Days.Sum(x => x.Total);
	
	public double TotalProfitFromClicks => Days.Sum(x => x.FromClicks);
	
	public double TotalProfitFromBanners => Days.Sum(x => x.FromBanners);
	
	public double TotalProfitFromRewardedVideo => Days.Sum(x => x.FromRewardedVideo);

	public double ProfitToday => Today.Total;

	public MonetizationDayInfo Today => Days.First(d => d.Date == DateTime.Now.Date);
	
	public double ProfitYesterday => Days.First(d => d.Date == DateTime.Now.AddDays(-1).Date).Total;

	public double TodayProfitFromClicks => Today.FromClicks;
	
	public double TodayProfitFromBanners => Today.FromBanners;
	
	public double TodayProfitFromRewardedVideo => Today.FromRewardedVideo;
}

public class AllGamesDayInfo
{
	public DateTime Date { get; set; }
	
	public string DateStr => Date.ToString("dd.MM.yyyy");

	public double Total { get; set; }
	
	public double FromClicks { get; set; }
	
	public double FromRewardedVideo { get; set; }
	
	public double FromBanners { get; set; }
	}

public class MonetizationDayInfo
{
	public DateTime Date { get; set; }
	
	public ICollection<MonetizationCountryInfo> Countries { get; set; } = new List<MonetizationCountryInfo>();
	
	public double Total => Countries.Sum(c => c.Total);
	
	public double FromClicks => Countries.Sum(c => c.FromClicks);
	
	public double FromRewardedVideo => Countries.Sum(c => c.FromRewardedVideo);
	
	public double FromBanners => Countries.Sum(c => c.FromBanners);
}

public class MonetizationCountryInfo
{
	public string Name { get; set; }
	
	public double FromClicks { get; set; }
	
	public double FromRewardedVideo { get; set; }
	
	public double FromBanners { get; set; }
	
	public double Total => FromClicks + FromRewardedVideo + FromBanners;
}