namespace UnityAnalyze.Shared.Dtos;

public class ReadRateDto
{
	public int Id { get; set; }
	
	public int Value { get; set; }
	
	public DateTime Date { get; set; }
	
	public ReadFeedbackDto? Feedback { get; set; }
	
}
