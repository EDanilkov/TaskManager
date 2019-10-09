using BusinessLogicModule.Interfaces;
using GalaSoft.MvvmLight.CommandWpf;
using MaterialDesignThemes.Wpf;
using NLog;
using SharedServicesModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace UIModule.ViewModels
{
    public class ProjectsViewModel : NavigateViewModel
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        IProjectRepository _projectRepository;
        IUserRepository _userRepository;
        IRoleRepository _roleRepository;

        public ProjectsViewModel(IProjectRepository projectRepository, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            Title = "Projects";

            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
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

        #region Properties

        private List<RecordListBox> _listProjects = new List<RecordListBox>();
        public List<RecordListBox> ListProjects
        {
            get { return _listProjects; }
            set
            {
                _listProjects = value;
                OnPropertyChanged();
            }
        }
        
        private string _filter;
        public string Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                OnPropertyChanged();
            }
        }

        private Visibility _notProjectsVisibility = Visibility.Collapsed;
        public Visibility NotProjectsVisibility
        {
            get { return _notProjectsVisibility; }
            set
            {
                _notProjectsVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _projectsVisibility = Visibility.Visible;
        public Visibility ProjectsVisibility
        {
            get { return _projectsVisibility; }
            set
            {
                _projectsVisibility = value;
                OnPropertyChanged();
            }
        }

        private RecordListBox _selectedProject;
        public RecordListBox SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                _selectedProject = value;
                OnPropertyChanged();
            }
        }
        
        private bool _personal;
        public bool Personal
        {
            get { return _personal; }
            set
            {
                _personal = value;
                OnPropertyChanged();
            }
        }

        private string _chipRole;
        public string ChipRole
        {
            get { return _chipRole; }
            set
            {
                _chipRole = value;
                OnPropertyChanged();
            }
        }



        #endregion

        #region Methods

        private ICommand _selectionChanged;
        public ICommand SelectionChanged
        {
            get
            {
                if (_selectionChanged == null)
                {
                    _selectionChanged = new RelayCommand(() =>
                    {
                        System.Windows.Application.Current.Properties["ProjectId"] = SelectedProject.Id;
                        Navigate("Pages/Project.xaml");
                    });
                }
                return _selectionChanged;
            }
            set { _selectionChanged = value; }
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
                        logger.Debug(ex.ToString());
                        MessageBox.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message);
                    }
                });
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
                        ListProjects = await GetRecordListBoxes();
                    }
                    catch (Exception ex)
                    {
                        logger.Debug(ex.ToString());
                        MessageBox.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message);
                    }
                });
            }
        }

        public ICommand PersonalChecked
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        User user = await _userRepository.GetUser(System.Windows.Application.Current.Properties["UserName"].ToString());
                        ListProjects = ListProjects.Where(p => p.AdminId == user.Id).ToList();
                        CheckCountProjects(ListProjects);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        logger.Debug(ex.ToString());
                    }
                    
                });
            }
        }

        public ICommand PersonalUnChecked
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        ListProjects = Filter == null ? await GetRecordListBoxes() : (await FilterProjects());
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        logger.Debug(ex.ToString());
                    }
                });
            }
        }

        public ICommand FilterChanged
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        List<RecordListBox> recordListBoxes = await FilterProjects();
                        if (Personal)
                        {
                            User user = await _userRepository.GetUser(System.Windows.Application.Current.Properties["UserName"].ToString());
                            recordListBoxes = recordListBoxes.Where(p => p.AdminId == user.Id).ToList();
                        }
                        CheckCountProjects(recordListBoxes);
                        ListProjects = recordListBoxes;
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        logger.Debug(ex.ToString());
                    }
                });
            }
        }

        private async Task<List<RecordListBox>> GetRecordListBoxes()
        {
            string userName = Application.Current.Properties["UserName"].ToString();
            List<RecordListBox> recordListBoxes = new List<RecordListBox>();
            List<Project> projects = await _projectRepository.GetProjectsFromUser(userName);
            foreach (Project project in projects)
            {
                RecordListBox recordListBox = new RecordListBox() { Id = project.Id, ProjectName = project.Name, AdminId = project.AdminId, ChipRole = (await _roleRepository.GetRoleFromUser(userName, project.Id)).Name };
                recordListBoxes.Add(recordListBox);
            }
            CheckCountProjects(recordListBoxes);
            return recordListBoxes;
        }

        private async Task<List<RecordListBox>> FilterProjects()
        {
            if (Filter != null)
            {
                string userName = Application.Current.Properties["UserName"].ToString();
                List<RecordListBox> recordListBoxes = new List<RecordListBox>();
                List<Project> projects = await _projectRepository.GetProjectsFromUser(userName);
                foreach (Project project in projects)
                {
                    if (project.Name.Contains(Filter))
                    {
                        RecordListBox recordListBox = new RecordListBox() { Id = project.Id, ProjectName = project.Name, AdminId = project.AdminId, ChipRole = (await _roleRepository.GetRoleFromUser(userName, project.Id)).Name };
                        recordListBoxes.Add(recordListBox);
                    }
                }
                CheckCountProjects(recordListBoxes);
                return recordListBoxes;
            }
            else
            {
                return await GetRecordListBoxes();
            }
        }

        private void CheckCountProjects(List<RecordListBox> recordListBoxes)
        {
            if (recordListBoxes.Count == 0)
            {
                ProjectsVisibility = Visibility.Collapsed;
                NotProjectsVisibility = Visibility.Visible;
            }
            else
            {
                ProjectsVisibility = Visibility.Visible;
                NotProjectsVisibility = Visibility.Collapsed;
            }
        }
        
        public ICommand NewProjectClick => new DelegateCommand(ExecuteRunDialog);

        private async void ExecuteRunDialog(object o)
        {
            var view = new Pages.AddNewProject
            {
                DataContext = new AddNewProjectViewModel(_userRepository, _projectRepository)
            };
            
            var result = await DialogHost.Show(view, "RootDialog", ClosingEventHandler);
            ListProjects = await GetRecordListBoxes();
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Console.WriteLine("You can intercept the closing event, and cancel here.");
        }
        #endregion
    }
}
