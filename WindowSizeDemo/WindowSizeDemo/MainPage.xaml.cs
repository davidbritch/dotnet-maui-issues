namespace WindowSizeDemo;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        BindingContext = this;
	}

	void OnNewWindowClicked(object sender, EventArgs e)
	{
        try
        {
            Application.Current.OpenWindow(new Window(new MainPage()));
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
        }

    }

    void OnCloseWindowClicked(object sender, EventArgs e)
    {
        Application.Current.CloseWindow(Window);
    }

    void OnSetMinimumSizeClicked(object sender, EventArgs e)
    {
        Window.MinimumWidth = 640;
        Window.MinimumHeight = 480;
    }

    void OnSetMaximumSizeClicked(object sender, EventArgs e)
    {
        Window.MaximumWidth = 800;
        Window.MaximumHeight = 600;
    }

    void OnSetFreeSizeClicked(object sender, EventArgs e)
    {
        Window.MaximumWidth = double.PositiveInfinity;
        Window.MaximumHeight = double.PositiveInfinity;

        Window.MinimumWidth = 0;
        Window.MinimumHeight = 0;
    }

    void OnSetCustomSizeClicked(object sender, EventArgs e)
    {
#if MACCATALYST
        Window.MinimumWidth = 700;
        Window.MaximumWidth = 700;
        Window.MinimumHeight = 500;
        Window.MaximumHeight = 500;

        // Give the Window time to resize
        Dispatcher.Dispatch(() =>
        {
            Window.MinimumWidth = 0;
            Window.MinimumHeight = 0;
            Window.MaximumWidth = double.PositiveInfinity;
            Window.MaximumHeight = double.PositiveInfinity;
        });
#elif WINDOWS
        Window.Width = 700;
        Window.Height = 500;
#endif
    }

    void OnCenterWindowClicked(object sender, EventArgs e)
    {
        var displayInfo = DeviceDisplay.Current.MainDisplayInfo;
        Window.X = (displayInfo.Width / displayInfo.Density - Window.Width) / 2;
        Window.Y = (displayInfo.Height / displayInfo.Density - Window.Height) / 2;
    }
}
