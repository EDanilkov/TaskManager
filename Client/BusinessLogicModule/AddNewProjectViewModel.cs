using BusinessLogicModule.ViewModel;
using SharedServicesModule;
using System;
using System.Windows;
using System.Windows.Input;

namespace BusinessLogicModule
{
    public class AddNewProjectViewModel : NavigateViewModel
    {
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

        IRepository _dbRepository = new DBRepository();

        public ICommand Add
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        if (ProjectName != null && ProjectDescription != null)
                        {
                            string userName = Application.Current.Properties["UserName"].ToString();
                            Project project = new Project() { Name = ProjectName, Description = ProjectDescription, AdminId = (await _dbRepository.GetUser(userName)).Id, RegistrationDate = DateTime.Now };
                            await _dbRepository.AddProject(project);
                            Navigate("Pages/Projects.xaml");
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

        public ICommand Back
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Navigate("Pages/Projects.xaml");
                });
            }
        }
    }
}
