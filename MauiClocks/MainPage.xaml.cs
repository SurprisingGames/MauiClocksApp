using MauiClocks.ViewModels;
namespace MauiClocks
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new StopWatchViewModel();
        }
    }
}
