namespace UnityAnalyze.Shared.Dtos;

public class ReadPlayStoreDto
{
	public string Name { get; set; }
	
	public ICollection<ReadDownloadDto> Downloads { get; set; }
	
	public ICollection<ReadRateDto> Rates { get; set; }
}

public class ReadPlayStoreDtoExtension
{
	public string Name { get; set; }

	public DownloadsInfo DownloadsInfo { get; set; } = new DownloadsInfo();

	public RatesInfo RatesInfo { get; set; } = new RatesInfo();
}

public partial class DownloadsInfo
{
	public ICollection<ReadDownloadDto> Downloads { get; set; }

	public IEnumerable<DayDownloadsInfo> DayDownloadsInfos
	{
		get {
			var days = Downloads.GroupBy(d => d.CheckedAt.Date);

			foreach (IGrouping<DateTime, ReadDownloadDto> day in days)
			{
				yield return new DayDownloadsInfo
				{
					Date = day.Key,
					Downloads = day.Sum(d => d.Count)
				};
			}
		}
	}
}

public class DayDownloadsInfo
{
	public DateTime Date { get; set; }
	
	public string DateStr => Date.ToString("dd.MM.yyyy HH:mm");
	
	public int Downloads { get; set; }
}

public partial class DownloadsInfo
{
	public double DayDelta
	{
		get {
			var todayDates     = Downloads.Where(d => d.CheckedAt.Date == DateTime.Now.Date);
			var yesterdayDates = Downloads.Where(d => d.CheckedAt.Date == DateTime.Now.Date.AddDays(-1));

			var todaySum     = todayDates.Sum(d => d.Count);
			var yesterdaySum = yesterdayDates.Sum(d => d.Count);
		
			return (((double)todaySum - yesterdaySum) / yesterdaySum) * 100;
		}
	}

	public double DayDeltaBy(Country country)
	{
		var todayDates     = Downloads.Where(d => d.CheckedAt.Date == DateTime.Now.Date);
		var yesterdayDates = Downloads.Where(d => d.CheckedAt.Date == DateTime.Now.Date.AddDays(-1));

		int todaySum = 1;
		int yesterdaySum = 1;
		
		switch (country)
		{
			case Country.Ukraine:
				todaySum     = todayDates.Sum(d => d.FromUkraine);
				yesterdaySum = yesterdayDates.Sum(d => d.FromUkraine);
				break;
			case Country.USA:
				todaySum     = todayDates.Sum(d => d.FromUSA);
				yesterdaySum = yesterdayDates.Sum(d => d.FromUSA);
				break;
			case Country.China:
				todaySum     = todayDates.Sum(d => d.FromChina);
				yesterdaySum = yesterdayDates.Sum(d => d.FromChina);
				break;
		}
		
		return (((double)todaySum - yesterdaySum) / yesterdaySum) * 100;
	}
	
	public int TodayUkraine => Downloads.Where(d => d.CheckedAt.Date == DateTime.Now.Date).Sum(d => d.FromUkraine);
	
	public int TodayChina => Downloads.Where(d => d.CheckedAt.Date == DateTime.Now.Date).Sum(d => d.FromChina);
	
	public int TodayUsa => Downloads.Where(d => d.CheckedAt.Date == DateTime.Now.Date).Sum(d => d.FromUSA);
	
	public int TodayCount => Downloads.Where(d => d.CheckedAt.Date == DateTime.Now.Date).Sum(d => d.Count);
	
	
	public int TotalCount => Downloads.Sum(d => d.Count);
	
	public int TotalCountFromUkraine => Downloads.Sum(d => d.FromUkraine);
	
	public int TotalCountFromChina => Downloads.Sum(d => d.FromChina);
	
	public int TotalCountFromUSA => Downloads.Sum(d => d.FromUSA);
}

public partial class RatesInfo
{
	public ICollection<ReadRateDto> Rates { get; set; }
}

public partial class RatesInfo
{
	public double Average => Rates.Average(r => r.Value);
	
	public int TotalCount => Rates.Count;
	
	public int FiveStarsCount => Rates.Count(r => r.Value == 5);
	
	public int FourStarsCount => Rates.Count(r => r.Value == 4);
	
	public int ThreeStarsCount => Rates.Count(r => r.Value == 3);
	
	public int TwoStarsCount => Rates.Count(r => r.Value == 2);
	
	public int OneStarCount => Rates.Count(r => r.Value == 1);
}

public enum Country
{
	Ukraine,
	China,
	USA
}