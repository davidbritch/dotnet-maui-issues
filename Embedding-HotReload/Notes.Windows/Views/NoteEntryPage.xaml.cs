using Microsoft.Maui.Controls;
using Notes.Windows.Models;
using System;
using System.IO;

namespace Notes.Windows.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteEntryPage : ContentPage
    {
        public NoteEntryPage()
        {
            InitializeComponent();
        }

        void OnSaveButtonClicked(object sender, EventArgs e)
        {
            Note note = (Note)BindingContext;

            if (string.IsNullOrWhiteSpace(note.Filename))
            {
                // Save
                string folderPath = MainPage.Instance.FolderPath;
                var filename = Path.Combine(folderPath, $"{Path.GetRandomFileName()}.notes.txt");
                File.WriteAllText(filename, note.Text);
            }
            else
            {
                // Update 
                File.WriteAllText(note.Filename, note.Text);
            }

            DetailPage.Instance.NavigateBack();
        }

        void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;

            if (File.Exists(note.Filename))
            {
                File.Delete(note.Filename);
            }

            DetailPage.Instance.NavigateBack();
        }
    }
}
