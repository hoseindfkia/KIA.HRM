using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationProvider.Models.User
{
    public class UserEntity :IdentityUser
    {
        /// <summary>
        /// کلید خارجی کد کاربر در پروژه اصلی
        /// </summary>
        public long KIAHRM_UserId { get; set; }
    }
}
