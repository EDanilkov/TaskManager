using BusinessLogicModule.API;
using Newtonsoft.Json;
using SharedServicesModule.ResponseModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using SharedServicesModule;
using System.Windows;
using System.Windows.Input;

namespace SharedServicesModule
{
    public class DBRepository : IRepository
    {
        private TaskManagerDb db = new TaskManagerDb();

        public async Task<NewResponseModel> AddTask(Task task)
        {
            try
            {
                string json = JsonConvert.SerializeObject(task);
                return await Requests.Post("https://localhost:44316/api/tasks/new", json);
            }
            catch
            {
                throw;
            }
            /*db.Task.Add(task);
            db.SaveChanges();*/
        }

        public async Task<NewResponseModel> AddProject(Project project)
        {
            try
            {

                string json = JsonConvert.SerializeObject(project);
                return await Requests.Post("https://localhost:44316/api/projects/new", json);
            }
            catch 
            {
                throw;
            }
            /*db.Project.Add(project);
            db.SaveChanges();*/
        }

        public async Task<NewResponseModel> AddPermission(Permission permission)
        {
            try
            {
                string json = JsonConvert.SerializeObject(permission);
                return await Requests.Post("https://localhost:44316/api/permissions/new", json);
            }
            catch
            {
                throw;
            }
        }

        public async Task<NewResponseModel> AddRolePermission(RolePermission rolePermission)
        {
            try
            {

                string json = JsonConvert.SerializeObject(rolePermission);
                return await Requests.Post("https://localhost:44316/api/rolepermissions/new", json);
            }
            catch
            {
                throw;
            }
            /*db.Project.Add(project);
            db.SaveChanges();*/
        }

        public async Task<NewResponseModel> AddUser(User user)
        {
            try
            {
                string json = JsonConvert.SerializeObject(user);
                return await Requests.Post("https://localhost:44316/api/users/new", json);
            }
            catch
            {
                throw;
            }

            /*db.User.Add(user);
            db.SaveChanges();*/
        }

        public async Task<NewResponseModel> AddUserProject(UserProject userProject)
        {
            try
            {

                string json = JsonConvert.SerializeObject(userProject);
                return await Requests.Post("https://localhost:44316/api/userprojects/new", json);
            }
            catch 
            {
                throw;
            }

            /*db.UserProject.Add(userProject);
            db.SaveChanges();*/
        }

        public async Task<NewResponseModel> AddRole(Role role)
        {
            try
            {
                string json = JsonConvert.SerializeObject(role);
                return await Requests.Post("https://localhost:44316/api/roles/new", json);

            }
            catch 
            {
                throw;
            }
            /*db.Role.Add(role);
            db.SaveChanges();*/
        }

        public async System.Threading.Tasks.Task ChangeTask(Task task, string taskName, string taskDescription, int userId, DateTime taskFinishDate)
        {
            try
            {

                UpdateTaskModel updateTaskModel = new UpdateTaskModel() { Task = task, TaskName = taskName, TaskDescription = taskDescription, UserId = userId, TaskFinishDate = taskFinishDate };
                string json = JsonConvert.SerializeObject(updateTaskModel);
                await Requests.Put("https://localhost:44316/api/tasks/", json);
            }
            catch 
            {
                throw;
            }
            /*task.Name = taskName;
            task.Description = taskDescription;
            task.UserId = userId;
            task.EndDate = taskFinishDate;
            await db.SaveChangesAsync();*/
        }

        public async System.Threading.Tasks.Task DeleteTask(int taskId)
        {
            try
            {
                await Requests.Delete("https://localhost:44316/api/tasks/" + taskId.ToString());

            }
            catch 
            {
                throw;
            }
        }

        public async System.Threading.Tasks.Task DeleteProject(int projectId)
        {
            try
            {
                await Requests.Delete("https://localhost:44316/api/projects/" + projectId.ToString());

            }
            catch 
            {
                throw;
            }
            /*List<Task> tasks = await GetTasksFromProject(projectId);
            foreach (Task task in tasks)
            {
                db.Task.Remove(task);
                db.SaveChanges();
            }
            db.Project.Remove(await GetProject(projectId));
            db.SaveChanges();*/
        }

