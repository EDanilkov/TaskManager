using BusinessLogicModule.ViewModel;
using SharedServicesModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace BusinessLogicModule
{
    public class TaskViewModel : NavigateViewModel
    {
        IRepository _dbRepository = new DBRepository();
        
        public TaskViewModel()
        {
            Title = "Task";
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

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        private string _taskFinishDate;
        public string TaskFinishDate
        {
            get { return _taskFinishDate; }
            set
            {
                _taskFinishDate = value;
                OnPropertyChanged();
            }
        }

        private string _taskDescriprion;
        public string TaskDescriprion
        {
            get { return _taskDescriprion; }
            set
            {
                _taskDescriprion = value;
                OnPropertyChanged();
            }
        }

        private Visibility _deleteTaskVisibility = Visibility.Collapsed;
        public Visibility DeleteTaskVisibility
        {
            get { return _deleteTaskVisibility; }
            set
            {
                _deleteTaskVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _changeTaskVisibility = Visibility.Collapsed;
        public Visibility ChangeTaskVisibility
        {
            get { return _changeTaskVisibility; }
            set
            {
                _changeTaskVisibility = value;
                OnPropertyChanged();
            }
        }

        public ICommand ChangeTaskClick
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    try
                    {
                        Navigate("Pages/ChangeTask.xaml");
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                });
            }
        }

        public ICommand Loaded
        {
            get
            {
                return new DelegateCommand(async(obj) =>
                {
                    try
                    {
                        string userName = System.Windows.Application.Current.Properties["UserName"].ToString();
                        int taskId = int.Parse(System.Windows.Application.Current.Properties["TaskId"].ToString());
                        int projectId = int.Parse(System.Windows.Application.Current.Properties["ProjectId"].ToString());

                        List<Permission> permissions = await _dbRepository.GetPermissionsFromRole((await _dbRepository.GetRoleFromUser(userName, projectId)).Id);
                        if (permissions.Any(c => string.Equals(c.Name, "ChangeTask")) != false)
                        {
                            ChangeTaskVisibility = Visibility.Visible;
                        }
                        if (permissions.Any(c => string.Equals(c.Name, "DeleteTask")) != false)
                        {
                            DeleteTaskVisibility = Visibility.Visible;
                        }
                        SharedServicesModule.Task task = await _dbRepository.GetTask(taskId);
                        TaskName = task.Name;
                        UserName = (await _dbRepository.GetUser(id: task.UserId)).Login;
                        TaskDescriprion = task.Description; 
                        TaskFinishDate = task.EndDate.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message);
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

        public ICommand DeleteTask
        {
            get
            {
                return new DelegateCommand(async(obj) =>
                {
                    try
                    {
                        await _dbRepository.DeleteTask(int.Parse(System.Windows.Application.Current.Properties["TaskId"].ToString()));
                        Navigate("Pages/Project.xaml");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Application.Current.Resources["m_error_delete_task"].ToString() + "\n" + ex.Message);
                    }
                });
            }
        }
        
    }
}
