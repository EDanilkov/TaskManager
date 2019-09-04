using BusinessLogicModule.ViewModel;
using Newtonsoft.Json;
using SharedServicesModule;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace BusinessLogicModule
{
    public class AddNewTaskViewModel : NavigateViewModel
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

        private DateTime _TaskFinishDate;
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
