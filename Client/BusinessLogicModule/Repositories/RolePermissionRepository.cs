using BusinessLogicModule.Services;
using BusinessLogicModule.Interfaces;
using Newtonsoft.Json;
using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using System.Threading.Tasks;

namespace BusinessLogicModule.Repositories
{
    public class RolePermissionRepository : IRolePermissionRepository
    {
        public async Task<NewResponseModel> AddRolePermission(RolePermission rolePermission)
        {
            try
            {

                string json = JsonConvert.SerializeObject(rolePermission);
                return await RequestService.Post("https://localhost:44316/api/role-permissions/new", json);
            }
            catch
            {
                throw;
            }
        }
    }
}
