using MrCoto.Ca.Application.Common.Query;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Query.Default.Response;

namespace MrCoto.Ca.Application.Modules.GeneralModule.Users.Query.Default
{
    public interface IUserQuery : IQuery<UserDto>
    {
        public const string FilterSearch = "search";
        
        public const string SortId = "id";
        public const string SortDescription = "description";
        public const string SortCreatedAt = "createdAt";
        public const string SortUpdatedAt = "updatedAt";
    }
}