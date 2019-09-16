using BusinessLogicModule.ViewModel;
using Newtonsoft.Json;
using SharedServicesModule;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace BusinessLogicModule
{
    public class MainWindowViewModel : NavigateViewModel
    {
        IRepository _dbRepository = new DBRepository();

        public ICommand Loaded
        {
            get
            {
                return new DelegateCommand(async(obj) =>
                {
                    try
                    {
                        List<Role> roles = await _dbRepository.GetRoles();
                        if (roles.Count == 0)
                        {
                            Permission AddNewTask = new Permission() { Name = "AddNewTask" };
                            await _dbRepository.AddPermission(AddNewTask);
                            Permission ChangeTask = new Permission() { Name = "ChangeTask" };
                            await _dbRepository.AddPermission(ChangeTask);
                            Permission DeleteTask = new Permission() { Name = "DeleteTask" };
                            await _dbRepository.AddPermission(DeleteTask);
                            Permission VisibilityTask = new Permission() { Name = "VisibilityTask" };
                            await _dbRepository.AddPermission(VisibilityTask);
                            Permission DeleteProject = new Permission() { Name = "DeleteProject" };
                            await _dbRepository.AddPermission(DeleteProject);
                            Permission AddNewMembers = new Permission() { Name = "AddNewMembers" };
                            await _dbRepository.AddPermission(AddNewMembers);
                            Permission DeleteMembers = new Permission() { Name = "DeleteMembers" };
                            await _dbRepository.AddPermission(DeleteMembers);
                            Permission ChangeRole = new Permission() { Name = "ChangeRole" };
                            await _dbRepository.AddPermission(ChangeRole);
                            List<Permission> permissions = new List<Permission>() { (await _dbRepository.GetPermission("AddNewTask")), (await _dbRepository.GetPermission("ChangeTask")), (await _dbRepository.GetPermission("DeleteTask")), (await _dbRepository.GetPermission("VisibilityTask")), (await _dbRepository.GetPermission("DeleteProject")), (await _dbRepository.GetPermission("AddNewMembers")), (await _dbRepository.GetPermission("DeleteMembers")), (await _dbRepository.GetPermission("ChangeRole")) };
                            Role Admin = new Role() { Name = "Admin" };
                            Role Developer = new Role() { Name = "Developer"};
                            Role Manager = new Role() { Name = "Manager"};
                            await _dbRepository.AddRole(Admin);
                            await _dbRepository.AddRole(Developer);
                            await _dbRepository.AddRole(Manager);
                            foreach (Permission permission in permissions)
                            {
                                RolePermission rolePermission = new RolePermission() { RoleId = (await _dbRepository.GetRole("Admin")).Id, PermissionId = permission.Id };
                                await _dbRepository.AddRolePermission(rolePermission);
                            }
                            RolePermission rolePermission1 = new RolePermission() { RoleId = (await _dbRepository.GetRole("Manager")).Id, PermissionId = (await _dbRepository.GetPermission("AddNewTask")).Id };
                            RolePermission rolePermission2 = new RolePermission() { RoleId = (await _dbRepository.GetRole("Manager")).Id, PermissionId = (await _dbRepository.GetPermission("ChangeTask")).Id };
                            RolePermission rolePermission3 = new RolePermission() { RoleId = (await _dbRepository.GetRole("Manager")).Id, PermissionId = (await _dbRepository.GetPermission("DeleteTask")).Id };
                            RolePermission rolePermission4 = new RolePermission() { RoleId = (await _dbRepository.GetRole("Manager")).Id, PermissionId = (await _dbRepository.GetPermission("VisibilityTask")).Id };
                            await _dbRepository.AddRolePermission(rolePermission1);
                            await _dbRepository.AddRolePermission(rolePermission2);
                            await _dbRepository.AddRolePermission(rolePermission3);
                            await _dbRepository.AddRolePermission(rolePermission4);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message);
                    }
                });
            }
        }

        public ICommand OpenProfile
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if(System.Windows.Application.Current.Properties["UserName"] != null)
                    {

                        Navigate("Pages/Profile.xaml");
                    }
                    else
                    {
                        MessageBox.Show(Application.Current.Resources["m_identify"].ToString());
                    }
                });
            }
        }

        public ICommand OpenProject
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if (System.Windows.Application.Current.Properties["UserName"] != null)
                    {
                        Navigate("Pages/Projects.xaml");
                    }
                    else
                    {
                        MessageBox.Show("Вы не вошли в свой аккаунт");
                    }
                });
            }
        }

        public ICommand Exit
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    System.Windows.Application.Current.Properties["UserName"] = null;
                    Navigate("Pages/Reg.xaml");
                });
            }
        }
    }
}
