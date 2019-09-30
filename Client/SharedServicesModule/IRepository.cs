using SharedServicesModule.ResponseModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharedServicesModule
{
    public interface IRepository
    {
        Task<NewResponseModel> AddTask(Task task);
        Task<NewResponseModel> AddProject(Project project);
        Task<NewResponseModel> AddUser(User user);
        Task<NewResponseModel> AddUserProject(UserProject userProject);
        Task<NewResponseModel> AddRole(Role role);
        Task<NewResponseModel> AddPermission(Permission permission);
        Task<NewResponseModel> AddRolePermission(RolePermission rolePermission);
        System.Threading.Tasks.Task ChangeTask(Task task, string taskName, string taskDescription, int userId, DateTime taskFinishDate);
        System.Threading.Tasks.Task DeleteProject(int projectId);
        System.Threading.Tasks.Task DeleteTask(int taskId);
        System.Threading.Tasks.Task DeleteTasksFromProject(int projectId);
        System.Threading.Tasks.Task DeleteUsersFromProject(int projectId);
        System.Threading.Tasks.Task DeleteUserFromProject(int userId, int projectId);
        System.Threading.Tasks.Task DeleteTasksFromUser(int userId, int projectId);
        Task<Role> GetRoleFromUser(string userName, int projectId);
        Task<List<User>> GetUsers();
        Task<List<Role>> GetRoles();
        Task<User> GetUser(string name = null, int? id = 0);
        Task<Project> GetProject(int projectId);
        Task<Role> GetRole(string name = null, int? id = 0);
        Task<Permission> GetPermission(string permissionName);
        Task<List<Project>> GetProjects();
        Task<List<Task>> GetTasks();
        Task<Task> GetTask(int taskId);
        Task<int> GetProjectId(int projectId);
        Task<List<Project>> GetProjectsFromUser(string userName);
        Task<List<User>> GetUsersFromProject(int projectId);
        Task<List<Task>> GetTasksFromProject(int projectId);
        Task<List<Task>> GetProjectTasksFromUser(int userId, int projectId);
        Task<List<Task>> GetTasksFromUser(int userId);
        Task<List<Permission>> GetPermissionsFromRole(int roleId);
        Task<TokenResponseModel> GetToken(User user);
    }
}
