using BusinessLogicModule.ViewModel;
using SharedServicesModule;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BusinessLogicModule
{
    public class RegViewModel : NavigateViewModel
    {
        IRepository _dbRepository = new DBRepository();

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
                return new DelegateCommand(async(obj) =>
                {
                    try
                    {
                        var passwordBox = obj as PasswordBox;
                        var password = passwordBox.Password;

                        User user = new User();
                        user.Login = Login.ToString();
                        user.Password = password;
                        await _dbRepository.GetToken(user);

                        System.Windows.Application.Current.Properties["UserName"] = Login;
                        Navigate("Pages/Projects.xaml");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Application.Current.Resources["m_error_enter"].ToString() + "\n" + ex.Message);
                    }
                });
            }
        }

        public ICommand Registration
        {
            get
            {
                return new DelegateCommand(async(obj) =>
                {
                    try
                    {
                        var passwordBox = obj as PasswordBox;
                        var password = passwordBox.Password;
                        if (!string.Equals(password, "") && !string.Equals(Login, ""))
                        {
                            if ((await _dbRepository.GetUsers()).Where(c => string.Equals(c.Login, Login)).ToList().Count == 0)
                            {
                                User user = new User() { Login = Login, Password = password, RegistrationDate = DateTime.Now };
                                await _dbRepository.AddUser(user);
                                ShowError("Вы успешно зарегестрированы", Constants.Success);
                            }
                            else
                            {
                                ShowError("Данный логин уже существует", Constants.Error);
                            }
                            
                        }
                        else
                        {
                            ShowError("Вы обязаны ввести все поля", Constants.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Application.Current.Resources["m_error_add_user"].ToString() + "\n" + ex.Message);
                    }
                });
            }
        }
    }
}
