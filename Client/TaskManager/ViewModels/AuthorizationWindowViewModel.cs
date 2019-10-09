using BusinessLogicModule.Interfaces;
using BusinessLogicModule.Interfaces;
using NLog;
using SharedServicesModule;
using SharedServicesModule.Models;
using SharedServicesModule.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace UIModule.ViewModels
{
    public class AuthorizationWindowViewModel : NavigateViewModel
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static Action CloseAction { get; set; }
        IUserRepository _userRepository;
        IRoleRepository _roleRepository;
        IPermissionRepository _permissionRepository;
        IRolePermissionRepository _rolePermissionRepository;

        public AuthorizationWindowViewModel()
        {
            _userRepository = new UserRepository();
            _roleRepository = new RoleRepository();
            _permissionRepository = new PermissionRepository();
            _rolePermissionRepository = new RolePermissionRepository();
        }

        #region Properties
        
        private string _login;
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        private string _textError;
        public string TextError
        {
            get { return _textError; }
            set
            {
                _textError = value;
                OnPropertyChanged();
            }
        }

        private Brush _colorError;
        public Brush ColorError
        {
            get { return _colorError; }
            set
            {
                _colorError = value;
                OnPropertyChanged();
            }
        }


        private Visibility _visibilityError = Visibility.Hidden;
        public Visibility VisibilityError
        {
            get { return _visibilityError; }
            set
            {
                _visibilityError = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Methods

        public ICommand Loaded
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        List<Role> roles = await _roleRepository.GetRoles();
                        if (roles.Count == 0)
                        {
                            Permission AddNewTask = new Permission() { Name = "AddNewTask" };
                            await _permissionRepository.AddPermission(AddNewTask);
                            Permission ChangeTask = new Permission() { Name = "ChangeTask" };
                            await _permissionRepository.AddPermission(ChangeTask);
                            Permission DeleteTask = new Permission() { Name = "DeleteTask" };
                            await _permissionRepository.AddPermission(DeleteTask);
                            Permission VisibilityTask = new Permission() { Name = "VisibilityTask" };
                            await _permissionRepository.AddPermission(VisibilityTask);
                            Permission DeleteProject = new Permission() { Name = "DeleteProject" };
                            await _permissionRepository.AddPermission(DeleteProject);
                            Permission AddNewMembers = new Permission() { Name = "AddNewMembers" };
                            await _permissionRepository.AddPermission(AddNewMembers);
                            Permission DeleteMembers = new Permission() { Name = "DeleteMembers" };
                            await _permissionRepository.AddPermission(DeleteMembers);
                            Permission ChangeRole = new Permission() { Name = "ChangeRole" };
                            await _permissionRepository.AddPermission(ChangeRole);
                            List<Permission> permissions = new List<Permission>() { (await _permissionRepository.GetPermission("AddNewTask")), (await _permissionRepository.GetPermission("ChangeTask")), (await _permissionRepository.GetPermission("DeleteTask")), (await _permissionRepository.GetPermission("VisibilityTask")), (await _permissionRepository.GetPermission("DeleteProject")), (await _permissionRepository.GetPermission("AddNewMembers")), (await _permissionRepository.GetPermission("DeleteMembers")), (await _permissionRepository.GetPermission("ChangeRole")) };
                            Role Admin = new Role() { Name = "Admin" };
                            Role Developer = new Role() { Name = "Developer" };
                            Role Manager = new Role() { Name = "Manager" };
                            await _roleRepository.AddRole(Admin);
                            await _roleRepository.AddRole(Developer);
                            await _roleRepository.AddRole(Manager);
                            foreach (Permission permission in permissions)
                            {
                                RolePermission rolePermission = new RolePermission() { RoleId = (await _roleRepository.GetRole("Admin")).Id, PermissionId = permission.Id };
                                await _rolePermissionRepository.AddRolePermission(rolePermission);
                            }
                            RolePermission rolePermission1 = new RolePermission() { RoleId = (await _roleRepository.GetRole("Manager")).Id, PermissionId = (await _permissionRepository.GetPermission("AddNewTask")).Id };
                            RolePermission rolePermission2 = new RolePermission() { RoleId = (await _roleRepository.GetRole("Manager")).Id, PermissionId = (await _permissionRepository.GetPermission("ChangeTask")).Id };
                            RolePermission rolePermission3 = new RolePermission() { RoleId = (await _roleRepository.GetRole("Manager")).Id, PermissionId = (await _permissionRepository.GetPermission("DeleteTask")).Id };
                            RolePermission rolePermission4 = new RolePermission() { RoleId = (await _roleRepository.GetRole("Manager")).Id, PermissionId = (await _permissionRepository.GetPermission("VisibilityTask")).Id };
                            await _rolePermissionRepository.AddRolePermission(rolePermission1);
                            await _rolePermissionRepository.AddRolePermission(rolePermission2);
                            await _rolePermissionRepository.AddRolePermission(rolePermission3);
                            await _rolePermissionRepository.AddRolePermission(rolePermission4);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Debug(ex.ToString());
                        ShowError(Application.Current.Resources["m_error_download"].ToString(), Constants.Error);
                    }
                });
            }
        }

        private void ShowError(string textError, string colorError)
        {
            TextError = textError;
            ColorError = (Brush)new BrushConverter().ConvertFrom(colorError);
            VisibilityError = Visibility.Visible;
        }

        public ICommand Enter
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        var passwordBox = obj as PasswordBox;
                        var password = passwordBox.Password;
                        if (!string.Equals(password, "") && !string.Equals(Login, ""))
                        {

                            User user = new User()
                            {
                                Login = Login.ToString(),
                                Password = password
                            };
                            await TokenService.GetToken(user);

                            System.Windows.Application.Current.Properties["UserName"] = Login;
                            var displayRootRegistry = (Application.Current as App).displayRootRegistry;
                            await displayRootRegistry.ShowModalPresentation(new MainWindowViewModel());
                            CloseAction();
                            logger.Info("The user " + user.Login + " is logged in to the app");
                        }
                        else
                        {
                            ShowError(Application.Current.Resources["m_error_enter_all_fields"].ToString(), Constants.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Debug(ex.ToString());
                        ShowError(Application.Current.Resources["m_error_enter"].ToString() + "\n" + ex.Message, Constants.Error);
                    }
                });
            }
        }

        public ICommand Registration
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        var passwordBox = obj as PasswordBox;
                        var password = passwordBox.Password;
                        if (!string.Equals(password, "") && !string.Equals(Login, ""))
                        {
                            if ((await _userRepository.GetUsers()).Where(c => string.Equals(c.Login, Login)).ToList().Count == 0)
                            {
                                User user = new User() { Login = Login, Password = password, RegistrationDate = DateTime.Now };
                                await _userRepository.AddUser(user);
                                ShowError(Application.Current.Resources["m_success_registered"].ToString(), Constants.Success);
                            }
                            else
                            {
                                ShowError(Application.Current.Resources["m_error_bad_login"].ToString(), Constants.Error);
                            }
                        }
                        else
                        {
                            ShowError(Application.Current.Resources["m_error_enter_all_fields"].ToString(), Constants.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Debug(ex.ToString());
                        ShowError(Application.Current.Resources["m_error_add_user"].ToString(), Constants.Error);
                    }
                });
            }
        }
        #endregion
    }
}
