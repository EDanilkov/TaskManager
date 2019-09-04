using BusinessLogicModule.API;
using BusinessLogicModule.ViewModel;
using Newtonsoft.Json;
using SharedServicesModule;
using SharedServicesModule.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BusinessLogicModule
{
    public class RegViewModel : NavigateViewModel
    {
        IRepository _dbRepository = new DBRepository();

        private string _Login;
        public string Login
        {
            get { return _Login; }
            set
            {
                _Login = value;
                OnPropertyChanged();
            }
        }

        private string _TextError;
        public string TextError
        {
            get { return _TextError; }
            set
            {
                _TextError = value;
                OnPropertyChanged();
            }
        }

        private Brush _ColorError;
        public Brush ColorError
        {
            get { return _ColorError; }
            set
            {
                _ColorError = value;
                OnPropertyChanged();
            }
        }


        private Visibility _VisibilityError = Visibility.Hidden;
        public Visibility VisibilityError
        {
            get { return _VisibilityError; }
            set
            {
                _VisibilityError = value;
                OnPropertyChanged();
            }
        }

        private void ShowError(string textError, string colorError)
        {
            TextError = textError;
            ColorError = (Brush)new BrushConverter().ConvertFrom(colorError);
            VisibilityError = Visibility.Visible;
        }

        public ICommand Loaded
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                });
            }
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
                                ShowError("Вы успешно зарегестрированы", Literals.Success);
                            }
                            else
                            {
                                ShowError("Данный логин уже существует", Literals.Error);
                            }
                            
                        }
                        else
                        {
                            ShowError("Вы обязаны ввести все поля", Literals.Error);
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
