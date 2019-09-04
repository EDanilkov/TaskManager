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
        private ICommand _AddNewTaskClick;

        public ICommand AddNewTaskClick
        {
            get
            {
                if (_AddNewTaskClick == null)
                {
                    _AddNewTaskClick = new RelayCommand(() =>
                    {
                        Navigate("Pages/AddNewTask.xaml");
                    });
                }
                return _AddNewTaskClick;
            }
            set { _AddNewTaskClick = value; }
        }



        IRepository _dbRepository = new DBRepository();


        private string _Header;
        public string Header
        {
            get { return _Header; }
            set
            {
                _Header = value;
                OnPropertyChanged();
            }
        }

        private List<SharedServicesModule.Task> _ListTasks = new List<SharedServicesModule.Task>();
        public List<SharedServicesModule.Task> ListTasks
        {
            get { return _ListTasks; }
            set
            {
                _ListTasks = value;
                OnPropertyChanged();
            }
        }

        private List<SharedServicesModule.Task> _ListUserTask = new List<SharedServicesModule.Task>();
        public List<SharedServicesModule.Task> ListUserTask
        {
            get { return _ListUserTask; }
            set
            {
                _ListUserTask = value;
                OnPropertyChanged();
            }
        }
        

        private List<User> _ListMembers = new List<User>();
        public List<User> ListMembers
        {
            get { return _ListMembers; }
            set
            {
                _ListMembers = value;
                OnPropertyChanged();
            }
        }

        private List<User> _ListNewDevelopers = new List<User>();
        public List<User> ListNewDevelopers
        {
            get { return _ListNewDevelopers; }
            set
            {
                _ListNewDevelopers = value;
                OnPropertyChanged();
            }
        }

        private List<User> _NewMembersSourse = new List<User>();
        public List<User> NewMembersSourse
        {
            get { return _NewMembersSourse; }
            set
            {
                _NewMembersSourse = value;
                OnPropertyChanged();
            }
        }

        private List<Role> _RoleSourse = new List<Role>();
        public List<Role> RoleSourse
        {
            get { return _RoleSourse; }
            set
            {
                _RoleSourse = value;
                OnPropertyChanged();
            }
        }
        

        private User _SelectedMember;
        public User SelectedMember
        {
            get { return _SelectedMember; }
            set
            {
                _SelectedMember = value;
                OnPropertyChanged();
            }
        }

        private User _SelectedNewMember;
        public User SelectedNewMember
        {
            get { return _SelectedNewMember; }
            set
            {
                _SelectedNewMember = value;
                OnPropertyChanged();
            }
        }

        private Role _SelectedRole;
        public Role SelectedRole
        {
            get { return _SelectedRole; }
            set
            {
                _SelectedRole = value;
                OnPropertyChanged();
            }
        }


        private Role _SelectedChangeRole;
        public Role SelectedChangeRole
        {
            get { return _SelectedChangeRole; }
            set
            {
                _SelectedChangeRole = value;
                OnPropertyChanged();
            }
        }
        

        private bool _ComboBoxNewMemberIsDropDownOpen;
        public bool ComboBoxNewMemberIsDropDownOpen
        {
            get { return _ComboBoxNewMemberIsDropDownOpen; }
            set
            {
                _ComboBoxNewMemberIsDropDownOpen = value;
                OnPropertyChanged();
            }
        }
        

        private SharedServicesModule.Task _SelectedTask;
        public SharedServicesModule.Task SelectedTask
        {
            get { return _SelectedTask; }
            set
            {
                _SelectedTask = value;
                OnPropertyChanged();
            }
        }

        private string _ComboBoxNewMembersText;
        public string ComboBoxNewMembersText
        {
            get { return _ComboBoxNewMembersText; }
            set
            {
                _ComboBoxNewMembersText = value;
                OnPropertyChanged();
            }
        }

        private string _ComboBoxRoleText;
        public string ComboBoxRoleText
        {
            get { return _ComboBoxRoleText; }
            set
            {
                _ComboBoxRoleText = value;
                OnPropertyChanged();
            }
        }

        private string _ComboBoxChangeRoleText;
        public string ComboBoxChangeRoleText
        {
            get { return _ComboBoxChangeRoleText; }
            set
            {
                _ComboBoxChangeRoleText = value;
                OnPropertyChanged();
            }
        }

        private string _ProjectDescription;
        public string ProjectDescription
        {
            get { return _ProjectDescription; }
            set
            {
                _ProjectDescription = value;
                OnPropertyChanged();
            }
        }

        private Visibility _DeleteMemberButtonVisibility = Visibility.Collapsed;
        public Visibility DeleteMemberButtonVisibility
        {
            get { return _DeleteMemberButtonVisibility; }
            set
            {
                _DeleteMemberButtonVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _DeleteTaskButtonVisibility = Visibility.Collapsed;
        public Visibility DeleteTaskButtonVisibility
        {
            get { return _DeleteTaskButtonVisibility; }
            set
            {
                _DeleteTaskButtonVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _AddNewTaskButtonVisibility = Visibility.Collapsed;
        public Visibility AddNewTaskButtonVisibility
        {
            get { return _AddNewTaskButtonVisibility; }
            set
            {
                _AddNewTaskButtonVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _ChangeTaskButtonVisibility = Visibility.Collapsed;
        public Visibility ChangeTaskButtonVisibility
        {
            get { return _ChangeTaskButtonVisibility; }
            set
            {
                _ChangeTaskButtonVisibility = value;
                OnPropertyChanged();
            }
        }
        



        private Visibility _MemberInfo = Visibility.Collapsed;
        public Visibility MemberInfo
        {
            get { return _MemberInfo; }
            set
            {
                _MemberInfo = value;
                OnPropertyChanged();
            }
        }

        private Visibility _TaskInfo = Visibility.Collapsed;
        public Visibility TaskInfo
        {
            get { return _TaskInfo; }
            set
            {
                _TaskInfo = value;
                OnPropertyChanged();
            }
        }

        

        private Visibility _DeleteProjectVisibility = Visibility.Collapsed;
        public Visibility DeleteProjectVisibility
        {
            get { return _DeleteProjectVisibility; }
            set
            {
                _DeleteProjectVisibility = value;
                OnPropertyChanged();
            }
        }
        

        private Visibility _VisibilityMembers = Visibility.Collapsed;
        public Visibility VisibilityMembers
        {
            get { return _VisibilityMembers; }
            set
            {
                _VisibilityMembers = value;
                OnPropertyChanged();
            }
        }

        private Visibility _AddNewMembersButtonVisibility = Visibility.Collapsed;
        public Visibility AddNewMembersButtonVisibility
        {
            get { return _AddNewMembersButtonVisibility; }
            set
            {
                _AddNewMembersButtonVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _ChangeRoleVisibility = Visibility.Collapsed;
        public Visibility ChangeRoleVisibility
        {
            get { return _ChangeRoleVisibility; }
            set
            {
                _ChangeRoleVisibility = value;
                OnPropertyChanged();
            }
        }
        

        private string _ListTasksText;
        public string ListTasksText
        {
            get { return _ListTasksText; }
            set
            {
                _ListTasksText = value;
                OnPropertyChanged();
            }
        }

        private string _TaskEndDate;
        public string TaskEndDate
        {
            get { return _TaskEndDate; }
            set
            {
                _TaskEndDate = value;
                OnPropertyChanged();
            }
        }

        private string _TaskBeginDate;
        public string TaskBeginDate
        {
            get { return _TaskBeginDate; }
            set
            {
                _TaskBeginDate = value;
                OnPropertyChanged();
            }
        }

        private string _UserNameSelectedTask;
        public string UserNameSelectedTask
        {
            get { return _UserNameSelectedTask; }
            set
            {
                _UserNameSelectedTask = value;
                OnPropertyChanged();
            }
        }
        



        public async System.Threading.Tasks.Task RefreshUsers()
        {
            int projectId = int.Parse(System.Windows.Application.Current.Properties["ProjectId"].ToString());
            List<User> userInOtherProject = new List<User>();
            List<User> users = (await _dbRepository.GetUsers());
            List<User> usersInProject = (await _dbRepository.GetUsersFromProject(projectId));
            int number = 0;
            int CountUsersInProject = usersInProject.Count;
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
                if (number == CountUsersInProject)
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
                        int CountUsersInProject = usersInProject.Count;
                        foreach (User user1 in users)
                        {
                            number = 0;
                            foreach (User userProject1 in usersInProject)
                            {
                                if (!string.Equals(user1.Login, userProject1.Login))
                                {
                                    number++;
                                }
                            }
                            if (number == CountUsersInProject)
                            {
                                userInOtherProject.Add(user1);
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


                            List<Permission> permissions = await _dbRepository.GetPermissionsFromRole((await _dbRepository.GetRoleFromUser(userName, projectId)).Id);//(await DBRepository.GetRoleFromUser(userName, projectId)).Permission.ToList();

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
                        int CountUsersInProject = usersInProject.Count;
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
                            if (number == CountUsersInProject) userInOtherProject.Add(user);
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

        private TextDecorationCollection _Underline;
        public TextDecorationCollection Underline
        {
            get { return _Underline; }
            set
            {
                _Underline = value;
                OnPropertyChanged();
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

    }
}
