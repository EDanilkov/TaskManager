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
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private IDBRepository _db;

        public TaskController(IDBRepository db)
        {
            _db = db;
        }

        [HttpPost("new")]
        public IActionResult AddTask([FromBody]Data.Task task)
        {
            NewResponseModel newTaskResponseModel = new NewResponseModel();
            try
            {
                _db.AddTask(task);
                newTaskResponseModel.Message = "Success !!!";
                newTaskResponseModel.CreatedId = task.Id;
                return Ok(newTaskResponseModel);
            }
            catch (Exception ex)
            {
                newTaskResponseModel.Message = ex.Message;
                return BadRequest(newTaskResponseModel);
            }
        }
        
        [HttpPut]
        public async Task<IActionResult> ChangeTask([FromBody]UpdateTaskModel updateTaskModel)
        {
            NewResponseModel newTaskResponseModel = new NewResponseModel();
            try
            {
                Data.Task task = await _db.GetTask(updateTaskModel.Task.Id); //Data.Task.FirstAsync(c => c.Id == updateTaskModel.Task.Id);
                task.Name = updateTaskModel.TaskName;
                task.Description = updateTaskModel.TaskDescription;
                task.UserId = updateTaskModel.UserId;
                task.EndDate = updateTaskModel.TaskFinishDate;


                /*newTaskResponseModel =*/ await _db.ChangeTask(task);
                newTaskResponseModel.CreatedId = task.Id;
                newTaskResponseModel.Message = "Success !!!";
                return Ok(newTaskResponseModel);
            }
            catch (Exception ex)
            {
                newTaskResponseModel.Message = ex.Message;
                return BadRequest(newTaskResponseModel);
            }
        }

        [HttpDelete("{taskId}")]
        public async System.Threading.Tasks.Task DeleteTask(int taskId)
        {
            await _db.DeleteTask(taskId);
        }

        [HttpDelete("{userId}/{projectId}")]
        public async System.Threading.Tasks.Task DeleteTasksByUser(int userId, int projectId)
        {
            await _db.DeleteTasksFromUser(userId, projectId);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Data.Task>>> GetTasks()
          => await _db.GetTasks();

        [HttpGet("{taskId}")]
        public async Task<ActionResult<Data.Task>> GetTask(int taskId)
          => await _db.GetTask(taskId);
        
        [HttpGet("projects/{projectId}")]
        public async Task<ActionResult<IEnumerable<Data.Task>>> GetTasksFromProject(int projectId)
            => await _db.GetTasksFromProject(projectId);
        
        [HttpGet("{userId}/{projectId}")]
        public async Task<List<Data.Task>> GetProjectTasksByUser(int userId, int projectId)
            => await _db.GetProjectTasksFromUser(userId, projectId);

    }
}