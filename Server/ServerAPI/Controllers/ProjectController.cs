using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Data;
using ServerAPI.Models;

namespace ServerAPI.Controllers
{
    [Authorize]
    [Route("api/projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private IDBRepository _db;

        public ProjectController(IDBRepository db)
        {
            _db = db;
        }
        
        [HttpPost("new")]
        public async Task<IActionResult> AddProject([FromBody]Project project)
        {

            NewResponseModel NewProjectResponseModel = new NewResponseModel();
            try
            {
                await _db.AddProject(project);
                NewProjectResponseModel.Message = "Success !!!";
                NewProjectResponseModel.CreatedId = project.Id;
                UserProject userProject = new UserProject() { ProjectId = project.Id, UserId = project.AdminId, RoleId = (await _db.GetRole("Admin")).Id };
                await _db.AddUserProject(userProject);
                return Ok(NewProjectResponseModel);
            }
            catch (Exception ex)
            {
                NewProjectResponseModel.Message = ex.Message;
                return BadRequest(NewProjectResponseModel);
            }
        }
        
        [HttpDelete("{projectId}")]
        public async System.Threading.Tasks.Task DeleteProject(int projectId)
        {
            await _db.DeleteTasksFromProject(projectId);
            await _db.DeleteProject(projectId);
        }

        [HttpDelete("tasks/{projectId}")]
        public async System.Threading.Tasks.Task DeleteTasksByProject(int projectId)
        {
            await _db.DeleteTasksFromProject(projectId);
        }

        [HttpDelete("users/{projectId}")]
        public async System.Threading.Tasks.Task DeleteUsersByProject(int projectId)
        {
            await _db.DeleteUsersFromProject(projectId);
        }

        [HttpGet("{projectId}")]
        public async Task<ActionResult<Project>> GetProject(int projectId)
            => await _db.GetProject(projectId);

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
            => await _db.GetProjects();

        [HttpGet("users/{userName}")]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjectsByUser(string userName)
            => await _db.GetProjectsFromUser(userName);

        [HttpGet("tasks/{userId}")]
        public async Task<ActionResult<IEnumerable<Data.Task>>> GetTasksByUser(int userId)
            => await _db.GetTasksFromUser(userId);
    }
}