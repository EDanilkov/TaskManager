using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskThreading = System.Threading.Tasks.Task;
using System.Linq;
using ServerAPI.Models;

namespace ServerAPI.Data
{
    public class DBRepository : IDBRepository
    {
        TaskManagerContext _db = new TaskManagerContext();

        #region Task

        public async TaskThreading AddTask(Task task)
        {
            _db.Task.Add(task);
            await _db.SaveChangesAsync();
        }

        public async TaskThreading DeleteTask(int taskId)
        {
            Task task = await GetTask(taskId);
            _db.Task.Remove(task);
            await _db.SaveChangesAsync();
        }

        public async TaskThreading DeleteTask(Task task)
        {
            _db.Task.Remove(task);
            await _db.SaveChangesAsync();
        }

        public async TaskThreading DeleteProject(int projectId)
        {
            Project project = await GetProject(projectId);
            _db.Project.Remove(project);
            _db.SaveChanges();
        }

        public async Task<NewResponseModel> ChangeTask(UpdateTaskModel updateTaskModel)
        {
            NewResponseModel newTaskResponseModel = new NewResponseModel();
            Task task = await _db.Task.FirstAsync(c => c.Id == updateTaskModel.Task.Id);
            task.Name = updateTaskModel.TaskName;
            task.Description = updateTaskModel.TaskDescription;
            task.UserId = updateTaskModel.UserId;
            task.EndDate = updateTaskModel.TaskFinishDate;
            await _db.SaveChangesAsync();
            newTaskResponseModel.Message = "Success !!!";
            newTaskResponseModel.CreatedId = task.Id;
            return newTaskResponseModel;
        }

        public async Task<List<Task>> GetTasks()
            => await _db.Task.ToListAsync();

        public async Task<List<Task>> GetTasks(int userId, int projectId)
            => (await GetTasks()).Where(c => c.Project.Id == projectId && c.UserId == userId).ToList();

        public async Task<Task> GetTask(int taskId)
            => await _db.Task.Where(c => c.Id == taskId).FirstAsync();

        public async Task<List<Task>> GetTasksFromUser(int userId)
             => (await GetTasks()).Where(c => c.UserId == userId).ToList();

        public async Task<List<Task>> GetProjectTasksFromUser(int userId, int projectId)
            => (await GetTasks()).Where(c => c.ProjectId == projectId && c.UserId == userId).ToList();

        #endregion

        #region Role

        public async TaskThreading AddRole(Role role)
        {
            _db.Role.Add(role);
            await _db.SaveChangesAsync();
        }
        
        public async Task<List<Role>> GetRoles()
          => await _db.Role.ToListAsync();
        
        public async Task<Role> GetRole(string roleName)
         => await _db.Role.FirstAsync(c => string.Equals(c.Name, roleName));
        
        public async Task<Role> GetRole(int roleId)
         => await _db.Role.FirstAsync(c => c.Id == roleId);

        #endregion

        #region Permission

        public async TaskThreading AddPermission(Permission permission)
        {
            _db.Permission.Add(permission);
            await _db.SaveChangesAsync();
        }

        public async Task<Permission> GetPermission(int permissionId)
         => await _db.Permission.FirstAsync(c => c.Id == permissionId);

        public async Task<Permission> GetPermission(string permissionName)
         => await _db.Permission.FirstAsync(c => string.Equals(c.Name, permissionName));
        
        public async Task<List<Permission>> GetPermissionsByRole(int roleId)
        {
            List<RolePermission> rolePermissions = await GetRolePermissionByRoleId(roleId);
            List<Permission> permissions = new List<Permission>();
            foreach(RolePermission rolePermission in rolePermissions)
            {
                permissions.Add(await GetPermission(rolePermission.PermissionId));
            }
            return permissions;
        }

        #endregion

        #region UserProject

        public async TaskThreading AddUserProject(UserProject userProject)
        {
            _db.UserProject.Add(userProject);
            await _db.SaveChangesAsync();
        }
        
        public async TaskThreading DeleteUserProject(UserProject userProject)
        {
            _db.UserProject.Remove(userProject);
            await _db.SaveChangesAsync();
        }

