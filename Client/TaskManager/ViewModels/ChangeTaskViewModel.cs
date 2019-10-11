using BusinessLogicModule.Interfaces;
using NLog;
using SharedServicesModule.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using UIModule.Utils;

namespace UIModule.ViewModels
{
    public class ChangeTaskViewModel : NavigateViewModel
    {
        string _dialogIdentifier = "ChangeTaskDialog";
        private static Logger logger = LogManager.GetCurrentClassLogger();
        IUserRepository _userRepository;
        ITaskRepository _taskRepository;
        IProjectRepository _projectRepository;

        #region Properties

        public ChangeTaskViewModel(IUserRepository userRepository, ITaskRepository taskRepository, IProjectRepository projectRepository)
        {
            _userRepository = userRepository;
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
        }

        private string _taskName;
        public string TaskName
        {
            get { return _taskName; }
            set
            {
                _taskName = value;
                OnPropertyChanged();
            }
        }

        private string _taskDescription;
        public string TaskDescription
        {
            get { return _taskDescription; }
            set
            {
                _taskDescription = value;
                OnPropertyChanged();
            }
        }

        private DateTime _taskFinishDate = DateTime.Now;
        public DateTime TaskFinishDate
        {
            get { return _taskFinishDate; }
            set
            {
                _taskFinishDate = value;
                OnPropertyChanged();
            }
        }


        private DateTime _startDay;
        public DateTime StartDay
        {
            get { return _startDay; }
            set
            {
                _startDay = value;
                OnPropertyChanged();
            }
        }

        private List<User> _users;
        public List<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        private User _selectedUser;
        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
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
                        int projectId = int.Parse(Application.Current.Properties["ProjectId"].ToString());
                        Users = await _userRepository.GetUsersFromProject(projectId);
                        Task task = await _taskRepository.GetTask(int.Parse(Application.Current.Properties["TaskId"].ToString()));
                        TaskName = task.Name;
                        TaskDescription = task.Description;
                        TaskFinishDate = task.EndDate;
                        SelectedUser = Users.Find(c => c.Id == task.UserId);
                        StartDay = DateTime.Now;

                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString());
                        ErrorHandler.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message, _dialogIdentifier);
                    }
                });
            }
        }

        public ICommand Change
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        if (TaskName != null && TaskDescription != null && SelectedUser != null && TaskFinishDate != null && TaskFinishDate >= DateTime.Today)
                        {
                            int projectId = int.Parse(Application.Current.Properties["ProjectId"].ToString());
                            int taskId = (await _taskRepository.GetTask(int.Parse(Application.Current.Properties["TaskId"].ToString()))).Id;
                        
                            Task task = (await _taskRepository.GetTasks()).Find(c => c.Id == taskId);
                            await _taskRepository.ChangeTask(task, TaskName, TaskDescription, SelectedUser.Id, TaskFinishDate);
                        
                            logger.Debug("user " + Application.Current.Properties["UserName"].ToString() + " changed task " + TaskName + " to the project " + (await _projectRepository.GetProject(projectId)).Name);
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
                        ErrorHandler.Show(Application.Current.Resources["m_error_change_task"].ToString() + "\n" + ex.Message, _dialogIdentifier);
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
                    Navigate("Pages/Task.xaml");
                });
            }
        }

        #endregion
    }
}
