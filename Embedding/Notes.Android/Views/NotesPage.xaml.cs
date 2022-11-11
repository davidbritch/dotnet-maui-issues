using Notes.Android.Models;

namespace Notes.Android.Views
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

            var files = Directory.EnumerateFiles(MainActivity.FolderPath, "*.notes.txt");
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
            MainActivity.Instance.NavigateToNoteEntryPage(new Note());
        }

        void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count != 0)
            {
                Note note = (Note)e.CurrentSelection[0];
                MainActivity.Instance.NavigateToNoteEntryPage(note);
            }
        }
    }
}
