using ServiceLayer.Profiles;
using MyChat.Profiles;
using ServiceLayer.Hubs;


var builder = WebApplication.CreateBuilder(args);

#region RegisterServices
builder.Services.RegisterServices();
builder.Services.RegisterInversionOfControlls();
builder.Services.RegisterMapsterConfiguration();
#endregion

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

