using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nerdytinder
{
    public interface IToastNotifier
    {
        Task<bool> Notify(ToastNotificationType type, string title, string description, TimeSpan duration, object context = null);

        void HideAll();
    }

    public enum ToastNotificationType
    {
        Info,
        Success,
        Error,
        Warning,
    }
}
