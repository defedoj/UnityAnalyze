using UnityAnalyze.Shared.ActionResult;
namespace UnityAnalyze.Client.Infrastructure;

public class ActionStatusService
{
	public event Action<string> Success;
	public event Action<string> Failure;

	public void CheckResult(CustomActionResult customActionResult)
	{
		if (customActionResult.Status == ActionResultStatus.Success)
		{
			Success?.Invoke(customActionResult.Message);
		} else
		{
			Failure?.Invoke(customActionResult.Message);
		}
	}
}


