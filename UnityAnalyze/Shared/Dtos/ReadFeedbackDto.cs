namespace UnityAnalyze.Shared.Dtos;

public partial class ReadFeedbackDto
{
	public int Id { get; set; }

	public int RateId { get; set; }
	
	public string Sender { get; set; }
	
	public string Comment { get; set; }
	
	public DateTime CreatedAt { get; set; }

	public ReadResponseDto? Response { get; set; }
	
	public int Value { get; set; }
}

public partial class ReadFeedbackDto
{
    public ResponseStatus ResponseStatus => Response == null ? ResponseStatus.WaitingForResponse : ResponseStatus.Replied;
    
    public string CreatedAtString => CreatedAt.ToString("dd/MM/yyyy HH:mm");
}

public enum ResponseStatus
{
	WaitingForResponse,
	Replied
}

public class ReadResponseDto
{
	public string Text { get; set; }
	
	public string FromCompany { get; set; }
}