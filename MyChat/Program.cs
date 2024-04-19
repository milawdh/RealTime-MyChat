using ServiceLayer.Profiles;
using MyChat.Profiles;
using ServiceLayer.Hubs;
using Microsoft.EntityFrameworkCore;
using Domain.DataLayer.Contexts;
using Domain.DataLayer.Contexts.Base;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;


var builder = WebApplication.CreateBuilder(args);

#region RegisterServices

builder.Services.AddPooledDbContextFactory<AppBaseDbContex>(
    o => o.UseSqlServer(builder.Configuration["ConnectionStrings:MainDb"]));

builder.Services.RegisterServices();

builder.Services.RegisterInversionOfControlls();

builder.Services.RegisterMapsterConfiguration();

#endregion

builder.Services.Configure<FormOptions>(options =>
{
    options.MemoryBufferThreshold = 1073741824;
    options.BufferBody = true;
    options.ValueLengthLimit = 1073741824;
    options.MultipartBodyLengthLimit = 1073741824;
});
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = 1073741824;
    options.Limits.MaxRequestBodySize = 1073741824;
});

var app = builder.Build();

app.UseMiddlewareProfile();

await app.ConfigureStartUps();

app.MapHub<ChatHub>("/chatHub", options =>
{
    options.AllowStatefulReconnects = false;
    options.CloseOnAuthenticationExpiration = true;
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");



app.Run();

