using BusinessLogicModule.Interfaces;
using NLog;
using SharedServicesModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using UIModule.Utils;

namespace UIModule.ViewModels
{
    class AddNewMemberViewModel : NavigateViewModel
    {
        string _dialogIdentifier = "AddMemberDialog";
        private static Logger logger = LogManager.GetCurrentClassLogger();
        IUserRepository _userRepository;
        IProjectRepository _projectRepository;
        IRoleRepository _roleRepository;
        IUserProjectRepository _userProjectRepository;

        public AddNewMemberViewModel(IUserRepository userRepository, IProjectRepository projectRepository, IRoleRepository roleRepository, IUserProjectRepository userProjectRepository)
        {
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _roleRepository = roleRepository;
            _userProjectRepository = userProjectRepository;
        }

        #region Properties

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

        #endregion

        #region Methods

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
                        List<User> users = (await _userRepository.GetUsers());
                        List<User> usersInProject = (await _userRepository.GetUsersFromProject(projectId));
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
                        logger.Error(ex.ToString());
                        ErrorHandler.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message, _dialogIdentifier);
                    }
                });
            }
        }

        public ICommand Accept
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        if (SelectedNewMember != null && SelectedRole != null)
                        {
                            int projectId = int.Parse(System.Windows.Application.Current.Properties["ProjectId"].ToString());
                            Project project = await _projectRepository.GetProject(projectId);
                            UserProject userProject = new UserProject() { UserId = SelectedNewMember.Id, ProjectId = project.Id, RoleId = SelectedRole.Id };
                            await _userProjectRepository.AddUserProject(userProject);

                            logger.Debug("user " + Application.Current.Properties["UserName"].ToString() + " added user " + SelectedNewMember.Login + " to the project " + project.Name + " with the " + SelectedRole.Name + " role");
                            MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
                        }
                        else
                        {
                            ErrorHandler.Show(Application.Current.Resources["m_correct_entry"].ToString(), _dialogIdentifier);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString());
                        ErrorHandler.Show(Application.Current.Resources["m_error_add_user"].ToString() + "\n" + ex.Message, _dialogIdentifier);
                    }
                });
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
                        RoleSourse = await _roleRepository.GetRoles();
                        await RefreshUsers();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString());
                        ErrorHandler.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message, _dialogIdentifier);
                    }
                });
            }
        }

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
            ListNewDevelopers = userInOtherProject;
            NewMembersSourse = userInOtherProject;
            ListMembers = await _userRepository.GetUsersFromProject(projectId);
        }

        #endregion
    }
}
