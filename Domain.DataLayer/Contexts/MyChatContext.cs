using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using Domain.DataLayer.Contexts.Base;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public partial class MyChatContext : AppBaseDbContex
{
    public MyChatContext() : base()
    {
    }

    public MyChatContext(DbContextOptions<MyChatContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=MyChatDb_New;Integrated Security=True;Trust Server Certificate=True");
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