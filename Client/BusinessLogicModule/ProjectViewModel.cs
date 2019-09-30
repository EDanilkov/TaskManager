using BusinessLogicModule.ViewModel;
using GalaSoft.MvvmLight.CommandWpf;
using SharedServicesModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace BusinessLogicModule
{
    public class ProjectViewModel : NavigateViewModel
    {
        public ProjectViewModel()
        {
            Title = "Project";
        }

        IRepository _dbRepository = new DBRepository();

        #region Properties

        private ICommand _addNewTaskClick;
        public ICommand AddNewTaskClick
        {
            get
            {
                if (_addNewTaskClick == null)
                {
                    _addNewTaskClick = new RelayCommand(() =>
                    {
                        Navigate("Pages/AddNewTask.xaml");
                    });
                }
                return _addNewTaskClick;
            }
            set { _addNewTaskClick = value; }
        }

        private string _header;
        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                OnPropertyChanged();
            }
        }

        private List<SharedServicesModule.Task> _listTasks = new List<SharedServicesModule.Task>();
        public List<SharedServicesModule.Task> ListTasks
        {
            get { return _listTasks; }
            set
            {
                _listTasks = value;
                OnPropertyChanged();
            }
        }

        private List<SharedServicesModule.Task> _listUserTask = new List<SharedServicesModule.Task>();
        public List<SharedServicesModule.Task> ListUserTask
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

        private List<User> _listNewDevelopers = new List<User>();
        public List<User> ListNewDevelopers
        {
            get { return _listNewDevelopers; }
            set
            {
                _listNewDevelopers = value;
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

        private User _selectedNewMember;
        public User SelectedNewMember
        {
            get { return _selectedNewMember; }
            set
            {
                _selectedNewMember = value;
                OnPropertyChanged();
            }
        }

        private Role _selectedRole;
        public Role SelectedRole
        {
            get { return _selectedRole; }
            set
            {
                _selectedRole = value;
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
        

        private bool _comboBoxNewMemberIsDropDownOpen;
        public bool ComboBoxNewMemberIsDropDownOpen
        {
            get { return _comboBoxNewMemberIsDropDownOpen; }
            set
            {
                _comboBoxNewMemberIsDropDownOpen = value;
                OnPropertyChanged();
            }
        }
        

        private SharedServicesModule.Task _selectedTask;
        public SharedServicesModule.Task SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                OnPropertyChanged();
            }
        }

        private string _comboBoxNewMembersText;
        public string ComboBoxNewMembersText
        {
            get { return _comboBoxNewMembersText; }
            set
            {
                _comboBoxNewMembersText = value;
                OnPropertyChanged();
            }
        }

        private string _comboBoxRoleText;
        public string ComboBoxRoleText
        {
            get { return _comboBoxRoleText; }
            set
            {
                _comboBoxRoleText = value;
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
            List<User> users = (await _dbRepository.GetUsers());
            List<User> usersInProject = (await _dbRepository.GetUsersFromProject(projectId));
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
            ListNewDevelopers = userInOtherProject;
            NewMembersSourse = userInOtherProject;
            ListMembers = await _dbRepository.GetUsersFromProject(projectId);
        }

        public ICommand ComboBoxNewMembersChanged
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        int projectId = int.Parse(System.Windows.Application.Current.Properties["ProjectId"].ToString());
                        List<User> userInOtherProject = new List<User>();
                        List<User> users = (await _dbRepository.GetUsers());
                        List<User> usersInProject = (await _dbRepository.GetUsersFromProject(projectId));
                        int number = 0;
                        int countUsersInProject = usersInProject.Count;
                        foreach (User user in users)
                        {
                            number = 0;
                            foreach (User userProject1 in usersInProject)
                            {
                                if (!string.Equals(user.Login, userProject1.Login))
                                {
                                    number++;
                                }
                            }
                            if (number == countUsersInProject)
                            {
                                userInOtherProject.Add(user);
                            }
                        }
                        ComboBoxNewMemberIsDropDownOpen = true;
                        NewMembersSourse = userInOtherProject.Where(p => p.Login.Contains(ComboBoxNewMembersText)).ToList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message);
                    }
                });
            }
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

                        await _dbRepository.DeleteTask(SelectedTask.Id);

                        ListTasks = (await _dbRepository.GetTasksFromProject(projectId));
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
                            Project project = await _dbRepository.GetProject(projectId);
                            User user = await _dbRepository.GetUser(SelectedMember.Login);
                            int adminId = (await _dbRepository.GetUser(SelectedMember.Login)).Id;
                            
                            List<Permission> permissions = await _dbRepository.GetPermissionsFromRole((await _dbRepository.GetRoleFromUser(userName, projectId)).Id);
                            DeleteMemberButtonVisibility = (project.AdminId == adminId ? Visibility.Collapsed :
                                                            permissions.Where(c => string.Equals(c.Name, "DeleteMembers")).ToList().Count == 0 ? Visibility.Collapsed : Visibility.Visible);
                            ChangeRoleVisibility = (project.AdminId == adminId ? Visibility.Collapsed :
                                                            permissions.Where(c => string.Equals(c.Name, "ChangeRole")).ToList().Count == 0 ? Visibility.Collapsed : Visibility.Visible);

                            ListUserTask = await _dbRepository.GetProjectTasksFromUser(user.Id, projectId);
                            ListTasksText = ListUserTask.Count == 0 ? Application.Current.Resources["m_member_dont_have_tasks"].ToString() : Application.Current.Resources["m_tasks"].ToString();
                            MemberInfo = Visibility.Visible;
                            SelectedChangeRole = await _dbRepository.GetRoleFromUser(SelectedMember.Login, projectId);
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message);
                    }
                });
            }
        }

        public async System.Threading.Tasks.Task SelectVisibility(Role role)
        {
            try
            {
                List<Permission> permissions = await _dbRepository.GetPermissionsFromRole(role.Id);
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

                        Role role = await _dbRepository.GetRoleFromUser(userName, projectId);
                        await SelectVisibility(role);
                        Project project = await _dbRepository.GetProject(projectId);
                        ProjectDescription = project.Description;
                        Header += project.Name;
                        ListTasks = (await _dbRepository.GetTasksFromProject(projectId));
                        RoleSourse = await _dbRepository.GetRoles();
                        await RefreshUsers();
                    
                        if (string.Equals((await _dbRepository.GetRoleFromUser(userName, projectId)).Name, "Admin"))
                        {
                            VisibilityMembers = Visibility.Visible;
                        }
                        if ((await _dbRepository.GetProject(projectId)).AdminId == (await _dbRepository.GetUser(userName)).Id)
                        {
                            DeleteProjectVisibility = Visibility.Visible;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message);
                    }
                });
            }
        }

        public ICommand AddNewMemberClick
        {
            get
            {
                return new DelegateCommand(async(obj) =>
                {
                    try
                    {
                        if(SelectedNewMember != null && SelectedRole != null)
                        {
                            int projectId = int.Parse(System.Windows.Application.Current.Properties["ProjectId"].ToString());
                            Project project = await _dbRepository.GetProject(projectId);
                            UserProject userProject = new UserProject() { UserId = SelectedNewMember.Id, ProjectId = project.Id, RoleId = SelectedRole.Id };
                            await _dbRepository.AddUserProject(userProject);

                            ComboBoxRoleText = "";

                            await RefreshUsers();
                        }
                        else
                        {
                            MessageBox.Show(Application.Current.Resources["m_correct_entry"].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Application.Current.Resources["m_error_add_user"].ToString() + "\n" + ex.Message);
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
                            await _dbRepository.DeleteProject(int.Parse(System.Windows.Application.Current.Properties["ProjectId"].ToString()));
                            Navigate("Pages/Projects.xaml");
                        }
                        else
                        { 
                        }

                    }
                    catch (Exception ex)
                    {
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

                        if ((await _dbRepository.GetProjectTasksFromUser(SelectedMember.Id, projectId)).Count != 0)
                        {
                            if (MessageBox.Show(Application.Current.Resources["m_delete_all_task"].ToString(), "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            {
                                await _dbRepository.DeleteTasksFromUser(SelectedMember.Id, projectId);
                            }
                        }
                 

                        await _dbRepository.DeleteUserFromProject(SelectedMember.Id, projectId);
                    
                        List<User> userInOtherProject = new List<User>();
                        List<User> users = (await _dbRepository.GetUsers());
                        List<User> usersInProject = (await _dbRepository.GetUsersFromProject(projectId));
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
                        ListMembers = await _dbRepository.GetUsersFromProject(projectId);
                        MemberInfo = Visibility.Collapsed;
                    }
                    catch (Exception ex)
                    {
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
                            UserNameSelectedTask = (await _dbRepository.GetUser(id: SelectedTask.UserId)).Login;
                            TaskInfo = Visibility.Visible;
                        }
                    }
                    catch (Exception ex)
                    {
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

                        Project project = await _dbRepository.GetProject(projectId);
                        await _dbRepository.DeleteUserFromProject(SelectedMember.Id, projectId);
                        UserProject userProject = new UserProject() { UserId = SelectedMember.Id, ProjectId = project.Id, RoleId = SelectedChangeRole.Id };
                        await _dbRepository.AddUserProject(userProject);
                    }
                    catch (Exception ex)
                    {
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
                    catch (Exception e)
                    {
                        throw;
                    }
                });
            }
        }
        
        public ICommand UnderlineOff
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    try
                    {
                        Underline = null;
                    }
                    catch (Exception e)
                    {
                        throw;
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
                        throw;
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

        #endregion
    }
}