        public async Task<List<UserProject>> GetUserProject()
            => await _db.UserProject.ToListAsync();
    
        public async Task<UserProject> GetUserProject(int userId, int projectId)
            => await _db.UserProject.FirstAsync(c => userId == c.UserId && projectId == c.ProjectId);

        public async Task<UserProject> GetUserProject(string userName, int projectId)
            => await _db.UserProject.FirstAsync(c => string.Equals(userName, c.User.Login) && projectId == c.Project.Id);

        public async Task<List<UserProject>> GetUserProjectByProjectId(int projectId)
            => await _db.UserProject.Where(c => c.ProjectId == projectId).ToListAsync();
        
        public async Task<List<UserProject>> GetUserProjectByUserId(int userId)
            => await _db.UserProject.Where(c => userId == c.UserId).ToListAsync();

        #endregion

        #region RolePermission

        public async TaskThreading AddRolePermission(RolePermission rolePermission)
        {
            _db.RolePermission.Add(rolePermission);
            await _db.SaveChangesAsync();
        }

        public async Task<List<RolePermission>> GetRolePermissionByRoleId(int roleId)
            => await _db.RolePermission.Where(c => c.RoleId == roleId).ToListAsync();


        #endregion
        
        #region Project
        
        public async TaskThreading AddProject(Project project)
        {
            _db.Project.Add(project);
            await _db.SaveChangesAsync();
        }

        public async TaskThreading DeleteTasksFromProject(int projectId)
        {
            List<Data.Task> tasks = await GetTasksFromProject(projectId);
            foreach (Data.Task task in tasks)
            {
                await DeleteTask(task);
            }
        }

        public async TaskThreading DeleteUsersFromProject(int projectId)
        {
            int adminId = (await GetProject(projectId)).AdminId;
            List<UserProject> userProjects = await _db.UserProject.Where(c => c.ProjectId == projectId && c.UserId != adminId).ToListAsync();
            foreach (UserProject userProject in userProjects)
            {
                await DeleteUserProject(userProject);
            }
        }
        
        public async TaskThreading DeleteUserFromProject(int userId, int projectId)
        {     
            await DeleteUserProject(await GetUserProject(userId, projectId));
        }
        
        public async TaskThreading DeleteTasksFromUser(int userId, int projectId)
        {
            List<Data.Task> tasks = await GetTasks(userId, projectId);
            foreach (Task task in tasks)
            {
                await DeleteTask(task);
            }
        }

        public async Task<Project> GetProject(int projectId)
            => await _db.Project.FirstAsync(c => c.Id == projectId);
        
        public async Task<List<Project>> GetProjects()
            => await _db.Project.ToListAsync();

        public async Task<List<User>> GetUsersFromProject(int projectId)
        {
            List<UserProject> userProjects = await GetUserProjectByProjectId(projectId);
            List<User> users = new List<User>();
            foreach (UserProject userProject in userProjects)
            {
                users.Add(await GetUser(userProject.UserId));
            }
            return users;
        }
        
        public async Task<List<Task>> GetTasksFromProject(int projectId)
            => (await _db.Task.ToListAsync()).Where(c => c.ProjectId == projectId).ToList();

        #endregion

        #region User
        
        public async TaskThreading AddUser(User user)
        {
            _db.User.Add(user);
            await _db.SaveChangesAsync();
        }
        
        public async Task<Role> GetRoleFromUser(string userName, int projectId)
            => await GetRole((await GetUserProject(userName, projectId)).RoleId);
        
        public async Task<List<User>> GetUsers()
          => await _db.User.ToListAsync();

        public async Task<User> GetUser(string name)
          => await _db.User.FirstAsync(c => string.Equals(name, c.Login));
        
        public async Task<User> GetUser(int id)
          => await _db.User.FirstAsync(c => id == c.Id);
        
        public async Task<List<Project>> GetProjectsFromUser(string userName)
        {
            List<UserProject> userProjects = await GetUserProjectByUserId((await GetUser(userName)).Id);
            List<Project> projects = new List<Project>();
            foreach (UserProject c in userProjects)
            {
                projects.Add(await GetProject(c.ProjectId));
            }
            return projects;
        }
        
        #endregion
    }
}
