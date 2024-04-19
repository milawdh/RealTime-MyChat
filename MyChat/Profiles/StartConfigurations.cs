using Domain.DataLayer.Contexts;
using Domain.DataLayer.Contexts.Base;
using Domain.DataLayer.UnitOfWorks;
using Domain.Entities;
using Domain.Profiles;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MyChat.Profiles
{
    public static class StartConfigurations
    {
        public static async Task ConfigureStartUps(this WebApplication app)
        {
            var IsPermissionSeedEnable = bool.Parse(app.Configuration["DataSeed:Permission"]);
            if (IsPermissionSeedEnable)
            {
                var context = app.Services.GetRequiredKeyedService<AppBaseDbContex>("StartUpContext");
                var contextFactory = app.Services.GetRequiredKeyedService<DbContextFactory>("StartUpContextFactory");


                await SyncPermissions(context);

                context.Dispose();
                contextFactory.Dispose();
            }
        }

        public static async Task SyncPermissions(AppBaseDbContex context)
        {
            var permissionsDbSet = context.Set<TblPermission>().ToList();

            var allPermissions = await ConfigPermissionSeedAsync(typeof(PermissionProfile));

            var permissionWillDelete = permissionsDbSet.Where(x => !allPermissions.Any(v => v.Name == x.Name)).AsEnumerable();
            context.RemoveRangeForce(permissionWillDelete);

            var permissionsWillAdd = allPermissions.Where(x => !permissionsDbSet.Any(v => v.Name == x.Name));
            context.AddRange(permissionsWillAdd);

            context.SaveChanges();
        }

        private static async Task<List<TblPermission>> ConfigPermissionSeedAsync(Type type, TblPermission parent = null)
        {
            var res = new List<TblPermission>();
            foreach (var item in type.GetNestedTypes())
            {
                foreach (var field in item.GetFields())
                {
                    var tblPermission = new TblPermission()
                    {
                        Name = field.GetValue(null).ToString(),
                        Parent = parent,
                    };

                    res.Add(tblPermission);
                    res.AddRange(await ConfigPermissionSeedAsync(item, tblPermission));
                }
            }
            return res;

        }
    }
}
