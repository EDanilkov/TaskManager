namespace UIModule.ViewModels
{
    class ErrorViewModel : NavigateViewModel
    {
        public ErrorViewModel()
        {
        }

        public ErrorViewModel(string errorText)
        {
            ErrorText = errorText;
        }

        private string _taskName;
        public string ErrorText
        {
            get { return _taskName; }
            set
            {
                _taskName = value;
                OnPropertyChanged();
            }
        }
    }
}
