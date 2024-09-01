using DataLayer;
using FileService;
using Microsoft.Extensions.DependencyInjection;
using Service.Degree;
using Service.Mongo;
using Service.Project;
using Service.ProjectAction;
using Service.ProjectActionAssignUser;

namespace KIA.HRM.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection Services)
        {
            Services.AddTransient<IUnitOfWorkContext, UnitOfWorkContext>();
            Services.AddTransient<IMongoService, MongoService>();
            Services.AddScoped<IProjectService, ProjectService>();
            Services.AddScoped<IProjectActionService, ProjectActionService>();
            Services.AddScoped<IDegreeService, DegreeService>();
            Services.AddScoped<IFileManagerService, FileManagerService>();
            Services.AddScoped<IProjectActionAssignUserService, ProjectActionAssignUserService>();
            return Services;
        }
    }
}
