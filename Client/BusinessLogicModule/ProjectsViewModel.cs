using BusinessLogicModule.ViewModel;
using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;
using SharedServicesModule;
using System;
using System.Collections.Generic;
using System.Net.Http;
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
        private ICommand _AddNewProjectClick;

        public ICommand AddNewProjectClick
        {
            get
            {
                if (_AddNewProjectClick == null)
                {
                    _AddNewProjectClick = new RelayCommand(() =>
                    {
                        Navigate("Pages/AddNewProject.xaml");
                    });
                }
                return _AddNewProjectClick;
            }
            set { _AddNewProjectClick = value; }
        }
        
        IRepository _dbRepository = new DBRepository();
        
        private List<Project> _ListProjects = new List<Project>();
        public List<Project> ListProjects
        {
            get { return _ListProjects; }
            set
            {
                _ListProjects = value;
                OnPropertyChanged();
            }
        }

        private Project _SelectedProject;
        public Project SelectedProject
        {
            get { return _SelectedProject; }
            set
            {
                _SelectedProject = value;
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

        private ICommand _SelectionChanged;

        public ICommand SelectionChanged
        {
            get
            {
                if (_SelectionChanged == null)
                {
                    _SelectionChanged = new RelayCommand(() =>
                    {
                        System.Windows.Application.Current.Properties["ProjectId"] = SelectedProject.Id; 
                        Navigate("Pages/Project.xaml");
                    });
                }
                return _SelectionChanged;
            }
            set { _SelectionChanged = value; }
        }
    }
}
