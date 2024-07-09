namespace RetroLines.Models;

public class ColorOptions(string name, Color color)
{
    public string Name { get; set; } = name;
    public Color Color { get; set; } = color;
}
