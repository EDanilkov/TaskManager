using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Data;
using ServerAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerAPI.Controllers
{
    [Route("api/role-permissions")]
    [ApiController]
    public class RolePermissionController : ControllerBase
    {
        private IDBRepository _db;

        public RolePermissionController(IDBRepository db)
        {
            _db = db;
        }

        [HttpPost("new")]
        public async Task<IActionResult> AddRolePermission([FromBody]RolePermission rolePermission)
        {

            NewResponseModel newRolePermissionResponseModel = new NewResponseModel();
            try
            {
                await _db.AddRolePermission(rolePermission);
                newRolePermissionResponseModel.Message = "Success !!!";
                return Ok(newRolePermissionResponseModel);
            }
            catch (Exception ex)
            {
                newRolePermissionResponseModel.Message = ex.Message;
                return BadRequest(newRolePermissionResponseModel);
            }

        }

        [Authorize]
        [HttpGet("{roleId}")]
        public async Task<ActionResult<IEnumerable<RolePermission>>> GetRolePermissionByRoleId(int roleId)
          => await _db.GetRolePermissionByRoleId(roleId);
    }
}