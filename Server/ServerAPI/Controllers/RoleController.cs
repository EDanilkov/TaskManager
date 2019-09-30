using Microsoft.AspNetCore.Mvc;
using ServerAPI.Data;
using ServerAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerAPI.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IDBRepository _db;

        public RoleController(IDBRepository db)
        {
            _db = db;
        }

        [HttpPost("new")]
        public IActionResult AddRole([FromBody]Role role)
        {

            NewResponseModel newRoleResponseModel = new NewResponseModel();
            try
            {
                _db.AddRole(role);
                newRoleResponseModel.Message = "Success !!!";
                newRoleResponseModel.CreatedId = role.Id;
                return Ok(newRoleResponseModel);
            }
            catch (Exception ex)
            {
                newRoleResponseModel.Message = ex.Message;
                return BadRequest(newRoleResponseModel);
            }
            
        }
        
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
          => await _db.GetRoles();
        
        [HttpGet]
        public async Task<ActionResult<Role>> GetRole(string name, int id)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return await _db.GetRole(name);
            }
            if (id != 0)
            {
                return await _db.GetRole(id);
            }
            return null;
        }
        
        [HttpGet("{userName}/{projectId}")]
        public async Task<ActionResult<Role>> GetRoleByUser(string userName, int projectId)
            => await _db.GetRoleFromUser(userName, projectId);
    }
}