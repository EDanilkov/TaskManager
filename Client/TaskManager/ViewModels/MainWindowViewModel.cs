using BusinessLogicModule.Interfaces;
using NLog;
using System;
using System.Windows;
using System.Windows.Input;

namespace UIModule.ViewModels
{
    public class MainWindowViewModel : NavigateViewModel
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        IUserRepository _userRepository;

        public MainWindowViewModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
       

        #region Methods

        public ICommand OpenProfile
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    if(Application.Current.Properties["UserName"] != null)
                    {

                        Navigate("Pages/Profile.xaml");
                    }
                    else
                    {
                        MessageBox.Show(Application.Current.Resources["m_identify"].ToString());
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
                    if (Application.Current.Properties["UserName"] != null)
                    {
                        Navigate("Pages/Projects.xaml");
                    }
                    else
                    {
                        MessageBox.Show("Вы не вошли в свой аккаунт");
                    }
                });
            }
        }

        
        public static Action CloseAction { get; set; }

        public ICommand Exit
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    Application.Current.Properties["UserName"] = null;
                    var displayRootRegistry = (Application.Current as App).displayRootRegistry;
                    await displayRootRegistry.ShowModalPresentation(new AuthorizationWindowViewModel(_userRepository));
                    CloseAction();
                });
            }
        }

        public ICommand Loaded
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Application.Current.Properties["WindowHeight"] = Height - 220;
                });
            }
        }

        public ICommand SizeChanged
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Application.Current.Properties["WindowHeight"] = Height - 220;
                });
            }
        }

        public ICommand StateChanged
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Application.Current.Properties["WindowHeight"] = Height - 220;
                });
            }
        }
        #endregion
    }
}
