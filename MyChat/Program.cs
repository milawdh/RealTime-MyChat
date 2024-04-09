using ServiceLayer.Profiles;
using MyChat.Profiles;
using ServiceLayer.Hubs;
using Microsoft.EntityFrameworkCore;
using Domain.DataLayer.Contexts;


var builder = WebApplication.CreateBuilder(args);

#region RegisterServices
builder.Services.RegisterServices();
builder.Services.RegisterInversionOfControlls();
builder.Services.RegisterMapsterConfiguration();
#endregion

builder.Services.AddPooledDbContextFactory<MyChatContext>(
    o => o.UseSqlServer(builder.Configuration["ConnectionStrings:MyChatContext"]));

var app = builder.Build();

app.UseMiddlewareProfile();


app.MapHub<ChatHub>("/chatHub", options =>
{
    options.AllowStatefulReconnects = true;
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");



app.Run();

