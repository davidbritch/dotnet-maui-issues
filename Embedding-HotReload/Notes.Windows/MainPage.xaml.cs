using Microsoft.Maui;
using Microsoft.Maui.Embedding;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Platform;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Notes.Windows.Models;
using Notes.Windows.Views;
using System.IO;
using Windows.UI.Core;

namespace Notes.Windows
{
    public sealed partial class MainPage : Page
    {
        NotesPage _notesPage;
        NoteEntryPage _noteEntryPage;

        public static MainPage Instance;

        public MauiContext MauiContext { get; private set; }

        public string FolderPath { get; private set; }

        public MainPage()
        {
            this.NavigationCacheMode = NavigationCacheMode.Enabled;

            Instance = this;

            FolderPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData));

            MauiAppBuilder builder = MauiApp.CreateBuilder();
            builder.UseMauiEmbedding<Microsoft.Maui.Controls.Application>();
            MauiApp mauiApp = builder.Build();
            MauiContext = new MauiContext(mauiApp.Services);

            Microsoft.Maui.Controls.Application.Current = new Microsoft.Maui.Controls.Application();
            Microsoft.Maui.Controls.Application.Current.Resources = new MyDictionary();

            _notesPage = new NotesPage(FolderPath)
            {
                // Set the parent so that the app-level resource dictionary can be located.
                Parent = Microsoft.Maui.Controls.Application.Current
            };

            this.Content = _notesPage.ToPlatform(MauiContext);

            this.Loaded += OnMainPageLoaded;
            //SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;

            _notesPage.Parent = null;
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

        void OnMainPageLoaded(object sender, RoutedEventArgs e)
        {
            this.Frame.SizeChanged += (o, args) =>
            {
                if (_noteEntryPage != null)
                    _noteEntryPage.Layout(new Microsoft.Maui.Graphics.Rect(0, 0, args.NewSize.Width, args.NewSize.Height));
                else
                    _notesPage.Layout(new Microsoft.Maui.Graphics.Rect(0, 0, args.NewSize.Width, args.NewSize.Height));
            };
        }

        public void NavigateToNoteEntryPage(Note note)
        {
            //_noteEntryPage = new NoteEntryPage
            //{
            //    BindingContext = note,
            //    // Set the parent so the app-level resource dictionary can be located.
            //    Parent = Microsoft.Maui.Controls.Application.Current
            //};
            this.Frame.Navigate(typeof(DetailPage), note);
            //this.Frame.Navigate(_noteEntryPage);
            //_noteEntryPage.Parent = null;
        }

        public void NavigateBack()
        {
            this.Frame.GoBack();
            _noteEntryPage = null;
        }
    }
}
