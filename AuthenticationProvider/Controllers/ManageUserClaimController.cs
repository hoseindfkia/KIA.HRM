using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Share.Enum;
using Share;
using AuthenticationProvider.ViewModel.Claim;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using AuthenticationProvider.Repositories.Claim;
using AuthenticationProvider.Models.User;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthenticationProvider.Controllers
{
    //  [Tags("مدیریت Claim های کاربر")]
    [Route("api/[controller]")]
    [ApiController]
    public class ManageUserClaimController : ControllerBase
    {
        public UserManager<UserEntity> _userManager { get; }

        public ManageUserClaimController(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }


        /// <summary>
        ///  کاربر  Claim   لیست 
        ///  لیست تمامی کلیم ها آورده می شود. سپس آن کلیم هایی که کاربر دارد را ترو بر می گرداند
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet("GetUserClaims")]
        public async Task<Feedback<UserClaimViewModel>> GetUserClaims(string UserId)
        {
            var FbOut = new Feedback<UserClaimViewModel>();
            try
            {
                if (string.IsNullOrEmpty(UserId)) return FbOut.SetFeedbackNew(FeedbackStatus.DataIsNotFound, MessageType.Error, null, "کاربر یافت نشد");
                var user = await _userManager.FindByIdAsync(UserId);
                if (user == null) return FbOut.SetFeedbackNew(FeedbackStatus.DataIsNotFound, MessageType.Error, null, "کاربر یافت نشد");
                var allClaim = ClaimStore.AllClaim;
                var userClaims = await _userManager.GetClaimsAsync(user);

                var validClaims = allClaim.Select(c => new ClaimViewModel()
                {
                    ClaimType = c.Type,
                    IsSelected = userClaims.Any(x => x.Type == c.Type)
                }).ToList();
                var model = new UserClaimViewModel(UserId, validClaims.ToList());
                return FbOut.SetFeedbackNew(FeedbackStatus.FetchSuccessful, MessageType.Info, model, "دریافت اطلاعات کلیم با موفقیت انجام شد");
            }
            catch (Exception ex)
            {
                return FbOut.SetFeedbackNew(FeedbackStatus.CouldNotConnectToServer, MessageType.Error, null, ex.Message);
            }
        }


        /// <summary>
        /// به یک کاربر Claim نسبت دادن یک 
        /// </summary>
        /// <param name="userClaim"></param>
        /// <returns></returns>
        [HttpPost("AddUserToClaim")]
        public async Task<Feedback<string>> AddUserToClaim([FromBody] UserClaimViewModel userClaim)
        {
            var FbOut = new Feedback<string>();
            try
            {
                var user = await _userManager.FindByIdAsync(userClaim.UserId);
                if (user == null) return FbOut.SetFeedbackNew(FeedbackStatus.DataIsNotFound, MessageType.Error, null, "کاربر یافت نشد");
                var allClaim = ClaimStore.AllClaim;
                var userClaims = await _userManager.GetClaimsAsync(user);

                var claimSelected = userClaim.UserClaims.Where(c => c.IsSelected).ToList();
                var mustAdd = claimSelected.Where(c => !userClaims.Any(x => x.Type == c.ClaimType)).
                              Select(y => new Claim(y.ClaimType, true.ToString()))
                              .ToList();
                if (mustAdd.Any())
                {
                    var claimAdd = await _userManager.AddClaimsAsync(user, mustAdd);
                    if (!claimAdd.Succeeded)
                        return FbOut.SetFeedbackNew(FeedbackStatus.CouldNotConnectToServer, MessageType.Error, "", "خطا در اضافه کردن کلیم");
                }

                var claimUnSelected = userClaim.UserClaims.Where(c => !c.IsSelected).ToList();
                var mustRemove = claimUnSelected.Where(c => userClaims.Any(x => x.Type == c.ClaimType)).
                              Select(y => new Claim(y.ClaimType, true.ToString()))
                              .ToList();
                if (mustRemove.Any())
                {
                    var claimRemove = await _userManager.RemoveClaimsAsync(user, mustRemove);
                    if (!claimRemove.Succeeded)
                        return FbOut.SetFeedbackNew(FeedbackStatus.CouldNotConnectToServer, MessageType.Error, "", "خطا در حذف کردن کلیم");
                }


                return FbOut.SetFeedbackNew(FeedbackStatus.RegisteredSuccessful, MessageType.Info, "", "اطلاعات با موفقیت ویرایش شد ");

            }
            catch (Exception ex)
            {
                return FbOut.SetFeedbackNew(FeedbackStatus.CouldNotConnectToServer, MessageType.Error, null, ex.Message);
            }


        }


    }
}
