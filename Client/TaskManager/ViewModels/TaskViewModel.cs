using BusinessLogicModule.Interfaces;
using BusinessLogicModule.Interfaces;
using BusinessLogicModule.Services;
using MaterialDesignThemes.Wpf;
using NLog;
using SharedServicesModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace UIModule.ViewModels
{
    public class TaskViewModel : NavigateViewModel
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        IUserRepository _userRepository;
        ITaskRepository _taskRepository;
        IRoleRepository _roleRepository;
        IPermissionRepository _permissionRepository;
        IProjectRepository _projectRepository;

        public TaskViewModel()
        {
            Title = "Task";

            _userRepository = new UserRepository();
            _taskRepository = new TaskRepository();
            _roleRepository = new RoleRepository();
            _permissionRepository = new PermissionRepository();
            _projectRepository = new ProjectRepository();
        }

        private string _titleName;
        public string TitleName
        {
            get { return _titleName; }
            set
            {
                _titleName = value;
                OnPropertyChanged();
            }
        }

        private string _titleProject;
        public string TitleProject
        {
            get { return _titleProject; }
            set
            {
                _titleProject = value;
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

        private TextDecorationCollection _underlineProject;
        public TextDecorationCollection UnderlineProject
        {
            get { return _underlineProject; }
            set
            {
                _underlineProject = value;
                OnPropertyChanged();
            }
        }

        private TextDecorationCollection _underlineProjects;
        public TextDecorationCollection UnderlineProjects
        {
            get { return _underlineProjects; }
            set
            {
                _underlineProjects = value;
                OnPropertyChanged();
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

                        List<Permission> permissions = await _permissionRepository.GetPermissionsFromRole((await _roleRepository.GetRoleFromUser(userName, projectId)).Id);
                        if (permissions.Any(c => string.Equals(c.Name, "ChangeTask")) != false)
                        {
                            ChangeTaskVisibility = Visibility.Visible;
                        }
                        if (permissions.Any(c => string.Equals(c.Name, "DeleteTask")) != false)
                        {
                            DeleteTaskVisibility = Visibility.Visible;
                        }
                        
                        Task task = await _taskRepository.GetTask(taskId);
                        TitleName += "/" + task.Name;
                        Project project = await _projectRepository.GetProject(projectId);
                        TitleProject += project.Name;
                        UserName = ": " + (await _userRepository.GetUser(id: task.UserId)).Login;
                        TaskDescriprion = task.Description; 
                        TaskFinishDate = ": " + task.EndDate.ToShortDateString();
                    }
                    catch (Exception ex)
                    {
                        logger.Debug(ex.ToString());
                        MessageBox.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message);
                    }
                });
            }
        }

        public ICommand UnderlineOnProjects
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    try
                    {
                        UnderlineProjects = TextDecorations.Underline;
                    }
                    catch (Exception e)
                    {
                        logger.Debug(e.ToString());
                        MessageBox.Show(e.Message);
                    }
                });
            }
        }

        public ICommand UnderlineOnProject
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    try
                    {
                        UnderlineProject = TextDecorations.Underline;
                    }
                    catch (Exception e)
                    {
                        logger.Debug(e.ToString());
                        MessageBox.Show(e.Message);
                    }
                });
            }
        }

        public ICommand UnderlineOffProjects
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    try
                    {
                        UnderlineProjects = null;
                    }
                    catch (Exception e)
                    {
                        logger.Debug(e.ToString());
                        MessageBox.Show(e.Message);
                    }
                });
            }
        }

        public ICommand UnderlineOffProject
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    try
                    {
                        UnderlineProject = null;
                    }
                    catch (Exception e)
                    {
                        logger.Debug(e.ToString());
                        MessageBox.Show(e.Message);
                    }
                });
            }
        }

        public ICommand OpenProjects
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    try
                    {
                        Navigate("Pages/Projects.xaml");
                    }
                    catch (Exception e)
                    {
                        logger.Debug(e.ToString());
                        MessageBox.Show(e.Message);
                    }
                });
            }
        }

        public ICommand OpenProject
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    try
                    {
                        Navigate("Pages/Project.xaml");
                    }
                    catch (Exception e)
                    {
                        logger.Debug(e.ToString());
                        MessageBox.Show(e.Message);
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
                        await _taskRepository.DeleteTask(int.Parse(System.Windows.Application.Current.Properties["TaskId"].ToString()));
                        Navigate("Pages/Project.xaml");
                    }
                    catch (Exception ex)
                    {
                        logger.Debug(ex.ToString());
                        MessageBox.Show(Application.Current.Resources["m_error_delete_task"].ToString() + "\n" + ex.Message);
                    }
                });
            }
        }

        public ICommand ChangeTaskClick => new DelegateCommand(ChangeTask);

        private async void ChangeTask(object o)
        {
            var view = new Pages.ChangeTask
            {
                DataContext = new ChangeTaskViewModel()
            };

            var result = await DialogHost.Show(view, "ChangeTaskDialog", ClosingEventHandler);
            
            string userName = System.Windows.Application.Current.Properties["UserName"].ToString();
            int taskId = int.Parse(System.Windows.Application.Current.Properties["TaskId"].ToString());
            int projectId = int.Parse(System.Windows.Application.Current.Properties["ProjectId"].ToString());
            Task task = await _taskRepository.GetTask(taskId);
            TitleName = "";
            TitleName += "/" + task.Name;
            Project project = await _projectRepository.GetProject(projectId);
            TitleProject = "";
            TitleProject += project.Name;
            UserName = ": " + (await _userRepository.GetUser(id: task.UserId)).Login;
            TaskDescriprion = task.Description;
            TaskFinishDate = ": " + task.EndDate.ToShortDateString();
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("You can intercept the closing event, and cancel here.");
        }
    }
}
