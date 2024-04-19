using Domain.CustomExceptions;
using Domain.DataLayer.Contexts.Base;
using Domain.DataLayer.Repository;
using Domain.DataLayer.UnitOfWorks;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Security.Claims;

namespace Domain.DataLayer.Contexts
{
    public class DbContextFactory : IDbContextFactory<AppBaseDbContex> , IDisposable
    {
        private readonly string _userName;
        private readonly IDbContextFactory<AppBaseDbContex> _dbContextFactory;

        public DbContextFactory(IDbContextFactory<AppBaseDbContex> dbContextFactory, IHttpContextAccessor httpContextAccessor)
        {
            _dbContextFactory = dbContextFactory;
            _userName = httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier)?.Value;
        }
        public AppBaseDbContex CreateDbContext()
        {
            var context = _dbContextFactory.CreateDbContext();
            if (context.Any<TblUser>(x => x.UserName == _userName))
                context.User = new UserInfoContext(new Core(context), _userName);
            return context;
        }

        public void Dispose()
        {
        }
    }
}
