using Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Province;

namespace Service.Province
{
    public interface IProvinceService
    {
        public Task<Feedback<IList<ProvinceListViewModel>>> GettAllProvince();

        
    }
}
