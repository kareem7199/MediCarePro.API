using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;

namespace MediCarePro.API.Hubs
{
	[Authorize(Roles = "Physician")]
	public class VisitHub : Hub
	{
		public override async Task OnConnectedAsync()
		{
			var user = Context.User;
			if (user?.Identity?.IsAuthenticated ?? false)
			{
				var claims = user.Claims.Select(c => $"{c.Type}: {c.Value}");
				var claimsList = string.Join(", ", claims);
				Console.WriteLine($"User {user.Identity.Name} connected with claims: {claimsList}");
			}
			else
			{
				Console.WriteLine("User not authenticated.");
			}
			await base.OnConnectedAsync();
		}
	}
}
