using Microsoft.Maui.Controls;
using Notes.iOS.Models;

namespace Notes.iOS.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesPage : ContentPage
    {
        public NotesPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var notes = new List<Note>();

            var files = Directory.EnumerateFiles(AppDelegate.FolderPath, "*.notes.txt");
            foreach (var filename in files)
            {
                notes.Add(new Note
                {
                    Filename = filename,
                    Text = File.ReadAllText(filename),
                    Date = File.GetCreationTime(filename)
                });
            }

            collectionView.ItemsSource = notes
                .OrderBy(d => d.Date)
                .ToList();
        }

        void OnNoteAddedClicked(object sender, EventArgs e)
        {
            AppDelegate.Instance.NavigateToNoteEntryPage(new Note());
        }

        void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = e.CurrentSelection.FirstOrDefault() as Note;
            if (item != null)
            {
                AppDelegate.Instance.NavigateToNoteEntryPage(item);
            }
        }
    }
}