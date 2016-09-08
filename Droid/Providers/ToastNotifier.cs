using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using Android.Widget;
using nerdytinder.Droid;

[assembly: Dependency(typeof(nerdytinder.Droid.ToastNotifier))]

namespace nerdytinder.Droid
{
    public class ToastNotifier : IToastNotifier
    {
        public Task<bool> Notify(ToastNotificationType type, string title, string description, TimeSpan duration, object context = null)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();
            Toast.MakeText(Forms.Context, description, ToastLength.Short).Show();
            return taskCompletionSource.Task;
        }

        public void HideAll()
        {
        }
    }
}