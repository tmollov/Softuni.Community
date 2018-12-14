using Microsoft.AspNetCore.Identity;

namespace Softuni.Community.Data.Models
{
    public class CustomUser : IdentityUser
    {

        public int UserInfoId { get; set; }

        public virtual UserInfo MyInfo { get; set; }
    }
}