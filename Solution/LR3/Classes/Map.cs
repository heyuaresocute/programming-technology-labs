using LR3.Interfaces;

namespace LR3.Classes;

public class Map(string designation, int cols, int rows): IData
{
    public string Designation { get; set; } = designation;
    
    public int Cols { get; } = cols;
    public int Rows { get; } = rows;
    public List<Obstacle> Obstacles { get; } = [];
    public void Output()
    {
        Console.WriteLine($"Designation: {Designation}, Cols:{Cols}, Rows: {Rows}");
        Console.WriteLine("Obstacles: ");
        foreach (var obstacle in Obstacles)
        {
            obstacle.Output();
        }
        Console.WriteLine("---------------------------");
    }

    public void AddObstacle(string designation, int x, int y)
    {
        if (x < Cols & y < Rows)
        {
            Obstacles.Add(new Obstacle(designation, x, y));
        }
    }
}