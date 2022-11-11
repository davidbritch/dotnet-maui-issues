using Microsoft.Maui;
using Microsoft.Maui.Platform;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Notes.Windows.Models;
using Notes.Windows.Views;
using Windows.UI.Core;
using Microsoft.UI.Xaml;

namespace Notes.Windows
{
    public sealed partial class DetailPage : Page
    {
        NoteEntryPage _noteEntryPage;
        public static DetailPage Instance;

        public DetailPage()
        {
            this.InitializeComponent();
            Instance = this;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Note note = e.Parameter as Note;
            _noteEntryPage = new NoteEntryPage
            {
                BindingContext = note,
                // Set the parent so that the app-level resource dictionary can be located.
                Parent = Microsoft.Maui.Controls.Application.Current
            };

            this.Content = _noteEntryPage.ToPlatform(MainPage.Instance.MauiContext);

            //SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;

            _noteEntryPage.Parent = null;
        }

        void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame.CanGoBack)
            {
                e.Handled = true;
                rootFrame.GoBack();
                _noteEntryPage = null;
            }
        }

        public void NavigateBack()
        {
            this.Frame.GoBack();
            _noteEntryPage = null;
        }
    }
}
