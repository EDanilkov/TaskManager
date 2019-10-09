namespace UIModule.ViewModels
{
    class ViewModelLocator
    {
        public AddNewProjectViewModel UserControlViewModel
        {
            get { return DI.IocKernel.Get<AddNewProjectViewModel>(); } // Loading UserControlViewModel will automatically load the binding for IStorage
        }
    }
}
