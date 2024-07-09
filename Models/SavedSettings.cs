namespace RetroLines.Models
{
    public class SavedSettings
    {
        public int PointCount { get; set; } = 4; // verticies in the shape, 1-100, 1 = ellipse, 2 = line, >3 = polyline
        public int RenderSpeed { get; set; } = 10; // X times per second, 0-50
        public int LinearSpeed { get; set; } = 10; // pixels/second, (1 - 500)
        public bool Fill { get; set; } = false;
        public int FillOpacity { get; set; } = 0; // percentage
        public int Trails { get; set; } = 100; // how many before they disappear, 0-1000
        public int LineThickness { get; set; } = 1; // 1 - 20
        public int BackgroundColor { get; set; } = 0;
    }
}
