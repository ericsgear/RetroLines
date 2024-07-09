using System.Text.Json;

namespace RetroLines.Models;

public class AppPreferences
{
    public static SavedSettings Settings { get; set; } = new();
    public AppPreferences()
    {
    }

    public static void Load()
    {
        string foundSettings = Preferences.Default.Get("SavedSettings", string.Empty);

        if (string.IsNullOrWhiteSpace(foundSettings))
        {
            Settings = new();
        }
        else
        {
            try
            {
                SavedSettings? json = JsonSerializer.Deserialize<SavedSettings>(foundSettings);

                if (json != null)
                {
                    Settings = json;
                }
            }
            catch
            {
                Settings = new();
            }
        }
    }

    public static void Save()
    {
        Preferences.Set("SavedSettings", JsonSerializer.Serialize(Settings));
    }
}
