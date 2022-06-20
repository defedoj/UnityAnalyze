namespace UnityAnalyze.Shared.Dtos;

public class CreateResponseDto
{
	public string Token { get; set; }
	
	public int GameId { get; set; }
	
	public int FeedbackId { get; set; }

	public ReadResponseDto Response { get; set; }
}
