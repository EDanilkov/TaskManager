using BusinessLogicModule.Interfaces;
using NLog;
using SharedServicesModule.Models;
using System;
using System.Windows;
using System.Windows.Input;
using UIModule.Utils;

namespace UIModule.ViewModels
{
    class AddNewProjectViewModel : NavigateViewModel
    {
        string _dialogIdentifier = "AddProjectDialog";
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        IUserRepository _userRepository;
        IProjectRepository _projectRepository;


        public AddNewProjectViewModel(IUserRepository userRepository, IProjectRepository projectRepository)
        {
            _userRepository = userRepository;
            _projectRepository = projectRepository;
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
                            _logger.Debug("user " + Application.Current.Properties["UserName"].ToString() + " added project " + ProjectName);
                            MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(null, null);
                        }
                        else
                        {
                            ErrorHandler.Show(Application.Current.Resources["m_correct_entry"].ToString(), _dialogIdentifier);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex.ToString());
                        ErrorHandler.Show(Application.Current.Resources["m_error_create_project"].ToString() + "\n" + ex.Message, _dialogIdentifier);
                    }
                });
            }
        }
    }
}
