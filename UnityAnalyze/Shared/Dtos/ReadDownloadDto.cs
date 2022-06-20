namespace UnityAnalyze.Shared.Dtos;

public class ReadDownloadDto
{
	public int Count { get; set; }
	
	public DateTime CheckedAt { get; set; }

	public string DateStr => CheckedAt.ToString("dd.MM.yyyy");
	
	public string DateTimeStr => CheckedAt.ToString("g");
	
	public int FromUkraine { get; set; }
	
	public int FromUSA { get; set; }
	
	public int FromChina { get; set; }
}

