namespace CompiledBindingBug
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            //label.SetBinding(Label.ScaleProperty, "Value", source: slider);
            label.SetBinding(Label.ScaleProperty, Binding.Create(static (Slider s) => s.Value, source: slider));
        }
    }
}
