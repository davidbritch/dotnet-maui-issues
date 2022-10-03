namespace TapDemo;

public partial class MainPage : ContentPage
{
	int primaryTap;
	int secondaryTap;
	int eitherTap;
    int primaryTap1;
    int secondaryTap2;
    int eitherTap3;

    public MainPage()
	{
		InitializeComponent();
	}

	void OnPrimaryTapped(object sender, TappedEventArgs e)
	{
		primaryTap++;
		primaryTapLabel.Text = primaryTap.ToString();
	}

    void OnSecondaryTapped(object sender, TappedEventArgs e)
    {
		secondaryTap++;
		secondaryTapLabel.Text = secondaryTap.ToString();
    }

    void OnEitherTapped(object sender, TappedEventArgs e)
    {
		eitherTap++;
		eitherTapLabel.Text = eitherTap.ToString();
    }

    void On1PrimaryTapped(object sender, TappedEventArgs e)
    {
        primaryTap1++;
        primaryTap1Label.Text = primaryTap1.ToString();
    }

    void On2SecondaryTapped(object sender, TappedEventArgs e)
    {
        secondaryTap2++;
        secondaryTap2Label.Text = secondaryTap2.ToString();
    }

    void On3EitherTapped(object sender, TappedEventArgs e)
    {
        eitherTap3++;
        eitherTap3Label.Text = eitherTap3.ToString();
    }
}

