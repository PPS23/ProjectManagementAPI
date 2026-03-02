using EFCoreAPI.Repositories;
using EFCoreAPI.Repositories.Interfaces;
using EFCoreAPI.Services;
using EFCoreAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace EFCoreAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            //Repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IStatusRepository, StatusRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<ITaskRepository, TaskRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<ITaskUserRelationRepository, TaskUserRelationRepository>();

            //Services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<IStatusService, StatusService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<ITaskService, TaskService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<ITaskUserRelationService, TaskUserRelationService>();

            return services;
        }
    }
}
