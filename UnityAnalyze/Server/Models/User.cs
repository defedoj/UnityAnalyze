using Microsoft.AspNetCore.Identity;

namespace UnityAnalyze.Server.Models
{
    public class User : IdentityUser
    {
        public string CompanyName { get; set; }
        
        public string? Token { get; set; }
        
        public string? ConnectedUsername { get; set; }
    }
}
