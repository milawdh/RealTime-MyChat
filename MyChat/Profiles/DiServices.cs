using MyChat.PipeLine.HubFilters;
using ServiceLayer.Services.Caching;
using ServiceLayer.Services.Chat;
using ServiceLayer.Services.User;
using Domain.DataLayer.UnitOfWorks;
using Domain.DataLayer.Repository;
using Domain.DataLayer.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Domain.DataLayer.Contexts.Base;
using Domain.Entities;
using System.Reflection;
using ServiceLayer.Services.File;

namespace ServiceLayer.Profiles
{
    public static class DiServices
    {
        public static void RegisterInversionOfControlls(this IServiceCollection services)
        {
            services.AddScoped<Core>(sp => new Core(sp.GetRequiredService<AppBaseDbContex>()));
            services.AddScoped<DbContextFactory>();
            services.AddScoped(sp => sp.GetRequiredService<DbContextFactory>().CreateDbContext());
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserInfoContext>(sp => new UserInfoContext(sp.GetRequiredService<IHttpContextAccessor>(), new Core(sp.GetRequiredService<AppBaseDbContex>())));


            services.AddScoped<IUserLoginService, UserLoginService>();
            services.AddScoped<IChatServices, ChatService>();
            services.AddScoped<IMediaServcie, MediaService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IFileServerService, FileServerService>();

            services.AddKeyedSingleton<DbContextFactory>("StartUpContextFactory");
            services.AddKeyedSingleton("StartUpContext", (sp, a) => sp.GetRequiredKeyedService<DbContextFactory>("StartUpContextFactory").CreateDbContext());
            services.AddSingleton<ChatHubPipeLine>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IChatHubGroupManager, ChatHubGroupManager>();
            services.AddSingleton<ICacheManager, CacheManager>();
        }
    }
}
