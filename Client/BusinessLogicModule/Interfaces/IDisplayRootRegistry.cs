using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BusinessLogicModule.Interfaces
{
    public interface IDisplayRootRegistry
    {
        void RegisterWindowType<VM, Win>() where Win : Window, new() where VM : class;
        void UnregisterWindowType<VM>();
        Window CreateWindowInstanceWithVM(object vm);

        void ShowPresentation(object vm);
        void HidePresentation(object vm);
        Task ShowModalPresentation(object vm);
    }
}
