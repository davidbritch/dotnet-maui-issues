using Notes.Android.Models;

namespace Notes.Android.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteEntryPage : ContentPage
    {
        public NoteEntryPage()
        {
            InitializeComponent();
        }

        void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;

            if (string.IsNullOrWhiteSpace(note.Filename))
            {
                // Save
                string filename = Path.Combine(MainActivity.FolderPath, $"{Path.GetRandomFileName()}.notes.txt");
                File.WriteAllText(filename, note.Text);
            }
            else
                // Update 
                File.WriteAllText(note.Filename, note.Text);

            MainActivity.Instance.NavigateBack();
        }

        void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            Note note = (Note)BindingContext;

            if (File.Exists(note.Filename))
                File.Delete(note.Filename);

            MainActivity.Instance.NavigateBack();
        }
    }
}
