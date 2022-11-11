using Android.App;
using Android.OS;
using Android.Views;
using AndroidX.AppCompat.App;
using Microsoft.Maui.Embedding;
using Notes.Android.Models;
using Notes.Android.Views;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace Notes.Android
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        MauiContext _mauiContext;

        public static string FolderPath { get; private set; }

        public static MainActivity Instance;

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Instance = this;

            MauiAppBuilder builder = MauiApp.CreateBuilder();
            builder.UseMauiEmbedding<Microsoft.Maui.Controls.Application>();
            MauiApp mauiApp = builder.Build();
            _mauiContext = new MauiContext(mauiApp.Services, this);

            SetContentView(Resource.Layout.Main);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Notes";

            FolderPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData));

            // Create app-level resource dictionary.
            Microsoft.Maui.Controls.Application.Current = new Microsoft.Maui.Controls.Application();
            Microsoft.Maui.Controls.Application.Current.Resources = new MyDictionary();

            NotesPage notesPage = new NotesPage()
            {
                // Set the parent so that the app-level resource dictionary can be located.
                Parent = Microsoft.Maui.Controls.Application.Current
            };

            AndroidX.Fragment.App.Fragment notesPageFragment = notesPage.CreateSupportFragment(_mauiContext);

            SupportFragmentManager
                .BeginTransaction()
                .Replace(Resource.Id.fragment_frame_layout, notesPageFragment)
                .Commit();

            SupportFragmentManager.BackStackChanged += (sender, e) =>
            {
                bool hasBack = SupportFragmentManager.BackStackEntryCount > 0;
                SupportActionBar.SetHomeButtonEnabled(hasBack);
                SupportActionBar.SetDisplayHomeAsUpEnabled(hasBack);
                SupportActionBar.Title = hasBack ? "Note Entry" : "Notes";
            };

            notesPage.Parent = null;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == global::Android.Resource.Id.Home && SupportFragmentManager.BackStackEntryCount > 0)
            {
                SupportFragmentManager.PopBackStack();
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        public void NavigateToNoteEntryPage(Note note)
        {
            NoteEntryPage noteEntryPage = new NoteEntryPage
            {
                BindingContext = note,
                // Set the parent so that the app-level resource dictionary can be located.
                Parent = Microsoft.Maui.Controls.Application.Current
            };

            AndroidX.Fragment.App.Fragment noteEntryFragment = noteEntryPage.CreateSupportFragment(_mauiContext);
            SupportFragmentManager
                .BeginTransaction()
                .AddToBackStack(null)
                .Replace(Resource.Id.fragment_frame_layout, noteEntryFragment)
                .Commit();

            noteEntryPage.Parent = null;
        }

        public void NavigateBack()
        {
            SupportFragmentManager.PopBackStack();
        }
    }
}