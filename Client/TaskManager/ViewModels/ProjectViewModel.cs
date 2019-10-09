using BusinessLogicModule.Interfaces;
using BusinessLogicModule.Repositories;
using BusinessLogicModule.Services;
using MaterialDesignThemes.Wpf;
using NLog;
using SharedServicesModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace UIModule.ViewModels
{
    public class ProjectViewModel : NavigateViewModel
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        IUserRepository _userRepository;
        ITaskRepository _taskRepository;
        IProjectRepository _projectRepository;
        IPermissionRepository _permissionRepository;
        IRoleRepository _roleRepository;
        IUserProjectRepository _userProjectRepository;

        public ProjectViewModel()
        {
            Title = "Project";

            _userRepository = new UserRepository();
            _taskRepository = new TaskRepository();
            _projectRepository = new ProjectRepository();
            _permissionRepository = new PermissionRepository();
            _roleRepository = new RoleRepository();
            _userProjectRepository = new UserProjectRepository();
        }

        #region Properties
        

        public ICommand AddNewTaskClick => new DelegateCommand(AddNewTask);

        private async void AddNewTask(object o)
        {
            var view = new Pages.AddNewTask
            {
                DataContext = new AddNewTaskViewModel()
            };

            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);
            int projectId = int.Parse(System.Windows.Application.Current.Properties["ProjectId"].ToString());
            ListTasks = (await _taskRepository.GetTasksFromProject(projectId));
        }

        private string _titleName;
        public string TitleName
        {
            get { return _titleName; }
            set
            {
                _titleName = value;
                OnPropertyChanged();
            }
        }

        private List<Task> _listTasks = new List<Task>();
        public List<Task> ListTasks
        {
            get { return _listTasks; }
            set
            {
                _listTasks = value;
                OnPropertyChanged();
            }
        }

        private List<Task> _listUserTask = new List<Task>();
        public List<Task> ListUserTask
        {
            get { return _listUserTask; }
            set
            {
                _listUserTask = value;
                OnPropertyChanged();
            }
        }
        

        private List<User> _listMembers = new List<User>();
        public List<User> ListMembers
        {
            get { return _listMembers; }
            set
            {
                _listMembers = value;
                OnPropertyChanged();
            }
        }

        private List<User> _newMembersSourse = new List<User>();
        public List<User> NewMembersSourse
        {
            get { return _newMembersSourse; }
            set
            {
                _newMembersSourse = value;
                OnPropertyChanged();
            }
        }

        private List<Role> _roleSourse = new List<Role>();
        public List<Role> RoleSourse
        {
            get { return _roleSourse; }
            set
            {
                _roleSourse = value;
                OnPropertyChanged();
            }
        }
        

        private User _selectedMember;
        public User SelectedMember
        {
            get { return _selectedMember; }
            set
            {
                _selectedMember = value;
                OnPropertyChanged();
            }
        }


        private Role _selectedChangeRole;
        public Role SelectedChangeRole
        {
            get { return _selectedChangeRole; }
            set
            {
                _selectedChangeRole = value;
                OnPropertyChanged();
            }
        }
        

        private Task _selectedTask;
        public Task SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                OnPropertyChanged();
            }
        }

        private string _comboBoxChangeRoleText;
        public string ComboBoxChangeRoleText
        {
            get { return _comboBoxChangeRoleText; }
            set
            {
                _comboBoxChangeRoleText = value;
                OnPropertyChanged();
            }
        }

        private string _projectDescription;
        public string ProjectDescription
        {
            get { return _projectDescription; }
            set
            {
                _projectDescription = value;
                OnPropertyChanged();
            }
        }

        private Visibility _deleteMemberButtonVisibility = Visibility.Collapsed;
        public Visibility DeleteMemberButtonVisibility
        {
            get { return _deleteMemberButtonVisibility; }
            set
            {
                _deleteMemberButtonVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _deleteTaskButtonVisibility = Visibility.Collapsed;
        public Visibility DeleteTaskButtonVisibility
        {
            get { return _deleteTaskButtonVisibility; }
            set
            {
                _deleteTaskButtonVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _addNewTaskButtonVisibility = Visibility.Collapsed;
        public Visibility AddNewTaskButtonVisibility
        {
            get { return _addNewTaskButtonVisibility; }
            set
            {
                _addNewTaskButtonVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _changeTaskButtonVisibility = Visibility.Collapsed;
        public Visibility ChangeTaskButtonVisibility
        {
            get { return _changeTaskButtonVisibility; }
            set
            {
                _changeTaskButtonVisibility = value;
                OnPropertyChanged();
            }
        }
        
        private Visibility _memberInfo = Visibility.Collapsed;
        public Visibility MemberInfo
        {
            get { return _memberInfo; }
            set
            {
                _memberInfo = value;
                OnPropertyChanged();
            }
        }

        private Visibility _taskInfo = Visibility.Collapsed;
        public Visibility TaskInfo
        {
            get { return _taskInfo; }
            set
            {
                _taskInfo = value;
                OnPropertyChanged();
            }
        }

        private Visibility _deleteProjectVisibility = Visibility.Collapsed;
        public Visibility DeleteProjectVisibility
        {
            get { return _deleteProjectVisibility; }
            set
            {
                _deleteProjectVisibility = value;
                OnPropertyChanged();
            }
        }
        

        private Visibility _visibilityMembers = Visibility.Collapsed;
        public Visibility VisibilityMembers
        {
            get { return _visibilityMembers; }
            set
            {
                _visibilityMembers = value;
                OnPropertyChanged();
            }
        }

        private Visibility _addNewMembersButtonVisibility = Visibility.Collapsed;
        public Visibility AddNewMembersButtonVisibility
        {
            get { return _addNewMembersButtonVisibility; }
            set
            {
                _addNewMembersButtonVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _changeRoleVisibility = Visibility.Collapsed;
        public Visibility ChangeRoleVisibility
        {
            get { return _changeRoleVisibility; }
            set
            {
                _changeRoleVisibility = value;
                OnPropertyChanged();
            }
        }
        
        private string _listTasksText;
        public string ListTasksText
        {
            get { return _listTasksText; }
            set
            {
                _listTasksText = value;
                OnPropertyChanged();
            }
        }

        private string _taskEndDate;
        public string TaskEndDate
        {
            get { return _taskEndDate; }
            set
            {
                _taskEndDate = value;
                OnPropertyChanged();
            }
        }

        private string _taskBeginDate;
        public string TaskBeginDate
        {
            get { return _taskBeginDate; }
            set
            {
                _taskBeginDate = value;
                OnPropertyChanged();
            }
        }
        
        private string _userNameSelectedTask;
        public string UserNameSelectedTask
        {
            get { return _userNameSelectedTask; }
            set
            {
                _userNameSelectedTask = value;
                OnPropertyChanged();
            }
        }
        
        private TextDecorationCollection _underline;
        public TextDecorationCollection Underline
        {
            get { return _underline; }
            set
            {
                _underline = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Methods
        
        public async System.Threading.Tasks.Task RefreshUsers()
        {
            int projectId = int.Parse(System.Windows.Application.Current.Properties["ProjectId"].ToString());
            List<User> userInOtherProject = new List<User>();
            List<User> users = (await _userRepository.GetUsers());
            List<User> usersInProject = (await _userRepository.GetUsersFromProject(projectId));
            int number = 0;
            int countUsersInProject = usersInProject.Count;
            foreach (User user in users)
            {
                number = 0;
                foreach (User userProject in usersInProject)
                {
                    if (!string.Equals(user.Login, userProject.Login))
                    {
                        number++;
                    }
                }
                if (number == countUsersInProject)
                {
                    userInOtherProject.Add(user);
                }
            }
            NewMembersSourse = userInOtherProject;
            ListMembers = await _userRepository.GetUsersFromProject(projectId);
        }

        public ICommand DeleteTaskClick
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        int projectId = int.Parse(System.Windows.Application.Current.Properties["ProjectId"].ToString());

                        await _taskRepository.DeleteTask(SelectedTask.Id);

                        ListTasks = (await _taskRepository.GetTasksFromProject(projectId));
                        TaskInfo = Visibility.Collapsed;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Application.Current.Resources["m_error_delete_task"].ToString() + "\n" + ex.Message);
                    }
                });
            }
        }

        public ICommand MemberSelectionChanged
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        if(SelectedMember != null)
                        {
                            string userName = System.Windows.Application.Current.Properties["UserName"].ToString();
                            int projectId = int.Parse(System.Windows.Application.Current.Properties["ProjectId"].ToString());
                            Project project = await _projectRepository.GetProject(projectId);
                            User user = await _userRepository.GetUser(SelectedMember.Login);
                            int adminId = (await _userRepository.GetUser(SelectedMember.Login)).Id;
                            
                            List<Permission> permissions = await _permissionRepository.GetPermissionsFromRole((await _roleRepository.GetRoleFromUser(userName, projectId)).Id);
                            DeleteMemberButtonVisibility = (project.AdminId == adminId ? Visibility.Collapsed :
                                                            permissions.Where(c => string.Equals(c.Name, "DeleteMembers")).ToList().Count == 0 ? Visibility.Collapsed : Visibility.Visible);
                            ChangeRoleVisibility = (project.AdminId == adminId ? Visibility.Collapsed :
                                                            permissions.Where(c => string.Equals(c.Name, "ChangeRole")).ToList().Count == 0 ? Visibility.Collapsed : Visibility.Visible);

                            ListUserTask = await _taskRepository.GetProjectTasksByUser(user.Id, projectId);
                            ListTasksText = ListUserTask.Count == 0 ? Application.Current.Resources["m_member_dont_have_tasks"].ToString() : Application.Current.Resources["mTasks"].ToString();
                            MemberInfo = Visibility.Visible;
                            SelectedChangeRole = await _roleRepository.GetRoleFromUser(SelectedMember.Login, projectId);
                        }

                    }
                    catch (Exception ex)
                    {
                        logger.Debug(ex.ToString());
                        MessageBox.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message);
                    }
                });
            }
        }

        public async System.Threading.Tasks.Task SelectVisibility(Role role)
        {
            try
            {
                List<Permission> permissions = await _permissionRepository.GetPermissionsFromRole(role.Id);
                foreach (Permission permission in permissions)
                {
                    switch (permission.Name)
                    {
                        case "AddNewTask":
                        {
                            AddNewTaskButtonVisibility = Visibility.Visible;
                            break;
                        }
                        case "DeleteTask":
                        {
                            DeleteTaskButtonVisibility = Visibility.Visible;
                            break;
                        }
                        case "AddNewMembers":
                        {
                            AddNewMembersButtonVisibility = Visibility.Visible;
                            break;
                        }
                        case "DeleteMembers":
                        {
                            DeleteMemberButtonVisibility = Visibility.Visible;
                            break;
                        }
                        case "ChangeRole":
                        {
                            ChangeRoleVisibility = Visibility.Visible;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Debug(ex.ToString());
                MessageBox.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message);
            }
        }

        public ICommand Loaded
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        int projectId = int.Parse(System.Windows.Application.Current.Properties["ProjectId"].ToString());
                        string userName = System.Windows.Application.Current.Properties["UserName"].ToString();

                        TaskInfo = Visibility.Collapsed;
                        Role role = await _roleRepository.GetRoleFromUser(userName, projectId);
                        await SelectVisibility(role);
                        Project project = await _projectRepository.GetProject(projectId);
                        ProjectDescription = project.Description;
                        TitleName += "/" + project.Name;
                        ListTasks = (await _taskRepository.GetTasksFromProject(projectId));
                        RoleSourse = await _roleRepository.GetRoles();
                        await RefreshUsers();
                    
                        if (string.Equals((await _roleRepository.GetRoleFromUser(userName, projectId)).Name, "Admin"))
                        {
                            VisibilityMembers = Visibility.Visible;
                        }
                        if ((await _projectRepository.GetProject(projectId)).AdminId == (await _userRepository.GetUser(userName)).Id)
                        {
                            DeleteProjectVisibility = Visibility.Visible;
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Debug(ex.ToString());
                        MessageBox.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message);
                    }
                });
            }
        }

        public ICommand DeleteProjectClick
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        if (MessageBox.Show(Application.Current.Resources["m_delete_project"].ToString(), "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            await _projectRepository.DeleteProject(int.Parse(System.Windows.Application.Current.Properties["ProjectId"].ToString()));
                            Navigate("Pages/Projects.xaml");
                        }

                    }
                    catch (Exception ex)
                    {
                        logger.Debug(ex.ToString());
                        MessageBox.Show(Application.Current.Resources["m_error_delete_project"].ToString() + "\n" + ex.Message);
                    }
                });
            }
        }
        
        public ICommand DeleteMemberClick
        {
            get
            {
                return new DelegateCommand(async(obj) =>
                {
                    try
                    {
                        string userName = System.Windows.Application.Current.Properties["UserName"].ToString();
                        int projectId = int.Parse(System.Windows.Application.Current.Properties["ProjectId"].ToString());

                        if ((await _taskRepository.GetProjectTasksByUser(SelectedMember.Id, projectId)).Count != 0)
                        {
                            if (MessageBox.Show(Application.Current.Resources["m_delete_all_task"].ToString(), "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            {
                                await _taskRepository.DeleteTasksByUser(SelectedMember.Id, projectId);
                            }
                        }
                 

                        await _userRepository.DeleteUserFromProject(SelectedMember.Id, projectId);
                    
                        List<User> userInOtherProject = new List<User>();
                        List<User> users = (await _userRepository.GetUsers());
                        List<User> usersInProject = (await _userRepository.GetUsersFromProject(projectId));
                        int number = 0;
                        int countUsersInProject = usersInProject.Count;
                        foreach (User user in users)
                        {
                            number = 0;
                            foreach (User userProject in usersInProject)
                            {
                                if (!string.Equals(user.Login, userProject.Login))
                                {
                                    number++;
                                }
                            }
                            if (number == countUsersInProject) userInOtherProject.Add(user);
                        }
                        NewMembersSourse = userInOtherProject;
                        ListMembers = await _userRepository.GetUsersFromProject(projectId);
                        MemberInfo = Visibility.Collapsed;
                    }
                    catch (Exception ex)
                    {
                        logger.Debug(ex.ToString());
                        MessageBox.Show(Application.Current.Resources["m_error_delete_member"].ToString() + "\n" + ex.Message);
                    }
                });
            }
        }

        public ICommand TaskInfoSelectionChanged
        {
            get
            {
                return new DelegateCommand(async(obj) =>
                {
                    try
                    {
                        if(SelectedTask != null)
                        {
                            TaskEndDate = SelectedTask.EndDate.ToShortDateString();
                            TaskBeginDate = SelectedTask.BeginDate.ToShortDateString();
                            UserNameSelectedTask = (await _userRepository.GetUser(id: SelectedTask.UserId)).Login;
                            TaskInfo = Visibility.Visible;
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Debug(ex.ToString());
                        MessageBox.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message);
                    }
                });
            }
        }

        public ICommand ChangeMemberRoleClick
        {
            get
            {
                return new DelegateCommand(async(obj) =>
                {
                    try
                    {
                        string userName = System.Windows.Application.Current.Properties["UserName"].ToString();
                        int projectId = int.Parse(System.Windows.Application.Current.Properties["ProjectId"].ToString());

                        if (SelectedChangeRole == null)
                        {
                            ComboBoxChangeRoleText = Application.Current.Resources["m_select_role"].ToString();
                        }

                        Project project = await _projectRepository.GetProject(projectId);
                        await _userRepository.DeleteUserFromProject(SelectedMember.Id, projectId);
                        UserProject userProject = new UserProject() { UserId = SelectedMember.Id, ProjectId = project.Id, RoleId = SelectedChangeRole.Id };
                        await _userProjectRepository.AddUserProject(userProject);
                    }
                    catch (Exception ex)
                    {
                        logger.Debug(ex.ToString());
                        MessageBox.Show(Application.Current.Resources["m_error_change_role"].ToString() + "\n" + ex.Message);
                    }
                });
            }
        }        

        public ICommand TaskOpenButtonClick
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    try
                    {
                        System.Windows.Application.Current.Properties["TaskId"] = SelectedTask.Id;
                        Navigate("Pages/Task.xaml");
                    }
                    catch (Exception ex)
                    {
                        logger.Debug(ex.ToString());
                        MessageBox.Show(Application.Current.Resources["m_error_download"].ToString());
                    }
                });
            }
        }

        public ICommand UnderlineOn
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    try
                    {
                        Underline = TextDecorations.Underline;
                    }
                    catch (Exception ex)
                    {

                        logger.Debug(ex.ToString());
                    }
                });
            }
        }
        
        public ICommand UnderlineOff
        {
            get
            {
                return new DelegateCommand((ex) =>
                {
                    try
                    {
                        Underline = null;
                    }
                    catch (Exception e)
                    {
                        logger.Debug(e.ToString());
                    }
                });
            }
        }

        public ICommand OpenProjects
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    try
                    {
                        Navigate("Pages/Projects.xaml");
                    }
                    catch (Exception e)
                    {
                        logger.Debug(e.ToString());
                    }
                });
            }
        }

        public ICommand Back
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Navigate("Pages/Projects.xaml");
                });
            }
        }

        public ICommand AddNewMemberClick => new DelegateCommand(AddNewMember);

        private async void AddNewMember(object o)
        {
            var view = new Pages.AddNewMember
            {
                DataContext = new AddNewMemberViewModel()
            };

            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);
            await RefreshUsers();
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("You can intercept the closing event, and cancel here.");
        }

        #endregion
    }
}
