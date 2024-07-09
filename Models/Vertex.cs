namespace RetroLines.Models;

internal class Vertex
{
    public double X { get; set; }
    public double Y { get; private set; }
    public double Rise { get; private set; }
    public double Run { get; private set; } = 1;
    public double B { get; private set; }
    public bool SlopePositive { get; set; } = true;
    public bool DirectionPositive { get; set; } = true;
    public double M { get; private set; }
    public Point Point { get; private set; }

    public double EllipseHeight
    {
        get => height;
        set
        {
            if (value < 0)
            {
                height = 0;
            }
            else
            {
                height = value;
            }
        }
    }

    public double EllipseWidth
    {
        get => width;
        set
        {
            if (value < 0)
            {
                width = 0;
            }
            else
            {
                width = value;
            }
        }
    }

    public double EllipseRotation
    {
        get => rotation;
        set
        {
            if (value < 0)
            {
                rotation = 0;
            }
            else if (value >= 360)
            {
                rotation = value - 360;
            }
            else
            {
                rotation = value;
            }
        }
    }

    private double rotation = 0;
    private double height = 100;
    private double width = 100;

    public Vertex(double x, double y, int linearSpeed)
    {
        Random rnd = new();
        X = x;

        DirectionPositive = rnd.Next(256) % 2 == 0;
        SlopePositive = rnd.Next(256) % 2 == 0;

        NewLine(y, linearSpeed);
    }

    public void NewLine(double y, int linearSpeed)
    {
        Random rnd = new();

        Y = y;
        Run = rnd.NextDouble() * 10;
        Rise = Math.Sqrt((linearSpeed * linearSpeed) - (Run * Run));
        M = Rise / Run * (SlopePositive ? 1 : -1);
        B = Y - (M * X);
    }

    public override string ToString()
    {
        return $"X: {X}, Y: {Y}, M: {M}, B: {B}, Rise: {Rise}, Run: {Run}";
    }

    public void Next()
    {
        X += Run * (DirectionPositive ? 1 : -1);
        Y = (M * X) + B;
        Point = new Point(X, Y);
    }
}
