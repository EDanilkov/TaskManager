using BusinessLogicModule.ViewModel;
using GalaSoft.MvvmLight.CommandWpf;
using SharedServicesModule;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace BusinessLogicModule
{
    public class ProjectsViewModel : NavigateViewModel
    {
        public ProjectsViewModel()
        {
            Title = "Projects";
        }

        IRepository _dbRepository = new DBRepository();

        private ICommand _addNewProjectClick;
        public ICommand AddNewProjectClick
        {
            get
            {
                if (_addNewProjectClick == null)
                {
                    _addNewProjectClick = new RelayCommand(() =>
                    {
                        Navigate("Pages/AddNewProject.xaml");
                    });
                }
                return _addNewProjectClick;
            }
            set { _addNewProjectClick = value; }
        }
        
        private List<Project> _listProjects = new List<Project>();
        public List<Project> ListProjects
        {
            get { return _listProjects; }
            set
            {
                _listProjects = value;
                OnPropertyChanged();
            }
        }

        private Project _selectedProject;
        public Project SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                _selectedProject = value;
                OnPropertyChanged();
            }
        }

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

        public ICommand Loaded
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        List<Project> projects = await _dbRepository.GetProjectsFromUser(System.Windows.Application.Current.Properties["UserName"].ToString());
                        ListProjects = projects;
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
