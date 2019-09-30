using BusinessLogicModule.ViewModel;
using SharedServicesModule;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace BusinessLogicModule
{
    public class AddNewTaskViewModel : NavigateViewModel
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
                        TaskFinishDate = DateTime.Now;
                        DateTime date = TaskFinishDate;

                        string projectId = System.Windows.Application.Current.Properties["ProjectId"].ToString();
                        List<User> users = await _dbRepository.GetUsersFromProject(int.Parse(projectId));

                        Users = users;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message);
                    }
                });
            }
        }

        public ICommand Add
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        if(TaskName != null && TaskDescription != null && SelectedUser != null && TaskFinishDate != null)
                        {
                            SharedServicesModule.Task task = new SharedServicesModule.Task()
                            {
                                Name = TaskName,
                                Description = TaskDescription,
                                BeginDate = DateTime.Now,
                                ProjectId = int.Parse(System.Windows.Application.Current.Properties["ProjectId"].ToString()),
                                UserId = SelectedUser.Id,
                                EndDate = TaskFinishDate
                            };

                            await _dbRepository.AddTask(task);
                            Navigate("Pages/Project.xaml");
                        }
                        else
                        {
                            MessageBox.Show(Application.Current.Resources["m_correct_entry"].ToString());
                        }

                    }
                    catch (Exception ex)
                    {
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