        public async System.Threading.Tasks.Task DeleteTasksFromProject(int projectId)
        {
            try
            {
                await Requests.Delete("https://localhost:44316/api/projects/tasks/" + projectId.ToString());

            }
            catch 
            {
                throw;
            }
            /*List<Task> tasks = db.Task.Where(c => c.Project.Id == projectId).ToList();
            foreach(Task task in tasks)
            {
                db.Task.Remove(task);
            }*/
        }

        public async System.Threading.Tasks.Task DeleteUsersFromProject(int projectId)
        {
            try
            {
                await Requests.Delete("https://localhost:44316/api/projects/users/" + projectId.ToString());

            }
            catch 
            {
                throw;
            }
            
            /*int adminId = (await GetProject(projectId)).AdminId;
            List<UserProject> userProjects = db.UserProject.Where(c => c.ProjectId == projectId && c.UserId != adminId).ToList();
            foreach (UserProject userProject in userProjects)
            {
                db.UserProject.Remove(userProject);
                db.SaveChanges();
            }*/
        }

        public async System.Threading.Tasks.Task DeleteUserFromProject(int userId, int projectId)
        {
            try
            {
                await Requests.Delete("https://localhost:44316/api/users/" + userId.ToString() + "/" + projectId.ToString());

            }
            catch 
            {
                throw;
            }

            /*UserProject userProjects = await db.UserProject.FirstAsync(c => c.ProjectId == projectId && c.UserId == userId);
            db.UserProject.Remove(userProjects);
            db.SaveChanges();*/
        }

        public async System.Threading.Tasks.Task DeleteTasksFromUser(int userId, int projectId)
        {
            try
            {

                await Requests.Delete("https://localhost:44316/api/tasks/" + userId.ToString() + "/" + projectId.ToString());
            }
            catch 
            {
                throw;
            }

            /*List<Task> tasks = await GetProjectTasksFromUser(userId, projectId);
            foreach(Task task in tasks)
            {
                db.Task.Remove(task);
                db.SaveChanges();
            }*/
        }

        public async Task<Role> GetRoleFromUser(string userName, int projectId)
        {
            try
            {
                return await Requests.Get<Role>("https://localhost:44316/api/roles/" + userName + "/" + projectId.ToString());
            }
            catch 
            {
                throw;
            }
        }
        //=> (await GetUserProject(userName, projectId)).Role;

        public async Task<List<User>> GetUsers()
        {
            try
            {
                return await Requests.Get<List<User>>("https://localhost:44316/api/users/all");

            }
            catch 
            {
                throw;
            }
        }
        //await db.User.ToListAsync();

        public async Task<List<Role>> GetRoles()
        {
            try
            {
                return await Requests.Get<List<Role>>("https://localhost:44316/api/roles/all");//await db.Role.ToListAsync();

            }
            catch
            {
                throw;
            }
        }
        public async Task<User> GetUser(string name = null, int? id = 0)
        {
            try
            {
                return await Requests.Get<User>("https://localhost:44316/api/users?name=" + name + "&id=" + id);//await db.User.FirstAsync(c => string.Equals(name, c.Login));

            }
            catch
            {
                throw;
            }
        }

        public async Task<Project> GetProject(int projectId)
        {
            try
            {
                return await Requests.Get<Project>("https://localhost:44316/api/projects/" + projectId.ToString());//await db.Project.FirstAsync(c => c.Id == projectId);

            }
            catch
            {
                throw;
            }
        }

        public async Task<Role> GetRole(string name = null, int? id = 0)
        {
            try
            {
                return await Requests.Get<Role>("https://localhost:44316/api/roles?name=" + name + "&id=" + id);//await db.Role.FirstAsync(c => string.Equals(c.Name, roleName));

            }
            catch
            {
                throw;
            }
        }

