namespace UnityAnalyze.Client.Infrastructure.Base;

public class HttpBaseRepository
{
	protected readonly HttpClient HttpClient;
	
	public HttpBaseRepository(IHttpClientFactory httpClientFactory)
	{
		HttpClient = httpClientFactory.CreateClient("public");
	}
}
