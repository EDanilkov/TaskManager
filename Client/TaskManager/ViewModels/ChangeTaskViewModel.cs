using BusinessLogicModule.Interfaces;
using NLog;
using SharedServicesModule.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace UIModule.ViewModels
{
    public class ChangeTaskViewModel : NavigateViewModel
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        IUserRepository _userRepository;
        ITaskRepository _taskRepository;

        public ChangeTaskViewModel(IUserRepository userRepository, ITaskRepository taskRepository)
        {
            _userRepository = userRepository;
            _taskRepository = taskRepository;
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

        public ICommand Loaded
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        StartDay = DateTime.Now;
                        TaskFinishDate = DateTime.Now;
                        int projectId = int.Parse(Application.Current.Properties["ProjectId"].ToString());
                        Users = await _userRepository.GetUsersFromProject(projectId);

                    }
                    catch (Exception ex)
                    {
                        logger.Debug(ex.ToString());
                        MessageBox.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message);
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
                        int projectId = int.Parse(Application.Current.Properties["ProjectId"].ToString());
                        int taskId = (await _taskRepository.GetTask(int.Parse(Application.Current.Properties["TaskId"].ToString()))).Id;
                        
                        Task task = (await _taskRepository.GetTasks()).Find(c => c.Id == taskId);
                        await _taskRepository.ChangeTask(task, TaskName, TaskDescription, SelectedUser.Id, TaskFinishDate);

                        Navigate("Pages/Task.xaml");
                        MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
                    }
                    catch (Exception ex)
                    {
                        logger.Debug(ex.ToString());
                        MessageBox.Show(Application.Current.Resources["m_error_change_task"].ToString() + "\n" + ex.Message);
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
    }
}
