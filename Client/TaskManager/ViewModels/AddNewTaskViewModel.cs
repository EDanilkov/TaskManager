using BusinessLogicModule.Interfaces;
using BusinessLogicModule.Interfaces;
using NLog;
using SharedServicesModule.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace UIModule.ViewModels
{
    public class AddNewTaskViewModel : NavigateViewModel
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        IUserRepository _userRepository;
        ITaskRepository _taskRepository;

        public AddNewTaskViewModel()
        {
            _userRepository = new UserRepository();
            _taskRepository = new TaskRepository();
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

        private DateTime _taskFinishDate;
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
                        string projectId = System.Windows.Application.Current.Properties["ProjectId"].ToString();
                        List<User> users = await _userRepository.GetUsersFromProject(int.Parse(projectId));
                        Users = users;
                    }
                    catch (Exception ex)
                    {
                        logger.Debug(ex.ToString());
                        MessageBox.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message);
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
                        if(TaskName != null && TaskDescription != null && SelectedUser != null && TaskFinishDate != null)
                        {
                            Task task = new Task()
                            {
                                Name = TaskName,
                                Description = TaskDescription,
                                BeginDate = DateTime.Now,
                                ProjectId = int.Parse(System.Windows.Application.Current.Properties["ProjectId"].ToString()),
                                UserId = SelectedUser.Id,
                                EndDate = TaskFinishDate
                            };

                            await _taskRepository.AddTask(task);
                            Navigate("Pages/Project.xaml");
                            MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
                        }
                        else
                        {
                            MessageBox.Show(Application.Current.Resources["m_correct_entry"].ToString());
                        }

                    }
                    catch (Exception ex)
                    {
                        logger.Debug(ex.ToString());
                        MessageBox.Show(Application.Current.Resources["m_error_create_task"].ToString() + "\n" + ex.Message);
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
                    Navigate("Pages/Project.xaml");
                });
            }
        }
    }
}
