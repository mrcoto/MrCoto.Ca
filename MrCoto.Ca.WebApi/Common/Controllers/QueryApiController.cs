using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MrCoto.Ca.Application.Common.CurrentUsers;
using MrCoto.Ca.Application.Common.Query;
using MrCoto.Ca.Application.Common.Query.Request;

namespace MrCoto.Ca.WebApi.Common.Controllers
{
    [ApiController]
    public abstract class QueryApiController : ControllerBase
    {
        protected ICurrentUserService CurrentUserService;
        protected readonly IHttpContextAccessor HttpContextAccessor;

        public QueryApiController(ICurrentUserService currentUserService, IHttpContextAccessor httpContextAccessor)
        {
            CurrentUserService = currentUserService;
            HttpContextAccessor = httpContextAccessor;
        }

        protected async Task<QueryBag> QueryBag() => Application.Common.Query.QueryBag.Of(QueryString(), await UserData());

        protected string QueryString()
        {
            var queryString = HttpContextAccessor.HttpContext.Request.QueryString.Value;
            return HttpUtility.UrlDecode(queryString);
        }

        protected async Task<UserData> UserData()
        {
            try
            {
                var currentUser = await CurrentUserService.CurrentUser();
                return currentUser != null ? new UserData() { UserId = currentUser.Id, TenantId = currentUser.Tenant.Id}
                    : new UserData();
            }
            catch (Exception)
            {
                return new UserData();
            }
        }
        
    }
}