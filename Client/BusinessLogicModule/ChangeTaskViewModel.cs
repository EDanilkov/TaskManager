using BusinessLogicModule.ViewModel;
using SharedServicesModule;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace BusinessLogicModule
{
    public class ChangeTaskViewModel : NavigateViewModel
    {
        IRepository _dbRepository = new DBRepository();

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
                        int projectId = int.Parse(Application.Current.Properties["ProjectId"].ToString());
                        Users = await _dbRepository.GetUsersFromProject(projectId);
                    }
                    catch (Exception ex)
                    {
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
                        int taskId = (await _dbRepository.GetTask(int.Parse(Application.Current.Properties["TaskId"].ToString()))).Id;
                        
                        SharedServicesModule.Task task = (await _dbRepository.GetTasks()).Find(c => c.Id == taskId);
                        await _dbRepository.ChangeTask(task, TaskName, TaskDescription, SelectedUser.Id, TaskFinishDate);
                        Navigate("Pages/Task.xaml");
                    }
                    catch (Exception ex)
                    {
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
