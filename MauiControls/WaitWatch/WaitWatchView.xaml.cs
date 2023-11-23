using MauiControls.WaitWatch.Drawable;

namespace MauiControls.WaitWatch;

public partial class WaitWatchView : ContentView
{
    private static ClockDrawable? _stopWatch;
    public static readonly BindableProperty StartProperty =
            BindableProperty.Create(nameof(Start), typeof(bool), typeof(WaitWatchView), default(bool),
                        propertyChanged: OnSwitchChanged);

    public bool Start
    {
        get => (bool)GetValue(StartProperty);
        set => SetValue(StartProperty, value);
    }

    private static void OnSwitchChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is WaitWatchView customControl)
        {
            customControl.StartWatch();
        }
    }


    public static readonly BindableProperty PaintProperty =
        BindableProperty.Create(nameof(Color), typeof(Color), typeof(WaitWatchView), defaultValue: Colors.Green,
                    propertyChanged: OnPaintChanged);


    public Color Paint
    {
        get => (Color)GetValue(PaintProperty);
        set => SetValue(PaintProperty, value);
    }

    private static void OnPaintChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is WaitWatchView customControl)
        {
            Color color = (Color)newValue;
            customControl.Paint = color;
            _stopWatch?.SetNewColor(color);
        }
    }


    public void StartWatch()
    {
        _timer.Start();
    }
    readonly System.Timers.Timer _timer;
    public WaitWatchView()
    {
        InitializeComponent();
        _stopWatch = (ClockDrawable)Resources["Drawable"];
        _timer = new System.Timers.Timer(1000);
        _timer.Elapsed += Timer_Elapsed;
        StartWatch();
    }

    private void Timer_Elapsed(object obj, System.Timers.ElapsedEventArgs e)
    {
        if (Start)
        {
            _timer.Start();
            StopWatchView.Invalidate();
        }
        else
        {
            _timer.Stop();
        }
    }
}