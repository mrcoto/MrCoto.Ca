using System;
using System.Linq.Expressions;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;
using MrCoto.Ca.Infrastructure.Common.Query;

namespace MrCoto.Ca.Infrastructure.Modules.GeneralModule.Users.Query.Default.Sorts
{
    public class CreatedAtUserSort : QuerySort<User>
    {
        public override Expression<Func<User, object>> Apply() => x => x.CreatedAt;
    }
}