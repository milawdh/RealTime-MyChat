using Domain.DataLayer.Contexts.Base;
using Domain.DataLayer.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.DataLayer.Contexts
{
    public class MyChatContextFactory : IDbContextFactory<MyChatContext>
    {
        private readonly IUserInfoContext _userInfoContext;
        private readonly IDbContextFactory<MyChatContext> _dbContextFactory;

        public MyChatContextFactory(IUserInfoContext userInfoContext, IDbContextFactory<MyChatContext> dbContextFactory)
        {
            _userInfoContext = userInfoContext;
            _dbContextFactory = dbContextFactory;
        }
        public MyChatContext CreateDbContext()
        {
            var context = _dbContextFactory.CreateDbContext();
            //context.UserInfoContext = _userInfoContext;
            return context;
        }
    }
}
