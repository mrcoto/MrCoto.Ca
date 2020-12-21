using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MrCoto.Ca.Application.Common.Query.Filtering.Bag;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;
using MrCoto.Ca.Infrastructure.Common.Query;
using MrCoto.Ca.Infrastructure.Common.Query.Extensions;

namespace MrCoto.Ca.Infrastructure.Modules.GeneralModule.Users.Query.Default.Filters
{
    public class SearchUserFilter : QueryFilter<User>
    {

        public override Expression<Func<User, bool>> Apply(Expression<Func<User, bool>> expression,
            FilterParam filterParam)
        {
            var search = $"%{filterParam.Value}%";
            return expression.And(x =>
                EF.Functions.ILike(x.Name, search) ||
                EF.Functions.ILike(x.Email, search)
            );
        }
    }
}