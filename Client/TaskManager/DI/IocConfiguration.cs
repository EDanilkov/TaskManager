using BusinessLogicModule.Interfaces;
using Ninject.Modules;

namespace UIModule.DI
{
    class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<IProjectRepository>().To<ProjectRepository>().InSingletonScope(); // Reuse same storage every time

            //Bind<UserControlViewModel>().ToSelf().InTransientScope(); // Create new instance every time
        }
    }
}
