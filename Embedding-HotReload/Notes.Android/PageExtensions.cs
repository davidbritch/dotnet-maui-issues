using Android.OS;
using Android.Views;
using Microsoft.Maui.Platform;
using Fragment = AndroidX.Fragment.App.Fragment;

namespace Notes.Android
{
    public static class PageExtensions
    {
        public static Fragment CreateSupportFragment(this ContentPage view, MauiContext context)
        {
            return new ScopedFragment(view, context);
        }

        internal class ScopedFragment : Fragment
        {
            readonly IMauiContext _mauiContext;

            public IView DetailView { get; private set; }

            public ScopedFragment(IView detailView, IMauiContext mauiContext)
            {
                DetailView = detailView;
                _mauiContext = mauiContext;
            }

            public override global::Android.Views.View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
            {
                return DetailView.ToPlatform(_mauiContext);
            }
        }
    }
}
