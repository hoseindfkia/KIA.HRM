namespace AuthenticationProvider.ViewModel.Claim
{
    public class ClaimViewModel
    {
        #region Constructor
        public ClaimViewModel()
        {

        }
        public ClaimViewModel(string claimType)
        {
            ClaimType = claimType;
        }
        #endregion

        public string ClaimType { get; set; }
        public bool IsSelected { get; set; }
    }
}
