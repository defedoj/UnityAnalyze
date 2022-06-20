namespace UnityAnalyze.Shared.Dtos;

public class GameDetailsDto
{
	public int Id { get; set; }
	
	public AndroidGameSettingsDto? AndroidGameSettings { get; set; }
	
	public string Name { get; set; }
}
