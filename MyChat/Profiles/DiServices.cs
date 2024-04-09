using MyChat.PipeLine.HubFilters;
using ServiceLayer.Services.Caching;
using ServiceLayer.Services.Chat;
using ServiceLayer.Services.User;
using Domain.DataLayer.UnitOfWorks;
using Domain.DataLayer.Repository;
using Domain.DataLayer.Contexts;

namespace ServiceLayer.Profiles
{
    public static class DiServices
    {
        public static void RegisterInversionOfControlls(this IServiceCollection services)
        {
            services.AddScoped<Core>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserInfoContext, UserInfoContext>();

            services.AddScoped<MyChatContextFactory>();
            services.AddScoped(sp => sp.GetRequiredService<MyChatContextFactory>().CreateDbContext());

            services.AddSingleton<ChatHubPipeLine>();
            services.AddScoped<IUserLoginService, UserLoginService>();
            services.AddScoped<IChatServices, ChatService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IChatHubGroupManager, ChatHubGroupManager>();
            services.AddSingleton<ICacheManager, CacheManager>();
        }
    }
}
