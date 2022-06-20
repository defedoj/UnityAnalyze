namespace UnityAnalyze.Server.Infrastructure.Http.Base;

public abstract class HttpRepositoryBase
{
	private string _urlBase = "https://localhost:7260";

	protected async Task<TReturn> PostAndReadAsync<TReturn, TSerialized>(string url, TSerialized data)
	{
		using var client   = new HttpClient();
		using var response = await client.PostAsJsonAsync(_urlBase + url, data);
		
		return await response.Content.ReadFromJsonAsync<TReturn>();
	}

	protected async Task<TReturn> GetAndReadAsync<TReturn>(string url, string data)
	{
		using var client   = new HttpClient();
		return await client.GetFromJsonAsync<TReturn>($"{_urlBase + url}/{data}");
	}
}
