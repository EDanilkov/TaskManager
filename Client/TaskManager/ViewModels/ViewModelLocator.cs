namespace UIModule.ViewModels
{
    class ViewModelLocator
    {
        public AddNewMemberViewModel AddNewMemberViewModel
        {
            get { return DI.IocKernel.Get<AddNewMemberViewModel>(); } 
        }

        public AddNewProjectViewModel AddNewProjectViewModel
        {
            get { return DI.IocKernel.Get<AddNewProjectViewModel>(); } 
        }

        public AddNewTaskViewModel AddNewTaskViewModel
        {
            get { return DI.IocKernel.Get<AddNewTaskViewModel>(); }
        }

        public AuthorizationWindowViewModel AuthorizationWindowViewModel
        {
            get { return DI.IocKernel.Get<AuthorizationWindowViewModel>(); }
        }

        public ChangeTaskViewModel ChangeTaskViewModel
        {
            get { return DI.IocKernel.Get<ChangeTaskViewModel>(); }
        }

        public ProfileViewModel ProfileViewModel
        {
            get { return DI.IocKernel.Get<ProfileViewModel>(); }
        }

        public ProjectsViewModel ProjectsViewModel
        {
            get { return DI.IocKernel.Get<ProjectsViewModel>(); } 
        }

        public ProjectViewModel ProjectViewModel
        {
            get { return DI.IocKernel.Get<ProjectViewModel>(); }
        }

        public TaskViewModel TaskViewModel
        {
            get { return DI.IocKernel.Get<TaskViewModel>(); }
        }
    }
}
