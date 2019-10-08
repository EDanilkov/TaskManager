using BusinessLogicModule.Interfaces;
using BusinessLogicModule.Repositories;
using SharedServicesModule.Models;
using System;
using System.Windows;
using System.Windows.Input;

namespace UIModule.ViewModels
{
    class AddNewProjectViewModel : NavigateViewModel
    {
        IUserRepository _userRepository;
        IProjectRepository _projectRepository;

        public AddNewProjectViewModel()
        {
            _userRepository = new UserRepository();
            _projectRepository = new ProjectRepository();
        }

        private string _projectName;
        public string ProjectName
        {
            get { return _projectName; }
            set
            {
                _projectName = value;
                OnPropertyChanged();
            }
        }

        private string _projectDescription;
        public string ProjectDescription
        {
            get { return _projectDescription; }
            set
            {
                _projectDescription = value;
                OnPropertyChanged();
            }
        }

        public ICommand Accept
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        if (ProjectName != null && ProjectDescription != null)
                        {
                            Project project = new Project()
                            {
                                Name = ProjectName,
                                Description = ProjectDescription,
                                AdminId = (await _userRepository.GetUser(Application.Current.Properties["UserName"].ToString())).Id,
                                RegistrationDate = DateTime.Now
                            };
                            await _projectRepository.AddProject(project);
                            Navigate("Pages/Projects.xaml");
                            MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
                        }
                        else
                        {
                            MessageBox.Show(Application.Current.Resources["m_correct_entry"].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Application.Current.Resources["m_error_create_project"].ToString() + "\n" + ex.Message);
                    }
                });
            }
        }
    }
}
