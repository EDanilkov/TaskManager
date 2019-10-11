namespace UIModule.ViewModels
{
    public class ErrorViewModel : NavigateViewModel
    {
        public ErrorViewModel()
        {
        }

        public ErrorViewModel(string errorText)
        {
            ErrorText = errorText;
        }

        private string _errorText;
        public string ErrorText
        {
            get { return _errorText; }
            set
            {
                _errorText = value;
                OnPropertyChanged();
            }
        }
    }
}
