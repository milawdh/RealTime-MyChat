using Domain.Entities.File;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataLayer.Contexts.FileDbContext
{
    public class FileDbContext : DbContext
    {
        private string _connectionString;
        public FileDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        public FileDbContext(DbContextOptions<FileDbContext> options, string connectionString)
        : base(options)
        {
            _connectionString = connectionString;
        }

        public virtual DbSet<TblFile> TblFiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblFile>(entity =>
            {
                entity.HasKey(x => x.Id);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
