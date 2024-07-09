using RetroLines.Models;

namespace RetroLines.Views;

public partial class OptionsPanel : ContentPage
{
    internal List<ColorOptions> BackgroundOptions = [
        new ("Black", Colors.Black),
        new ("White", Colors.White),
        new ("Red", Colors.Red),
        new ("Green", Colors.Green),
        new ("Blue", Colors.Blue),
        new ("Orange", Colors.Orange),
        new ("Yellow", Colors.Yellow),
        new ("Purple", Colors.Purple)
    ];

    public OptionsPanel()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        SetupBackgroundSelection();
        LoadForm();
    }

    private void LoadForm()
    {
        PointCount.Text = AppPreferences.Settings.PointCount.ToString();
        RenderSpeed.Text = AppPreferences.Settings.RenderSpeed.ToString();
        LinearSpeed.Text = AppPreferences.Settings.LinearSpeed.ToString();
        Fill.IsChecked = AppPreferences.Settings.Fill;
        FillOpacity.Text = AppPreferences.Settings.FillOpacity.ToString();
        Trails.Text = AppPreferences.Settings.Trails.ToString();
        LineThickness.Text = AppPreferences.Settings.LineThickness.ToString();

        OpacityPanel.IsEnabled = AppPreferences.Settings.Fill;
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        AppPreferences.Settings.PointCount = int.Parse(PointCount.Text);
        AppPreferences.Settings.RenderSpeed = int.Parse(RenderSpeed.Text);
        AppPreferences.Settings.LinearSpeed = int.Parse(LinearSpeed.Text);
        AppPreferences.Settings.Fill = Fill.IsChecked;
        AppPreferences.Settings.FillOpacity = int.Parse(FillOpacity.Text);
        AppPreferences.Settings.Trails = int.Parse(Trails.Text);
        AppPreferences.Settings.LineThickness = int.Parse(LineThickness.Text);
        AppPreferences.Settings.BackgroundColor = BackgroundColorDDL.SelectedIndex;

        AppPreferences.Save();
        await Navigation.PopAsync();
    }

    private async void CancelButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private void SetupBackgroundSelection()
    {
        BackgroundColorDDL.ItemsSource = BackgroundOptions;
        BackgroundColorDDL.ItemDisplayBinding = new Binding("Name");

        BackgroundColorDDL.SelectedIndex = AppPreferences.Settings.BackgroundColor;
    }

    private void Fill_Changed(object sender, EventArgs e)
    {
        OpacityPanel.IsEnabled = ((CheckBox)sender).IsChecked;
    }

    private void Opacity_Unfocused(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(FillOpacity.Text) || !int.TryParse(FillOpacity.Text, out _))
        {
            FillOpacity.Text = "0";
        }
    }

    private void Opacity_Changed(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(FillOpacity.Text) || !int.TryParse(FillOpacity.Text, out int value))
        {
            return;
        }
        else if (value < 0)
        {
            FillOpacity.Text = "0";
        }
        else if (value > 100)
        {
            FillOpacity.Text = "100";
        }
    }

    private void Trails_Unfocused(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Trails.Text) || !int.TryParse(Trails.Text, out _))
        {
            Trails.Text = "0";
        }
    }

    private void Trails_Changed(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Trails.Text) || !int.TryParse(Trails.Text, out int value))
        {
            return;
        }
        else if (value < 0)
        {
            Trails.Text = "0";
        }
        else if (value > 1000)
        {
            Trails.Text = "1000";
        }
    }

    private void Points_Unfocused(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(PointCount.Text) || !int.TryParse(PointCount.Text, out _))
        {
            PointCount.Text = "1";
        }
    }

    private void Points_Changed(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(PointCount.Text) || !int.TryParse(PointCount.Text, out int value))
        {
            return;
        }
        else if (value < 1)
        {
            PointCount.Text = "1";
        }
        else if (value > 100)
        {
            PointCount.Text = "100";
        }
    }

    private void RenderSpeed_Unfocused(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(RenderSpeed.Text) || !int.TryParse(RenderSpeed.Text, out _))
        {
            RenderSpeed.Text = "1";
        }
    }

    private void RenderSpeed_Changed(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(RenderSpeed.Text) || !int.TryParse(RenderSpeed.Text, out int value))
        {
            return;
        }
        else if (value < 1)
        {
            RenderSpeed.Text = "1";
        }
        else if (value > 50)
        {
            RenderSpeed.Text = "50";
        }
    }

    private void LinearSpeed_Unfocused(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(LinearSpeed.Text) || !int.TryParse(LinearSpeed.Text, out _))
        {
            RenderSpeed.Text = "1";
        }
    }

    private void LinearSpeed_Changed(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(LinearSpeed.Text) || !int.TryParse(LinearSpeed.Text, out int value))
        {
            return;
        }
        else if (value < 1)
        {
            LinearSpeed.Text = "1";
        }
        else if (value > 500)
        {
            LinearSpeed.Text = "500";
        }
    }

    private void LineThickness_Unfocused(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(LineThickness.Text) || !int.TryParse(LineThickness.Text, out _))
        {
            RenderSpeed.Text = "1";
        }
    }

    private void LineThickness_Changed(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(LineThickness.Text) || !int.TryParse(LineThickness.Text, out int value))
        {
            return;
        }
        else if (value < 1)
        {
            LineThickness.Text = "1";
        }
        else if (value > 20)
        {
            LineThickness.Text = "20";
        }
    }

    private async void PrivacyPolicy_Clicked(object sender, EventArgs e)
    {
        Uri uri = new("https://ericsgear.com/RetroLines/PrivacyPolicy.html");
        await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
    }
}
