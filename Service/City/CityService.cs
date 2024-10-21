using DataLayer;
using DomainClass;
using DomainClass.WorkReport;
using Microsoft.EntityFrameworkCore;
using Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.City;

namespace Service.City
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWorkContext _Context;
        private readonly DbSet<CityEntity> _Entity;
        public CityService(IUnitOfWorkContext context)
        {
            _Context = context;
            _Entity = _Context.Set<CityEntity>();
        }
      

        public async Task<Feedback<IList<CityListViewModel>>> GetListByProviceIdAsync(int ProviceId)
        {
            var FbOut = new Feedback<IList<CityListViewModel>>();
            var EntityList =await _Entity.Where(x => x.ProvinceId == ProviceId)
                                  .AsNoTracking()
                                  .Select(x => new CityListViewModel()
                                  {
                                      Id = x.Id,
                                      Title = x.Title,
                                  }).ToListAsync();
            if (EntityList.Any())
                return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.FetchSuccessful, Share.Enum.MessageType.Info, EntityList, "");
            return FbOut.SetFeedbackNew(Share.Enum.FeedbackStatus.DataIsNotFound, Share.Enum.MessageType.Warninig, null, "محتوایی یافت نشد");

        }
    }
}
