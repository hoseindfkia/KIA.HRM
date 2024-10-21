using DataLayer;
using FileService;
using Service.CalendarDay;
using Service.City;
using Service.Degree;
using Service.File;
using Service.Mongo;
using Service.Project;
using Service.ProjectAction;
using Service.ProjectActionAssignUser;
using Service.Province;
using Service.WorkReport.Leave;
using Service.WorkReport.Meeting;
using Service.WorkReport.Mission;
using Service.WorkReport.PreparationDocument;

namespace KIA.HRM.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection Services)
        {
            #region تزریق وابستگی

            Services.AddTransient<IUnitOfWorkContext, UnitOfWorkContext>();
            Services.AddTransient<IMongoService, MongoService>();
            Services.AddScoped<IProjectService, ProjectService>();
            Services.AddScoped<IProjectActionService, ProjectActionService>();
            Services.AddScoped<IDegreeService, DegreeService>();
            Services.AddScoped<IFileManagerService, FileManagerService>();
            Services.AddScoped<IProjectActionAssignUserService, ProjectActionAssignUserService>();
            Services.AddScoped<IFileService, Service.File.FileService>();
            Services.AddScoped<ICityService , CityService>();
            Services.AddScoped<IProvinceService , ProvinceService>();
            Services.AddScoped<ICalendarDayService ,CalendarDayService>();


            // WorkReport
            Services.AddScoped<ILeaveService, LeaveService>();
            Services.AddScoped<IMeetingService, MeetingService>();
            Services.AddScoped<IMissionService, MissionService>();
            Services.AddScoped<IPreparationDocumentService, PreparationDocumentService>();
            
            #endregion


            return Services;
        }
    }
}
