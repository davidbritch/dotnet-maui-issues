using Foundation;
using Microsoft.Maui.Embedding;
using Microsoft.Maui.Platform;
using Notes.iOS.Controllers;
using Notes.iOS.Models;
using Notes.iOS.Views;
using UIKit;

namespace Notes.iOS;

[Register("AppDelegate")]
public class AppDelegate : UIApplicationDelegate
{
    public static MauiContext MauiContext;
    public static AppDelegate Instance;
    public static string FolderPath { get; private set; }

    AppNavigationController _navigation;

    public override UIWindow? Window
    {
        get;
        set;
    }

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        Instance = this;

        // Create a new window instance based on the screen size
        Window = new UIWindow(UIScreen.MainScreen.Bounds);

        UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes
        {
            TextColor = UIColor.Black
        });

        FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder.UseMauiEmbedding<Microsoft.Maui.Controls.Application>();
        builder.Services.Add(new Microsoft.Extensions.DependencyInjection.ServiceDescriptor(typeof(UIWindow), Window));
        MauiApp mauiApp = builder.Build();
        MauiContext = new MauiContext(mauiApp.Services);

        // Create app-level resource dictionary.
        Microsoft.Maui.Controls.Application.Current = new Microsoft.Maui.Controls.Application();
        Microsoft.Maui.Controls.Application.Current.Resources = new MyDictionary();

        NotesPage notesPage = new NotesPage()
        {
            // Set the parent so that the app-level resource dictionary can be located.
            Parent = Microsoft.Maui.Controls.Application.Current
        };

        UIViewController notesPageController = notesPage.ToUIViewController(MauiContext);
        notesPageController.Title = "Notes";

        _navigation = new AppNavigationController(notesPageController);

        Window.RootViewController = _navigation;
        Window.MakeKeyAndVisible();

        notesPage.Parent = null;
        return true;
    }

    public void NavigateToNoteEntryPage(Note note)
    {
        NoteEntryPage noteEntryPage = new NoteEntryPage
        {
            BindingContext = note,
            // Set the parent so that the app-level resource dictionary can be located.
            Parent = Microsoft.Maui.Controls.Application.Current
        };

        UIViewController noteEntryViewController = noteEntryPage.ToUIViewController(MauiContext);
        noteEntryViewController.Title = "Note Entry";

        _navigation.PushViewController(noteEntryViewController, true);
        noteEntryPage.Parent = null;
    }

    public void NavigateBack()
    {
        _navigation.PopViewController(true);
    }
}

