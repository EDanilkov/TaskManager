using BusinessLogicModule.Interfaces;
using Ninject.Modules;

namespace UIModule.Utils
{
    class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<IProjectRepository>().To<ProjectRepository>().InSingletonScope();
            Bind<IRoleRepository>().To<RoleRepository>().InSingletonScope();
            Bind<IUserRepository>().To<UserRepository>().InSingletonScope();
            Bind<IPermissionRepository>().To<PermissionRepository>().InSingletonScope();
            Bind<IRolePermissionRepository>().To<RolePermissionRepository>().InSingletonScope();
            Bind<ITaskRepository>().To<TaskRepository>().InSingletonScope();
            Bind<IUserProjectRepository>().To<UserProjectRepository>().InSingletonScope();
        }
    }
}
