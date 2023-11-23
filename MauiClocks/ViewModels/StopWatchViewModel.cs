using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiClocks.ViewModels
{
    public partial class StopWatchViewModel : ObservableObject
    {
        [ObservableProperty]
        string content;

        [ObservableProperty]
        Color selectedColor;


        public StopWatchViewModel()
        {
            Content = "Start";
            RedBtn = true;
        }

        [ObservableProperty]
        bool greenBtn;

        [ObservableProperty]
        bool redBtn;

        [ObservableProperty]
        bool orangeBtn;

        [ObservableProperty]
        bool yellowBtn;

        partial void OnRedBtnChanged(bool value)
        {
            if (value)
            {
                SelectedColor = Colors.Red;
            }
        }

        partial void OnYellowBtnChanged(bool value)
        {
            if (value)
            {
                SelectedColor = Colors.Yellow;
            }
        }

        partial void OnGreenBtnChanged(bool value)
        {
            if (value)
            {
                SelectedColor = Color.FromRgb(0x0, 0xff, 0x0);
            }
        }

        partial void OnOrangeBtnChanged(bool value)
        {
            if (value)
            {
                SelectedColor = Colors.Orange;
            }
        }
    }
}