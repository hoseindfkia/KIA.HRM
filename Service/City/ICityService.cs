using Share;
using ViewModel.City;

namespace Service.City
{
    public interface ICityService
    {
        Task<Feedback<IList<CityListViewModel>>> GetListByProviceIdAsync(int ProviceId);
    }
}
