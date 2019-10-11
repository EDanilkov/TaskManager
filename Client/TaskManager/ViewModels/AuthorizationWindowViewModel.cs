using BusinessLogicModule.Interfaces;
using NLog;
using SharedServicesModule;
using SharedServicesModule.Models;
using SharedServicesModule.Services;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace UIModule.ViewModels
{
    public class AuthorizationWindowViewModel : NavigateViewModel
    {
        private string _dialogIdentifier = "AuthorizationDialog";
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static Action CloseAction { get; set; }
        IUserRepository _userRepository;
        
        public AuthorizationWindowViewModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #region Properties

        private string _login;
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        private string _textError;
        public string TextError
        {
            get { return _textError; }
            set
            {
                _textError = value;
                OnPropertyChanged();
            }
        }

        private Brush _colorError;
        public Brush ColorError
        {
            get { return _colorError; }
            set
            {
                _colorError = value;
                OnPropertyChanged();
            }
        }


        private Visibility _visibilityError = Visibility.Hidden;
        public Visibility VisibilityError
        {
            get { return _visibilityError; }
            set
            {
                _visibilityError = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Methods

        private void ShowError(string textError, string colorError)
        {
            TextError = textError;
            ColorError = (Brush)new BrushConverter().ConvertFrom(colorError);
            VisibilityError = Visibility.Visible;
        }

        public ICommand Enter
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        var passwordBox = obj as PasswordBox;
                        var password = passwordBox.Password;
                        if (!string.Equals(password, "") && !string.Equals(Login, ""))
                        {

                            User user = new User()
                            {
                                Login = Login.ToString(),
                                Password = password
                            };
                            await TokenService.GetToken(user);

                            System.Windows.Application.Current.Properties["UserName"] = Login;
                            var displayRootRegistry = (Application.Current as App).displayRootRegistry;
                            await displayRootRegistry.ShowModalPresentation(new MainWindowViewModel(_userRepository));
                            logger.Debug("The user " + user.Login + " is logged in to the app");
                            CloseAction();
                        }
                        else
                        {
                            ShowError(Application.Current.Resources["m_error_enter_all_fields"].ToString(), Constants.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString());
                        ShowError(Application.Current.Resources["m_error_enter"].ToString() + "\n" + ex.Message, Constants.Error);
                    }
                });
            }
        }

        public ICommand Registration
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        var passwordBox = obj as PasswordBox;
                        var password = passwordBox.Password;
                        if (!string.Equals(password, "") && !string.Equals(Login, ""))
                        {
                            if ((await _userRepository.GetUsers()).Where(c => string.Equals(c.Login, Login)).ToList().Count == 0)
                            {
                                User user = new User() { Login = Login, Password = password, RegistrationDate = DateTime.Now };
                                await _userRepository.AddUser(user);
                                ShowError(Application.Current.Resources["m_success_registered"].ToString(), Constants.Success);
                                logger.Debug("The user " + user.Login + " is registered");
                            }
                            else
                            {
                                ShowError(Application.Current.Resources["m_error_bad_login"].ToString(), Constants.Error);
                            }
                        }
                        else
                        {
                            ShowError(Application.Current.Resources["m_error_enter_all_fields"].ToString(), Constants.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString());
                        ShowError(Application.Current.Resources["m_error_add_user"].ToString(), Constants.Error);
                    }
                });
            }
        }
        #endregion
    }
}
