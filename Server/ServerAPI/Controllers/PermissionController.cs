using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Data;
using ServerAPI.Models;

namespace ServerAPI.Controllers
{
    [Route("api/permissions")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private IDBRepository _db;

        public PermissionController(IDBRepository db)
        {
            _db = db;
        }

        [HttpPost("new")]
        public IActionResult AddPermission([FromBody]Permission permission)
        {

            NewResponseModel newPermissionResponseModel = new NewResponseModel();
            try
            {
                _db.AddPermission(permission);
                newPermissionResponseModel.Message = "Success !!!";
                newPermissionResponseModel.CreatedId = permission.Id;
                return Ok(newPermissionResponseModel);
            }
            catch (Exception ex)
            {
                newPermissionResponseModel.Message = ex.Message;
                return BadRequest(newPermissionResponseModel);
            }

        }
        
        [HttpGet("{permissionName}")]
        public async Task<ActionResult<Permission>> GetPermission(string permissionName)
            => await _db.GetPermission(permissionName);
        
        [HttpGet("roles/{roleId}")]
        public async Task<IEnumerable<Permission>> GetPermissionsByRole(int roleId)
            => await _db.GetPermissionsByRole(roleId);

    }
}