using DataLayer;
using DomainClass;
using Microsoft.EntityFrameworkCore;
using Share;
using Share.Enum;
using ViewModel.Degree;

namespace Service.Degree
{
    public class DegreeService : IDegreeService
    {
        private readonly IUnitOfWorkContext _Context;
        private readonly DbSet<DegreeTypeEntity> _Entity;
        public DegreeService(IUnitOfWorkContext unitOfWorkContext)
        {
            _Context = unitOfWorkContext;
            _Entity = _Context.Set<DegreeTypeEntity>();
        }

        public async Task<Feedback<IList<DegreeGetAllDropDownViewModel>>> GetAllDropDownAsync()
        {
            var FbOut = new Feedback<IList<DegreeGetAllDropDownViewModel>>();
            //try
            //{
                var ModelList = await _Entity.AsNoTracking().ToListAsync();
                var ViewModelList = ModelList.Select(x => new DegreeGetAllDropDownViewModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                }).ToList();
                FbOut.SetFeedback(FeedbackStatus.FetchSuccessful, MessageType.Info, ViewModelList, "");
            //}
            //catch (Exception ex)
            //{
            //    FbOut.SetFeedback(FeedbackStatus.CouldNotConnectToServer, MessageType.Error, null, ex.Message);
            //}

            return FbOut;
        }

        public async Task<Feedback<IList<DegreeGetAllDropDownViewModel>>> Test()
        {
            var FbOut = new Feedback<IList<DegreeGetAllDropDownViewModel>>();
            var ModelList = await _Entity.AsNoTracking().ToListAsync();
            var ViewModelList = ModelList.Select(x => new DegreeGetAllDropDownViewModel()
            {
                Id = x.Id,
                Title = x.Title,
            }).ToList();
            FbOut.SetFeedback(FeedbackStatus.FetchSuccessful, MessageType.Info, ViewModelList, "");


            return FbOut;
        }

    }
}
