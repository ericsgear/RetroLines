using Microsoft.Maui.Controls.Shapes;
using RetroLines.Models;

namespace RetroLines.Views;

public partial class MainPage : ContentPage
{
    private readonly DrawnShape shape = new(DeviceDisplay.Current.MainDisplayInfo.Height, DeviceDisplay.Current.MainDisplayInfo.Width, DeviceDisplay.Current.MainDisplayInfo.Density);
    private readonly OptionsPanel optionsPanel;
    private Timer? timer;

    public MainPage(OptionsPanel optionsPanelView)
    {
        InitializeComponent();

        AppPreferences.Load();
        optionsPanel = optionsPanelView;
        DeviceDisplay.Current.KeepScreenOn = true;
    }

    private async void ShowOptions(object sender, TappedEventArgs args)
    {
        timer?.Change(Timeout.Infinite, Timeout.Infinite);
        DeviceDisplay.Current.KeepScreenOn = false;
        await Navigation.PushAsync(optionsPanel);
    }

    private void ResetRender(object sender, TappedEventArgs args)
    {
        timer?.Change(Timeout.Infinite, Timeout.Infinite);

        StartRendering();
    }

    public void StartRendering()
    {
        Random rnd = new();

        Canvas.BackgroundColor = optionsPanel.BackgroundOptions[AppPreferences.Settings.BackgroundColor].Color;

        if (shape.Points.Count != 0)
        {
            shape.Points.Clear();
            Canvas.Children.Clear();
            shape.Elements = 0;
        }

        for (int i = 0; i < AppPreferences.Settings.PointCount; i++)
        {
            Vertex v = new(Math.Floor(rnd.NextDouble() * shape.ScreenWidth), Math.Floor(rnd.NextDouble() * shape.ScreenHeight), AppPreferences.Settings.LinearSpeed);
            shape.Points.Add(v);
        }

        timer?.Dispose();

        timer = new(new TimerCallback(ReDraw), null, TimeSpan.Zero, TimeSpan.FromMilliseconds(1000 / AppPreferences.Settings.RenderSpeed));
    }

    private void ReDraw(object? source)
    {
        Shape path = AppPreferences.Settings.PointCount switch
        {
            1 => shape.CreateCircle(),
            2 => shape.CreateLine(),
            _ => shape.CreatePolygon(),
        };

        MainThread.BeginInvokeOnMainThread(() =>
        {
            Canvas.BatchBegin();

            Canvas.Add(path);

            if (shape.Elements >= AppPreferences.Settings.Trails * 1.01)
            {
                while (shape.Elements >= AppPreferences.Settings.Trails)
                {
                    Canvas.Children.RemoveAt(0);
                    shape.Elements--;
                }
            }

            Canvas.BatchCommit();
        });
    }

    protected override void OnAppearing()
    {
        DeviceDisplay.Current.KeepScreenOn = true;
        StartRendering();
    }

    public void Dispose()
    {
        timer?.Dispose();
    }
}
