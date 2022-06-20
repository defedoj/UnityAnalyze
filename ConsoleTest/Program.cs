Console.WriteLine();

// HttpResponseMessage response = await client.GetAsync("https://localhost:7260/api/test/");
// 		
// var str = await response.Content.ReadAsStringAsync();
//
// PlayStoreInfo playStoreInfo = JsonSerializer.Deserialize<PlayStoreInfo>(str);
//
// var grouped = playStoreInfo?.downloadsInfo.GroupBy(d => d.checkedAt.Date);
//
// DownloadsFullInfo downloadsFullInfo = new DownloadsFullInfo();
//
// foreach (var apiDownloadsInfos in grouped)
// {
// 	downloadsFullInfo.CountryInfosByDate.Add(apiDownloadsInfos.Key, new CountryInfo()
// 	{
// 		FromChina = apiDownloadsInfos.Sum(a => a.fromChina),
// 		FromUkraine = apiDownloadsInfos.Sum(a => a.fromUkraine),
// 		FromUSA = apiDownloadsInfos.Sum(a => a.fromUSA),
// 	});
// }
//
// public class DownloadsFullInfo
// {
// 	public Dictionary<DateTime, CountryInfo> CountryInfosByDate { get; set; }
// }
//
// public class RatesFullInfo
// {
// 	
// }
//
// public class CountryInfo
// {
// 	public int FromUkraine { get; set; }
// 	
// 	public int FromUSA { get; set; }
// 	
// 	public int FromChina { get; set; }
// }
//
// public class PlayStoreInfo
// {
// 	public List<ApiDownloadsInfo> downloadsInfo { get; set; } 
//
// 	public List<RatesInfo> ratesInfo { get; set; } 
// }
//
// public class ApiDownloadsInfo
// {
// 	public DateTime checkedAt { get; set; }
//
// 	public int fromUkraine { get; set; }
// 		
// 	public int fromUSA { get; set; }
// 		
// 	public int fromChina { get; set; }
// }
//
// public class RatesInfo
// {
// 	public DateTime date { get; set; }
// 		
// 	public int rate { get; set; }
// }
