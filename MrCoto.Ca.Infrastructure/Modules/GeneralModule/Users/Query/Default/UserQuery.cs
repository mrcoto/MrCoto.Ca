using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MrCoto.Ca.Application.Common.Query;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Query.Default;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Query.Default.Response;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;
using MrCoto.Ca.Infrastructure.Common.Query;
using MrCoto.Ca.Infrastructure.Common.Query.Extensions;
using MrCoto.Ca.Infrastructure.Modules.GeneralModule.Users.Query.Default.Filters;
using MrCoto.Ca.Infrastructure.Modules.GeneralModule.Users.Query.Default.Sorts;

namespace MrCoto.Ca.Infrastructure.Modules.GeneralModule.Users.Query.Default
{
    public class UserQuery : BaseQuery<UserDto, User>, IUserQuery
    {
        public UserQuery(SimpleAgendaContext context, IPaginationBuilder paginationBuilder) : 
            base(context, paginationBuilder)
        {
            
        }
    
        protected override IQueryable<User> Query()
        {
            return Context.Users.AsNoTracking()
                .Include(x => x.Tenant)
                .Include(x => x.Role);
        }

        protected override Expression<Func<User, bool>> PreFilter(Expression<Func<User, bool>> expression, QueryBag queryBag)
        {
            return expression;
        }

        protected override Expression<Func<User, bool>> PostFilter(Expression<Func<User, bool>> expression, QueryBag queryBag)
        {
            return expression.And(x => x.DeletedAt == null);
        }

        protected override Dictionary<string, QueryFilter<User>> FilterMap()
        {
            return new Dictionary<string, QueryFilter<User>>()
            {
                [IUserQuery.FilterSearch] = new SearchUserFilter()
            };
        }
        
        protected override Dictionary<string, QuerySort<User>> SortMap()
        {
            return new Dictionary<string, QuerySort<User>>()
            {
                [IUserQuery.SortId] = new IdUserSort(),
                [IUserQuery.SortDescription] = new NameUserSort(),
                [IUserQuery.SortCreatedAt] = new CreatedAtUserSort(),
                [IUserQuery.SortUpdatedAt] = new UpdatedAtUserSort(),
            };
        }
        
        protected override UserDto Map(User user)
        {
            return new UserDto()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Tenant = new TenantDto()
                {
                    Id = user.Tenant.Id,
                    Code = user.Tenant.Code,
                    Name = user.Tenant.Name,
                },
                Role = new RoleDto()
                {
                    Id = user.Role.Id,
                    Code = user.Role.Code,
                    Name = user.Role.Name,
                },
                LoginAttempts = user.LoginAttempts,
                DisabledAccountAt = user.DisabledAccountAt,
                LastLoginAt = user.LastLoginAt,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }
    }

}