using Microsoft.Maui.Controls.Shapes;

namespace RetroLines.Models;

internal class DrawnShape(double height, double width, double density)
{
    public double ScreenWidth { get; private set; } = width / density;
    public double ScreenHeight { get; private set; } = height / density;
    public List<Vertex> Points { get; } = [];
    public int Elements { get; set; } = 0;

    public Shape CreateCircle()
    {
        Random rnd = new();

        double height = Points[0].EllipseHeight + (Points[0].Rise * (rnd.Next() % 2 == 0 ? -1 : 1));
        double width = Points[0].EllipseWidth + (Points[0].Run * (rnd.Next() % 2 == 0 ? -1 : 1));
        double rotation = Points[0].EllipseRotation + 10;

        ValidateNumber(ref height, ScreenHeight);
        ValidateNumber(ref width, ScreenWidth);

        Points[0].EllipseHeight = height;
        Points[0].EllipseWidth = width;
        Points[0].EllipseRotation = rotation;

        Ellipse path = new()
        {
            Rotation = rotation,
            HeightRequest = height,
            WidthRequest = width
        };

        Points[0].Next();
        path.Margin = new Thickness(Points[0].X, Points[0].Y);

        HandleBounce(Points[0]);
        Elements++;
        SetAttributes(path);

        return path;
    }

    public Shape CreateLine()
    {
        Polyline path = new();

        for (int i = 0; i < AppPreferences.Settings.PointCount; i++)
        {
            Points[i].Next();

            path.Points.Add(Points[i].Point);
            HandleBounce(Points[i]);
        }

        Elements++;
        SetAttributes(path);

        return path;
    }

    public Shape CreatePolygon()
    {
        Polygon path = new();

        for (int i = 0; i < AppPreferences.Settings.PointCount; i++)
        {
            Points[i].Next();

            path.Points.Add(Points[i].Point);
            HandleBounce(Points[i]);
        }

        Elements++;
        SetAttributes(path);

        return path;
    }

    private static void ValidateNumber(ref double value, double size)
    {
        if (value < AppPreferences.Settings.LinearSpeed)
        {
            value = AppPreferences.Settings.LinearSpeed;
        }
        else if (value > size)
        {
            value -= 2 * size;
        }
    }

    private void HandleBounce(Vertex p)
    {
        HandleBounceX(p);
        HandleBounceY(p);
    }

    private void HandleBounceX(Vertex p)
    {
        if (p.X < 0)
        {
            p.X = -p.X;
            p.DirectionPositive = true;

            p.NewLine(p.Y, AppPreferences.Settings.LinearSpeed);
        }
        else if (p.X >= ScreenWidth)
        {
            p.X = (2 * ScreenWidth) - p.X;
            p.DirectionPositive = false;

            p.NewLine(p.Y, AppPreferences.Settings.LinearSpeed);
        }
    }

    private void HandleBounceY(Vertex p)
    {
        if (p.Y < 0)
        {
            p.SlopePositive = !p.SlopePositive;

            p.NewLine(-p.Y, AppPreferences.Settings.LinearSpeed);
        }
        else if (p.Y >= ScreenHeight)
        {
            p.SlopePositive = !p.SlopePositive;

            p.NewLine((2 * ScreenHeight) - p.Y, AppPreferences.Settings.LinearSpeed);
        }
    }

    private static void SetAttributes(Shape path)
    {
        Random rnd = new();
        Color lineColor = Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));

        if (AppPreferences.Settings.Fill)
        {
            path.Fill = lineColor;
            path.Opacity = AppPreferences.Settings.FillOpacity / 100.0;
        }


        path.StrokeThickness = AppPreferences.Settings.LineThickness;
        path.Stroke = new SolidColorBrush(lineColor);
    }
}
