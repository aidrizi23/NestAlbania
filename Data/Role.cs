using Microsoft.AspNetCore.Identity;

namespace NestAlbania.Data
{
    public class Role : IdentityRole
    {
        public string Description { get; set; }
    }
}
