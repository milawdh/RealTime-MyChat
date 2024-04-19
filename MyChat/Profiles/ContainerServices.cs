using ElmahCore;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using Framework.CacheManagement;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using MyChat.PipeLine.HubFilters;
using ServiceLayer.Hubs;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Profiles
{
    public static class ContainerServices
    {

        public static void RegisterServices(this IServiceCollection services)
        {
            services.Configure<FormOptions>(options =>
            {
                options.MemoryBufferThreshold = 1073741824;
                options.BufferBody = true;
                options.ValueLengthLimit = 1073741824;
                options.MultipartBodyLengthLimit = 1073741824;
            });
            services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = 1073741824;
                options.Limits.MaxRequestBodySize = 1073741824;
            });

            services.AddControllersWithViews();
            services.AddMvc().AddRazorRuntimeCompilation();
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(opt =>
            {
                opt.LoginPath = "/Login/Index";
            });
            services.AddAntiforgery();
            services.AddSignalR(o =>
            {
                o.StatefulReconnectBufferSize = 1000;
            }).AddHubOptions<ChatHub>(options =>
            {
                options.AddFilter<ChatHubPipeLine>();
            });
            services.AddMemoryCache(opt =>
            {
                opt.SizeLimit = CacheDefualts.DefaultSizeLimit;
                opt.CompactionPercentage = CacheDefualts.DefaultCompactPercentage;
            });
            services.AddElmah<SqlErrorLog>(options =>
            {
                options.Path = "/Errors";

                options.ConnectionString = "Data Source=.;Initial Catalog=MyChatDb_Log;Integrated Security=True;TrustServerCertificate=True";
            });

        }
    }
}
