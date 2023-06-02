using Microsoft.AspNetCore.Identity;

namespace news_hogProj.Objects
{
    public class ApplicationUser : IdentityUser
    {
        public string? ProfilePhoto { get; set; } = "https://newshog.blob.core.windows.net/profile-photos/default.png";
    }
}
