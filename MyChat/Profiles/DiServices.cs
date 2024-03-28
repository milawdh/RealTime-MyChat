using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.HubServices;
using ServiceLayer.Services.User;
using Services.Services;
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
			services.AddScoped<IUserLoginService, UserLoginService>();
			services.AddScoped<IChatServices, ChatService>();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
		}
	}
}
