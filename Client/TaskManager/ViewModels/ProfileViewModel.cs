using BusinessLogicModule.Interfaces;
using NLog;
using SharedServicesModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace UIModule.ViewModels
{
    public class ProfileViewModel : NavigateViewModel
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        IUserRepository _userRepository;
        IProjectRepository _projectRepository;

        public ProfileViewModel(IUserRepository userRepository, IProjectRepository projectRepository)
        {
            _userRepository = userRepository;
            _projectRepository = projectRepository;
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        private int _height;
        public int Height
        {
            get { return _height; }
            set
            {
                _height = value;
                OnPropertyChanged();
            }
        }

        private string _taskInfo;
        public string TaskInfo
        {
            get { return _taskInfo; }
            set
            {
                _taskInfo = value;
                OnPropertyChanged();
            }
        }
        

        private List<RecordViewModel> _listRecords = new List<RecordViewModel>();
        public List<RecordViewModel> ListRecords
        {
            get { return _listRecords; }
            set
            {
                _listRecords = value;
                OnPropertyChanged();
            }
        }

        private RecordViewModel _selectedTask = new RecordViewModel();
        public RecordViewModel SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
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
                        Height = int.Parse(Application.Current.Properties["WindowHeight"] == null ? "0" : Application.Current.Properties["WindowHeight"].ToString());

                        Login = System.Windows.Application.Current.Properties["UserName"].ToString();
                        User user = await _userRepository.GetUser(Login);
                        List<Task> tasks = await _projectRepository.GetTasksFromUser(user.Id);
                        List<RecordViewModel> records = new List<RecordViewModel>();
                        foreach (Task task in tasks)
                        {
                            Project project = await _projectRepository.GetProject(task.ProjectId);

                            RecordViewModel record = new RecordViewModel()
                            {
                                Id = task.Id,
                                ProjectName = project.Name,
                                TaskName = task.Name,
                                BeginDate = task.BeginDate.ToShortDateString(),
                                EndDate = task.EndDate.ToShortDateString()
                            };
                            records.Add(record);
                        }
                        ListRecords = records;
                        TaskInfo = ListRecords.Count == 0 ? "You don't have Tasks." : "Tasks:";
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString());
                        MessageBox.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message);
                    }
                });
            }
        }

        public ICommand SizeChanged
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        Height = int.Parse(Application.Current.Properties["WindowHeight"] == null ? "0" : Application.Current.Properties["WindowHeight"].ToString());
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString());
                        MessageBox.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message);
                    }
                });
            }
        }

        public ICommand SelectionChanged
        {
            get
            {
                return new DelegateCommand(async(obj) =>
                {
                    try
                    {
                        Login = System.Windows.Application.Current.Properties["UserName"].ToString();
                        User user = await _userRepository.GetUser(Login);
                        List<Task> tasks = await _projectRepository.GetTasksFromUser(user.Id);
                        System.Windows.Application.Current.Properties["ProjectId"] = tasks.First(c => c.Id == SelectedTask.Id).Project.Id;
                        System.Windows.Application.Current.Properties["TaskId"] = SelectedTask.Id;
                        Navigate("Pages/Task.xaml");
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString());
                        MessageBox.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message);
                    }
                });
            }
        }
    }
}
