using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public partial class MyChatContext : DbContext
{
    public MyChatContext()
    {
    }

    public MyChatContext(DbContextOptions<MyChatContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=MyChatDb;Integrated Security=True;Trust Server Certificate=True");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        //var entityTypes = AppDomain.CurrentDomain.GetAssemblies()
        //    .SelectMany(x => x.GetExportedTypes())
        //    .Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericType &&
        //                t.Namespace != null && t.Namespace.StartsWith("Domain.Entities"));

        //foreach (var entityType in entityTypes)
        //{
        //    modelBuilder.Entity(entityType);
        //}

        //var applyGenericMethod = typeof(ModelBuilder).GetMethod("ApplyConfiguration", BindingFlags.Instance | BindingFlags.Public);


        //var configurationTypes = AppDomain.CurrentDomain.GetAssemblies()
        //    .SelectMany(x => x.GetExportedTypes())
        //    .Where(x => x.Namespace.StartsWith("Domain.Configs"))
        //    .ToList();

        //foreach (var type in configurationTypes)
        //{
        //    foreach (var iface in type.GetInterfaces())
        //    {
        //        if (iface.IsConstructedGenericType && iface.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
        //        {


        //            var applyConcreteMethod = applyGenericMethod.MakeGenericMethod(iface.GenericTypeArguments[0]);
        //            applyConcreteMethod.Invoke(modelBuilder, new object[] { Activator.CreateInstance(type) });
        //            break;
        //        }
        //    }
        //    //dynamic configurationInstance = Activator.CreateInstance(type);
        //    //modelBuilder.ApplyConfiguration(configurationInstance);
        //}

        var assembly = Assembly.GetAssembly(typeof(TblChatRoom));
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        LoadEntities(modelBuilder);


    }
    private void LoadEntities(ModelBuilder modelBuilder)
    {
        var model = modelBuilder.Model;

        var entityMethod = typeof(ModelBuilder).GetMethod("Entity", new Type[] { });

        var entityTypes = model.GetEntityTypes().Where(x => x.ClrType.Namespace.StartsWith("Domain.Entities")).Select(x => x.ClrType).ToList();

        entityTypes.ForEach(t =>
        {
            entityMethod.MakeGenericMethod(t).Invoke(modelBuilder, new object[] { });
        });

    }
}