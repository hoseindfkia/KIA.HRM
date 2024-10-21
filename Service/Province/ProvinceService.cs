using DataLayer;
using DomainClass;
using Microsoft.EntityFrameworkCore;
using Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.City;
using ViewModel.Province;

namespace Service.Province
{
    public class ProvinceService : IProvinceService
    {

        private readonly IUnitOfWorkContext _Context;
        private readonly DbSet<ProvinceEntity> _Entity;
        public ProvinceService(IUnitOfWorkContext context)
        {
            _Context = context;
            _Entity = _Context.Set<ProvinceEntity>();
        }

        public async Task<Feedback<IList<ProvinceListViewModel>>> GettAllProvince()
        {
            var FbOut = new Feedback<IList<ProvinceListViewModel>>();
            var EntityList = await _Entity.AsNoTracking()
                                 .Select(x => new ProvinceListViewModel()
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
