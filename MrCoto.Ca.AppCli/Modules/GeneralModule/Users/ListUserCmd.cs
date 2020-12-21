using System.Collections.Generic;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using MrCoto.Ca.AppCli.Common;
using MrCoto.Ca.AppCli.Common.Utils;
using MrCoto.Ca.Application.Common.Query;
using MrCoto.Ca.Application.Common.Query.Request;
using MrCoto.Ca.Application.Common.Query.Response;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Query.Default;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Query.Default.Response;

namespace MrCoto.Ca.AppCli.Modules.GeneralModule.Users
{
    [Command(Name = "list", Description = "Lista los usuarios de la aplicación")]
    public class ListUserCmd : ICommandable
    {
        private readonly CommandUtil _commandUtil;
        private readonly IUserQuery _userQuery;

        private const int DefaultLimit = 10;

        public ListUserCmd(CommandUtil commandUtil, IUserQuery userQuery)
        {
            _commandUtil = commandUtil;
            _userQuery = userQuery;
        }

        public async Task OnExecute()
        {
            _commandUtil.PrintTitle("Lista de Usuarios");

            var search = _commandUtil.PromptString("Búsqueda (q! para salir): ");
            while (search != "q!")
            {
                var page = _commandUtil.PromptInt("Página", defaultAnswer: 1);

                var queryString = $"sort=-updatedAt&search=pattern:{search}";
                var userData = new UserData();
                var request = new PaginationRequest() { Limit = DefaultLimit, Page = page };
                var queryBag = QueryBag.Of(queryString, userData);
                var paginated = await _userQuery.Paginated(request, queryBag);
                PrintUsersDataTable(paginated);
                
                search = _commandUtil.PromptString("Búsqueda (q! para salir): ");
            }
        }

        private void PrintUsersDataTable(PaginationResponse<UserDto> paginated)
        {
            var headers = new List<string>()
            {
              "#", "Nombre", "Email", "TenantId", "TenantCode", "RoleCode", "Role", "Ult. Mod"  
            };
            var data = new List<List<string>>();
            foreach (var userDto in paginated.Data)
            {
                data.Add(new List<string>()
                {
                    userDto.Id.ToString(), 
                    userDto.Name, 
                    userDto.Email, 
                    userDto.Tenant.Id.ToString(),
                    userDto.Tenant.Code,
                    userDto.Role.Code,
                    userDto.Role.Name,
                    userDto.UpdatedAt.ToString("dd/MM/yyyy HH:mm:ss")
                });
            }

            _commandUtil.PrintTable(headers, data);

            var pages = "> " + paginated.CurrentPage + 
                        " [P: 1 - " + paginated.LastPage + 
                        "] [R: " + paginated.From + " - " + paginated.To + "]";
            
            _commandUtil.PrintInfo(pages);
        }
    }
}