using GalaSoft.MvvmLight.Messaging;
using SharedServicesModule;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using UIModule.Pages;

namespace UIModule
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Frame.Navigate(new Reg());
            NavigationSetup();

            

            App.LanguageChanged += LanguageChanged;

            CultureInfo currLang = App.Language;
            
            menuLanguage.Items.Clear();
            foreach (var lang in App.Languages)
            {
                MenuItem menuLang = new MenuItem();
                menuLang.Header = lang.DisplayName;
                menuLang.Tag = lang;
                menuLang.IsChecked = lang.Equals(currLang);
                menuLang.Click += ChangeLanguageClick;
                menuLanguage.Items.Add(menuLang);
            }
        }

        private void LanguageChanged(Object sender, EventArgs e)
        {
            CultureInfo currLang = App.Language;

            //Отмечаем нужный пункт смены языка как выбранный язык
            foreach (MenuItem i in menuLanguage.Items)
            {
                CultureInfo ci = i.Tag as CultureInfo;
                i.IsChecked = ci != null && ci.Equals(currLang);
            }
        }

        private void ChangeLanguageClick(Object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if (mi != null)
            {
                CultureInfo lang = mi.Tag as CultureInfo;
                if (lang != null)
                {
                    App.Language = lang;
                }
            }

        }

        void NavigationSetup()
        {
            Messenger.Default.Register<NavigateArgs>(this, (x) =>
            {
                Frame.Navigate(new Uri(x.Url, UriKind.Relative));
            });
        }
    }
}
