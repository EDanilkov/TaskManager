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

        private string _TaskName;
        public string TaskName
        {
            get { return _TaskName; }
            set
            {
                _TaskName = value;
                OnPropertyChanged();
            }
        }

        private string _TaskDescription;
        public string TaskDescription
        {
            get { return _TaskDescription; }
            set
            {
                _TaskDescription = value;
                OnPropertyChanged();
            }
        }

        private DateTime _TaskFinishDate = DateTime.Now;
        public DateTime TaskFinishDate
        {
            get { return _TaskFinishDate; }
            set
            {
                _TaskFinishDate = value;
                OnPropertyChanged();
            }
        }

        private List<User> _Users;
        public List<User> Users
        {
            get { return _Users; }
            set
            {
                _Users = value;
                OnPropertyChanged();
            }
        }

        private User _SelectedUser;
        public User SelectedUser
        {
            get { return _SelectedUser; }
            set
            {
                _SelectedUser = value;
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
                        int projectId = int.Parse(System.Windows.Application.Current.Properties["ProjectId"].ToString());
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
                        int projectId = int.Parse(System.Windows.Application.Current.Properties["ProjectId"].ToString());
                        int taskId = (await _dbRepository.GetTask(int.Parse(System.Windows.Application.Current.Properties["TaskId"].ToString()))).Id;
                        
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
