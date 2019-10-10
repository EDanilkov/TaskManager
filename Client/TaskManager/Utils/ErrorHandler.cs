using MaterialDesignThemes.Wpf;
using NLog;
using System;
using System.Windows.Input;
using UIModule.ViewModels;

namespace UIModule.Utils
{
    public static class ErrorHandler
    {
        public static void Show(string errorMessage, string dialogIdentifier)
        {
            Identifier = dialogIdentifier;
            errorShowClick.Execute(errorMessage);
        }

        private static string Identifier;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static ICommand errorShowClick => new DelegateCommand(ErrorShow);

        private static async void ErrorShow(object o)
        {
            try
            {
                var view = new Pages.Error
                {
                    DataContext = new ErrorViewModel(o.ToString())
                };
                var result = await DialogHost.Show(view, Identifier);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }
    }
}
