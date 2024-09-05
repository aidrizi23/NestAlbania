using Microsoft.AspNetCore.Identity;

namespace NestAlbania.Data
{
    public class ApplicationUser : IdentityUser<string>
    {
        public string? CustomUserName { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
