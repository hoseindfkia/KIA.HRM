namespace AuthenticationProvider.ViewModel.Claim
{
    public class UserClaimViewModel
    {
        #region Constructor
        public UserClaimViewModel(){}

        public UserClaimViewModel(string userId, IList<ClaimViewModel> userClaim)
        {
            UserId = userId;    
            UserClaims = userClaim; 
        }
        #endregion
        public string UserId { get; set; }
        public IList<ClaimViewModel> UserClaims { get; set; } = new List<ClaimViewModel>();
    }
}
