using UIModule.Utils;

namespace UIModule.ViewModels
{
    class ViewModelLocator
    {
        public AddNewMemberViewModel AddNewMemberViewModel
        {
            get { return IocKernel.Get<AddNewMemberViewModel>(); } 
        }

        public AddNewProjectViewModel AddNewProjectViewModel
        {
            get { return IocKernel.Get<AddNewProjectViewModel>(); } 
        }

        public AddNewTaskViewModel AddNewTaskViewModel
        {
            get { return IocKernel.Get<AddNewTaskViewModel>(); }
        }

        public AuthorizationWindowViewModel AuthorizationWindowViewModel
        {
            get { return IocKernel.Get<AuthorizationWindowViewModel>(); }
        }

        public ChangeTaskViewModel ChangeTaskViewModel
        {
            get { return IocKernel.Get<ChangeTaskViewModel>(); }
        }

        public ProfileViewModel ProfileViewModel
        {
            get { return IocKernel.Get<ProfileViewModel>(); }
        }

        public ProjectsViewModel ProjectsViewModel
        {
            get { return IocKernel.Get<ProjectsViewModel>(); } 
        }

        public ProjectViewModel ProjectViewModel
        {
            get { return IocKernel.Get<ProjectViewModel>(); }
        }

        public TaskViewModel TaskViewModel
        {
            get { return IocKernel.Get<TaskViewModel>(); }
        }
    }
}