        public async Task<Permission> GetPermission(string permissionName)
        {
            try
            {
                return await Requests.Get<Permission>("https://localhost:44316/api/permissions/" + permissionName);//await db.Permission.FirstAsync(c => string.Equals(c.Name, permissionName));

            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Project>> GetProjects()
        {
            try
            {
                return await Requests.Get<List<Project>>("https://localhost:44316/api/projects/all"); //await db.Project.ToListAsync();

            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Task>> GetTasks()
        {
            try
            {
                return await Requests.Get<List<Task>>("https://localhost:44316/api/tasks/all"); //await db.Task.ToListAsync();

            }
            catch
            {
                throw;
            }
        }

        public async Task<Task> GetTask(int taskId)
        {
            try
            {
                return await Requests.Get<Task>("https://localhost:44316/api/tasks/" + taskId.ToString()); //await db.Task.Where(c => c.Id == taskId && projectId == c.Project.Id).FirstAsync();

            }
            catch
            {
                throw;
            }
        }
        public async Task<int> GetProjectId(int projectId)
        {
            try
            {
                return await Requests.Get<int>("https://localhost:44316/api/projects/getprojectid/" + projectId.ToString());//=> (await db.Project.Where(c => c.Id == projectId).FirstAsync()).Id;

            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Project>> GetProjectsFromUser(string userName)
        {
            try
            {
                return await Requests.Get<List<Project>>("https://localhost:44316/api/projects/users/" + userName);
            }
            catch
            {
                throw;
            }
            /*int userId = await GetUserId(userName);
            List<UserProject> userProject = await db.UserProject.Where(c => c.UserId == userId).ToListAsync();
            List<Project> projects = new List<Project>();
            foreach(UserProject c in userProject)
            {
                projects.Add(c.Project);
            }  
            return projects;*/
        }

        public async Task<List<User>> GetUsersFromProject(int projectId)
        {
            try
            {
                return await Requests.Get<List<User>>("https://localhost:44316/api/users/" + projectId.ToString());

            }
            catch
            {
                throw;
            }
            /*
            List<UserProject> userProject = await db.UserProject.Where(c => c.Project.Id == projectId).ToListAsync();
            List<User> users = new List<User>();
            foreach (UserProject c in userProject)
            {
                users.Add(c.User);
            }
            return users;*/
        }

        public async Task<List<Task>> GetTasksFromProject(int projectId)
        {
            try
            {
                return await Requests.Get<List<Task>>("https://localhost:44316/api/tasks/projects/" + projectId.ToString());//=> (await GetTasks()).Where(c => c.Project.Id == projectId).ToList();

            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Task>> GetProjectTasksFromUser(int userId, int projectId)
        {
            try
            {
                return await Requests.Get<List<Task>>("https://localhost:44316/api/tasks/" + userId.ToString() + "/" + projectId.ToString());//=> (await GetTasks()).Where(c => c.Project.Id == projectId && c.UserId == userId).ToList();

            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Task>> GetTasksFromUser(int userId)
        {
            try
            {
                return await Requests.Get<List<Task>>("https://localhost:44316/api/projects/tasks/" + userId.ToString());//=> (await GetTasks()).Where(c => c.UserId == userId).ToList();

            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Permission>> GetPermissionsFromRole(int roleId)
        {
            try
            {
                return await Requests.Get<List<Permission>>("https://localhost:44316/api/permissions/roles/" + roleId.ToString());

            }
            catch
            {
                throw;
            }
        }

        public async Task<TokenResponseModel> GetToken(User user)
        {
            string json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync("https://localhost:44316/api/accountes/token", content);
            TokenResponseModel tokenResponseModel = JsonConvert.DeserializeObject<TokenResponseModel>(await response.Content.ReadAsStringAsync());
            Properties.Resources.Token = tokenResponseModel.access_token;
            return tokenResponseModel;
        }

    }
}
