using BusinessLogicModule.Interfaces;
using NLog;
using System;
using System.Windows;
using System.Windows.Input;
using UIModule.Utils;

namespace UIModule.ViewModels
{
    public class MainWindowViewModel : NavigateViewModel
    {
        string _dialogIdentifier = "MainDialog";
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
                    Navigate("Pages/Profile.xaml");
                });
            }
        }

        public ICommand OpenProjects
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    Navigate("Pages/Projects.xaml");
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
                    try
                    {
                        Application.Current.Properties["UserName"] = null;
                        var displayRootRegistry = (Application.Current as App).displayRootRegistry;
                        await displayRootRegistry.ShowModalPresentation(new AuthorizationWindowViewModel(_userRepository));
                        CloseAction();
                    }
                    catch(Exception ex)
                    {
                        logger.Error(ex.ToString());
                        ErrorHandler.Show(ex.Message, _dialogIdentifier);
                    }
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
