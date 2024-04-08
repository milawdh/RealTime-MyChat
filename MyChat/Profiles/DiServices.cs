using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MyChat.PipeLine.HubFilters;
using ServiceLayer.Services.Caching;
using ServiceLayer.Services.Chat;
using ServiceLayer.Services.User;
using Domain.DataLayer.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Profiles
{
    public static class DiServices
	{
		public static void RegisterInversionOfControlls(this IServiceCollection services)
		{
			services.AddScoped<Core>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IUserInfoContext, UserInfoContext>();
            services.AddSingleton<ChatHubPipeLine>();
            services.AddScoped<IUserLoginService, UserLoginService>();
			services.AddScoped<IChatServices, ChatService>();

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddSingleton<IChatHubGroupManager, ChatHubGroupManager>();
			services.AddSingleton<ICacheManager, CacheManager>();
		}
	}
}
