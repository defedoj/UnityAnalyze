namespace UnityAnalyze.Shared.ActionResult;

public class CustomActionResult<TItem> : CustomActionResult
{
	public TItem Data { get; }
	
	public CustomActionResult(TItem data, string message, ActionResultStatus status) : base(message, status)
	{
		Data = data;
	}
}

public  class CustomActionResult
{
	public string Message { get; }
	
	public ActionResultStatus Status { get; }

	public CustomActionResult(string message, ActionResultStatus status)
	{
		Message = message;
		Status = status;
	}
}

public class Success<TItem> : CustomActionResult<TItem>
{
	public Success(TItem data) : base(data, "Success!", ActionResultStatus.Success)
	{
	}
}

public class Failure<TItem> : CustomActionResult<TItem>
{
	public Failure(TItem data) : base(data, "Failure!", ActionResultStatus.Failure)
	{
	}
}

public enum ActionResultStatus
{
	Success = 200,
	Failure = 400
}
