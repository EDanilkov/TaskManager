 using BusinessLogicModule.ViewModel;
using Newtonsoft.Json;
using SharedServicesModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;

namespace BusinessLogicModule
{
    public class ProfileViewModel : NavigateViewModel
    {
        IRepository _dbRepository = new DBRepository();

        private string _Login;
        public string Login
        {
            get { return _Login; }
            set
            {
                _Login = value;
                OnPropertyChanged();
            }
        }

        private string _TaskInfo;
        public string TaskInfo
        {
            get { return _TaskInfo; }
            set
            {
                _TaskInfo = value;
                OnPropertyChanged();
            }
        }
        

        private List<RecordViewModel> _ListRecords = new List<RecordViewModel>();
        public List<RecordViewModel> ListRecords
        {
            get { return _ListRecords; }
            set
            {
                _ListRecords = value;
                OnPropertyChanged();
            }
        }

        private RecordViewModel _SelectedTask = new RecordViewModel();
        public RecordViewModel SelectedTask
        {
            get { return _SelectedTask; }
            set
            {
                _SelectedTask = value;
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
                        Login = System.Windows.Application.Current.Properties["UserName"].ToString();
                        User user = await _dbRepository.GetUser(Login);
                        List<Task> tasks = await _dbRepository.GetTasksFromUser(user.Id);
                        List<RecordViewModel> records = new List<RecordViewModel>();
                        foreach (Task task in tasks)
                        {
                            Project project = await _dbRepository.GetProject(task.ProjectId);

                            RecordViewModel record = new RecordViewModel();
                            record.Id = task.Id;
                            record.ProjectName = project.Name;
                            record.TaskName = task.Name;
                            record.BeginDate = task.BeginDate.ToShortDateString();
                            record.EndDate = task.EndDate.ToShortDateString();
                            records.Add(record);
                        }
                        ListRecords = records;
                        TaskInfo = ListRecords.Count == 0 ? "You don't have Tasks." : "Tasks:";
                    }
                    catch (Exception ex)
                    {
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
                        User user = await _dbRepository.GetUser(Login);
                        List<SharedServicesModule.Task> tasks = await _dbRepository.GetTasksFromUser(user.Id);
                        System.Windows.Application.Current.Properties["ProjectId"] = tasks.First(c => c.Id == SelectedTask.Id).Project.Id;
                        System.Windows.Application.Current.Properties["TaskId"] = SelectedTask.Id;
                        Navigate("Pages/Task.xaml");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message);
                    }
                });
            }
        }

        
    }
}
