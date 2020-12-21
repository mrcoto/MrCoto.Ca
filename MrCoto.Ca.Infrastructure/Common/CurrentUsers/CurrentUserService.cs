using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MrCoto.Ca.Application.Common.CurrentUsers;
using MrCoto.Ca.Application.Common.CurrentUsers.Response;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;

namespace MrCoto.Ca.Infrastructure.Common.CurrentUsers
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SimpleAgendaContext _context;
        private CurrentUser _currentUser;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, SimpleAgendaContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _currentUser = null;
        }
        
        public async Task<CurrentUser> CurrentUser()
        {
            if (_currentUser != null)
            {
                return _currentUser;
            }

            if (_httpContextAccessor == null || _httpContextAccessor.HttpContext == null ||
                _httpContextAccessor.HttpContext.User == null)
            {
                return null;
            }

            var userIdStr = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userIdStr))
            {
                return null;
            }
            
            var userId = int.Parse(userIdStr);
            return await SetCurrentUser(userId);
        }

        public async Task<CurrentUser> SetCurrentUser(long id)
        {
            var user = await _context.Users.FindAsync(id);
            _currentUser = Map(user);
            return _currentUser;
        }

        private CurrentUser Map(User user)
        {
            var currentRole = new CurrentRole()
            {
                Id = user.RoleId,
                Code = user.Role.Code,
                Name = user.Role.Name
            };
            var currentTenant = new CurrentTenant()
            {
                Id = user.TenantId,
                Code = user.Tenant.Code,
                Name = user.Tenant.Name
            };
            return new CurrentUser()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Role = currentRole,
                Tenant = currentTenant
            };
        }
        
        
    }
}