namespace UnityApi.Shared.Dtos.PlayStore;

public class PlayStoreAllGamesInfo
{
	public ICollection<DayGameInfo> Games { get; set; } = new List<DayGameInfo>();
	
	public int DownloadsToday => Games.Sum(x => x.DownloadsToday);

	public int DownloadsYesterday => Games.Sum(x => x.DownloadsYesterday);

	public double DayDelta => ((double)(DownloadsToday - DownloadsYesterday) / DownloadsYesterday) * 100;
	
	public int TotalDownloads => Games.Sum(x => x.TotalDownloads);
	
	public double RatesAverageToday => Games.Sum(g => g.RatesAverageToday) / Games.Count;
	
	public double RatesCountToday => Games.Sum(g => g.RatesCountToday);

	public double RatesTotalCount => Games.Sum(r => r.RatesTotalCount);

	public double RatesTotalAverage => Games.Sum(r => r.RatesTotalAverage) / Games.Count;

	public IEnumerable<DownloadDayInfo> InfoByDays
	{
		get {
			var days = Games.SelectMany(g => g.DownloadDayInfos).GroupBy(d => d.Date);

			foreach (IGrouping<DateTime, DownloadDayInfo> day in days)
			{
				yield return new DownloadDayInfo()
				{
					Date = day.Key,
					FromChina = day.Sum(d => d.FromChina),
					FromUkraine = day.Sum(d => d.FromUkraine),
					FromUSA = day.Sum(d => d.FromUSA)
				};
			}
		}
	}
}

public class DayGameInfo
{
	public string Name { get; set; }
	
	public ICollection<DownloadDayInfo> DownloadDayInfos { get; set; }
	
	public ICollection<DayRateInfo> DayRateInfos { get; set; }
	
	private DayRateInfo RatesToday => DayRateInfos.FirstOrDefault(r => r.Date == DateTime.Now.Date);

	public double RatesAverageToday => RatesToday.Average;
	
	public double RatesCountToday => RatesToday.Count;

	public double RatesTotalCount => DayRateInfos.Sum(r => r.Count);

	public double RatesTotalAverage =>  DayRateInfos.Sum(r => r.Average) / DayRateInfos.Count;

	public int DownloadsToday
	{
		get {
			var today = DownloadDayInfos.FirstOrDefault(d => d.Date.Date == DateTime.Now.Date);

			if (today == null) return 0;

			return today.Count;
		}
	}
	
	public int DownloadsYesterday
	{
		get {
			var yesterday = DownloadDayInfos.FirstOrDefault(d => d.Date.Date == DateTime.Now.AddDays(-1).Date);

			if (yesterday == null) return 0;

			return yesterday.Count;
		}
	}

	public DownloadDayInfo AllTime
	{
		get {
			return new DownloadDayInfo()
			{
				FromChina = DownloadDayInfos.Sum(x => x.FromChina),
				FromUkraine = DownloadDayInfos.Sum(x => x.FromUkraine),
				FromUSA = DownloadDayInfos.Sum(x => x.FromUSA)
			};
		}
	}
	
	public DownloadDayInfo Today => DownloadDayInfos.FirstOrDefault(d => d.Date.Date == DateTime.Now.Date);

	public int TotalDownloads => DownloadDayInfos.Sum(x => x.Count);
}

public class DownloadDayInfo
{
	public DateTime Date { get; set; }
	
	public string DateStr => Date.ToString("dd.MM.yyyy");

	public int FromUkraine { get; set; }
	
	public int FromUSA { get; set; }
	
	public int FromChina { get; set; }

	public int Count => FromChina + FromUkraine + FromUSA;
}

public class DayRateInfo
{
	public DateTime Date { get; set; }
	
	public int Count { get; set; }
	
	public double Average { get; set; }
}




