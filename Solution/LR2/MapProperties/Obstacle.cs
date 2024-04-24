using LR2.Interfaces;

namespace LR2.MapProperties;

public class Obstacle(string designation, int x, int y): IData
{
    public string Designation { get; set; } = designation;
    public int X { get; set; } = x;
    public int Y { get; set; } = y;

    public void Output()
    {
        Console.WriteLine($"Designation: {Designation}, X: {X}, Y: {Y}");
    }
}