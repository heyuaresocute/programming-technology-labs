using LR2.Buildings;
using LR2.Factories;
using LR2.Interfaces;
using LR2.MapProperties;
using Newtonsoft.Json;

namespace LR2;

public class City(int catChance, Map map)
{
    public int Cols { get; } = map.Cols;
    public int CatChanсe { get; } = catChance;
    public int Rows { get; } = map.Rows;
    public double TavernBonus { get; set; } = 0;

    private const string PathToJsons =
        "/Users/heyuaresocute/projects/programming-technology-labs/Solution/LR3/jsons/";
    public List<IAnimal> Animals { get; } = [];
    public List<Player> Players { get; } = [];
    public Square[][] CityObjects { get; set; } = [];
    
    public List<IBuilding> CityBuildings { get; set; } = [];
    public Map Map { get; } = map;
    private UnitsFactory? _factory;

    public int GetWayRange(string direction, IUnit unit)
    {
        var x = unit.X;
        var y = unit.Y;
        double count = 0;
        double wayrange;
        var type = "";
        switch (direction)
        {
            case "u":
                type = "y-";
                break;
            case "d":
                type = "y+";
                break;
            case "r":
                type = "x+";
                break;
            case "l":
                type = "x-";
                break;
        }

        for (var i = 0; i < unit.MovementRange; i++)
        {
            var coordinates = FindCoordinates(x, y, type);
            x = coordinates[0];
            y = coordinates[1];
            if (x == -1 || y == -1 || x == Cols || y == Rows)
            {
                wayrange = Math.Floor(count);
                return Convert.ToInt32(wayrange);
            }

            var fine = CityObjects[y][x].GetFine(unit) - TavernBonus;
            if (fine < 1)
            {
                fine = 1;
            }
            if (count + fine <= unit.MovementRange)
            {
                count += fine;
                if (CityObjects[y][x].Obj == "T" & Animals.Count == 0)
                {
                    int[]? catCoordinates = FindFreePlace(y, x);
                    if (catCoordinates != null)
                    {
                        var rnd = new Random();
                        var rnd1 = rnd.Next(100);
                        if (rnd1 <= CatChanсe)
                        {
                            _factory!.CreateCat(catCoordinates[1], catCoordinates[0]);
                        }
                    }
                }
            }
        }

        wayrange = Math.Floor(count);
        return Convert.ToInt32(wayrange);
    }

    private int[]? FindFreePlace(int y, int x)
    {
        if (y != Rows - 1)
        {
            if (CityObjects[y + 1][x].Obj == "*")
            {
                return [y + 1, x];
            }
            if (x != Cols - 1)
            {
                if (CityObjects[y + 1][x + 1].Obj == "*")
                {
                    return [y + 1, x + 1];
                }
            }
        }
        if (y != 0)
        {
            if (CityObjects[y - 1][x].Obj == "*")
            {
                return [y - 1, x];
            }
            if (x != 0)
            {
                if (CityObjects[y - 1][x - 1].Obj == "*")
                {
                    return [y - 1, x - 1];
                }
            }
        }

        if (x != Cols - 1)
        {
            if (CityObjects[y][x + 1].Obj == "*")
            {
                return [y, x + 1];
            }
        }

        if (x != 0)
        {
            if (CityObjects[y][x - 1].Obj == "*")
            {
                return [y, x - 1];
            }
        }

        return null;
    }

    private static int[] FindCoordinates(int x, int y, string type)
    {
        switch (type)
        {
            case "x-":
                x -= 1;
                break;
            case "x+":
                x += 1;
                break;
            case "y-":
                y -= 1;
                break;
            case "y+":
                y += 1;
                break;
        }

        return [x, y];
    }


    public void GenerateCity()
    {
        _factory = new UnitsFactory(this);
        CityObjects = GenerateMatrix(Cols, Rows);
        FillTheCity();
    }

    public void OutputCity()
    {
        Console.Write("|    | ");
        for (int i = 1; i < Cols + 1; i++)
        {
            if (i < 10)
            {
                Console.Write($"{i}  | ");
            }
            else
            {
                Console.Write($"{i} | ");
            }
        }
        Console.Write($"Wood: {Players[0].Wood}     Stone: {Players[0].Stone}  Money: {Players[0].Cash}   TavernBonus: {TavernBonus}");

        Console.WriteLine();
        for (int i = 0; i < Rows; i++)
        {
            if (i < 9)
            {
                Console.Write($"| {i + 1}  |");
            }
            else
            {
                Console.Write($"| {i + 1} |");
            }

            for (int j = 0; j < Cols; j++)
            {
                Console.Write($" {CityObjects[i][j].Obj}  |");
            }

            Console.WriteLine();
        }
    }

    private void FillTheCity()
    {
        for (var i = 0; i < Cols; i++)
        {
            for (var j = 0; j < Rows; j++)
            {
                CityObjects[i][j] = new Square("*", 1, 1, 1, 1);
            }
        }
        List<ObstacleType> obstacleTypes = GetObstacles();
        foreach (var obstacle in Map.Obstacles)
        {
            var rnd = new Random();
            if (CityObjects[obstacle.X][obstacle.Y].Obj != "*")
            {
                obstacle.X = rnd.Next(1, Cols);
                obstacle.Y = rnd.Next(1, Rows);
            }

            ObstacleType obstacleProperties = new ObstacleType("*", 1, 1, 1, 1, 1);

            foreach (var obstacleType in obstacleTypes)
            {
                if (obstacleType.Designation == obstacle.Designation)
                {
                    obstacleProperties = obstacleType;
                }
            }
            PlaceObject(obstacle.X, obstacle.Y, new Square(obstacle.Designation, obstacleProperties.InfantryFine, obstacleProperties.HorseFine, obstacleProperties.ArcherFine, obstacleProperties.CatFine));
        }
        
    }

    public void PlaceObject(int x, int y, Square object1)
    {
        CityObjects[y][x] = object1;
    }

    private static Square[][] GenerateMatrix(int cols, int rows)
    {
        Square[][] result = new Square[rows][];
        for (var i = 0; i < rows; ++i)
            result[i] = new Square[cols];
        return result;
    }
    
    public static List<ObstacleType> GetObstacles()
    {
        string json = File.ReadAllText(GetPathToFile("obstacles.json")); 
        var obstacles = JsonConvert.DeserializeObject<List<ObstacleType>>(json)!;
        return obstacles!;
    }
    
    public static string GetPathToFile(string filename)
    {
        return PathToJsons + filename;
    }
}