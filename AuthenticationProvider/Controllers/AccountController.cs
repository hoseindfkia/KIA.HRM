using AuthenticationProvider.Models.User;
using AuthenticationProvider.Service.Message;
using AuthenticationProvider.ViewModel.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using Share;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthenticationProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly IMessageService _messageSender;
        private readonly IConfiguration _configuration;


        public AccountController(UserManager<UserEntity> userManager,
                                 SignInManager<UserEntity> signInManager,
                                 IMessageService messageSender,
                                 IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _messageSender = messageSender;
            _configuration = configuration;
        }

        /// <summary>
        /// ثبت نام
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST api/<AccountController>
        [HttpPost("Register")]
        public async Task<Feedback<ModelStateDictionary>> Register([FromBody] RegisterViewModel model)
        {
            var FbOut = new Feedback<ModelStateDictionary>();
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new UserEntity()
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        //  EmailConfirmed = true,
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        var emailConfiirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var EmailMessage = Url.Action("ConfirmEmail", "Account", new
                        {
                            username = user.UserName,
                            token
                        = emailConfiirmationToken
                        }, Request.Scheme);
                      //  await _messageSender.SendEmailAsync(model.Email, "Email confirmation", EmailMessage); //TODO: تنظیمات اوکی شود و ایمیل ارسال شود
                        FbOut.SetFeedback(Share.Enum.FeedbackStatus.RegisteredSuccessful, Share.Enum.MessageType.Info, ModelState, "");
                    }
                    else
                    {
                        string ErrorList = string.Join("||", result.Errors.Select(e => e.Description).ToList());

                        return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.CouldNotConnectToServer, Share.Enum.MessageType.Warninig, null, ErrorList);
                        //foreach (var itemErr in result.Errors)
                        //{
                        //  ModelState.AddModelError("", itemErr.Description);
                        //}
                    }
                }
            }
            catch (Exception Ex)
            {

                return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.CouldNotConnectToServer, Share.Enum.MessageType.Error, null, Ex.Message);
            }

            return FbOut;
        }

        /// <summary>
        /// ورود
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<Feedback<string>> Login([FromBody] LoginViewModel model)//, string returnUrl = null)
        {
            var FbOut = new Feedback<string>();
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);
                    if (result.Succeeded)
                    {
                        var user = new IdentityUser { UserName = model.UserName, NormalizedUserName = model.UserName.ToLower() };
                        var userModel = await _userManager.FindByNameAsync(user.UserName);
                        var Token = await GenerateJwtToken(userModel, _userManager);

                        // جهت ریدایریکت کردن کاربر به صفحه دلخواه
                        //if(string.IsNullOrEmpty(returnUrl))
                        //    if(Url.IsLocalUrl(returnUrl))   چک کردن این که آدرس بازگشت مربوط به همین سایت است یا خیر
                        //        return RedirectToAction("Index", "Home");
                        return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.FetchSuccessful, Share.Enum.MessageType.Info, Token, "");
                    }
                    if (result.IsLockedOut)
                    {
                        FbOut.SetFeedback(Share.Enum.FeedbackStatus.AccessDenied, Share.Enum.MessageType.Warninig, "", "اکانت شما به دلیل 5 بار ورود نا موفق به مدت 5 دقیقه قفل شده است.");
                        return FbOut;
                    }
                    ModelState.AddModelError("", "رمز عبور یا نام کاربری صحیح نیست");
                    FbOut.SetFeedback(Share.Enum.FeedbackStatus.DataIsNotFound, Share.Enum.MessageType.Error, "", "رمز عبور یا نام کاربری صحیح نیست");
                }
            }
            catch (Exception Ex)
            {
                FbOut.SetFeedback(Share.Enum.FeedbackStatus.DataIsNotFound, Share.Enum.MessageType.Error, "", Ex.Message);
            }

            return FbOut;
        }


        /// <summary>
        /// چک کردن تکراری نبودن ایمیل
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("IsEmailInUser")]
        public async Task<Feedback<string>> IsEmailInUser(string email)
        {
            var FbOut = new Feedback<string>();
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                FbOut.SetFeedback(Share.Enum.FeedbackStatus.FetchSuccessful, Share.Enum.MessageType.Info, "", "ایمیل معتبر می باشد");
            else
                FbOut.SetFeedback(Share.Enum.FeedbackStatus.AccessDenied, Share.Enum.MessageType.Warninig, "", "ایمیل وارد شده قبلا توسط شخص دیگری استفاده شده است.");

            return FbOut;
        }


        /// <summary>
        /// ایجاد یک توکن
        /// در صورت نیاز به این تابع در متد های دیگر ، می بایستی در یک کلاس مشترک منتقل شود  و در همه جا مورد استفاده قرار گیرد
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userManager"></param>
        /// <returns></returns>
        private async Task<string> GenerateJwtToken(UserEntity user, UserManager<UserEntity> userManager)
        {

            var roles = await userManager.GetRolesAsync(user);
            var claims = new List<Claim>
             {
                 new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };

            // Add role claims  
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(30),
                claims: claims,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        /// <summary>
        /// کاربردی ندارد چون باید در ری اکت این عملیات انجام شود
        /// توکن از لوکال استوریج حذف گردد
        /// </summary>
        /// <returns></returns>
        [HttpPost("LogOut")]
        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

    }
}
