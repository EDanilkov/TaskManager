using BusinessLogicModule.ViewModel;
using Newtonsoft.Json;
using SharedServicesModule;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace BusinessLogicModule
{
    public class AddNewProjectViewModel : NavigateViewModel
    {
        private string _ProjectName;
        public string ProjectName
        {
            get { return _ProjectName; }
            set
            {
                _ProjectName = value;
                OnPropertyChanged();
            }
        }

        private string _ProjectDescription;
        public string ProjectDescription
        {
            get { return _ProjectDescription; }
            set
            {
                _ProjectDescription = value;
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
