using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using Domain.DataLayer.Contexts.Base;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.DataLayer.Contexts;

public partial class MyChatContext : AppBaseDbContex
{
    public MyChatContext(DbContextOptions<MyChatContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

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