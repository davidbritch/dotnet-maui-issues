using Notes.Windows.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Maui.Controls;

namespace Notes.Windows.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesPage : ContentPage
    {
        public NotesPage(string folderPath)
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var notes = new List<Note>();

            var files = Directory.EnumerateFiles(MainPage.Instance.FolderPath, "*.notes.txt");
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
            MainPage.Instance.NavigateToNoteEntryPage(new Note());
        }

        void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = e.CurrentSelection.FirstOrDefault() as Note;
            if (item != null)
            {
                MainPage.Instance.NavigateToNoteEntryPage(item);
            }
        }
    }
}
