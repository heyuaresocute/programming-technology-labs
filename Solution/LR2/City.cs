using LR2.Animals;
using LR2.Factories;
using LR2.Interfaces;
using LR2.Units;

namespace LR2;

public class City(int cols, int rows, int swampsNumber, int hillsNumber, int treesNumber, int catChance)
{
    private int SwampsNumber { get; } = swampsNumber;
    private int HillsNumber { get; } = hillsNumber;
    private int TreesNumber { get; } = treesNumber;
    public int Cols { get; } = cols;
    public int CatChanсe { get; } = catChance;
    public int Rows { get; } = rows;
    public const string TreeType = "T";
    public const string SwampType = "S";
    public const string HillType = "H";
    public List<IAnimal> Animals { get; } = [];
    public List<Player> Players { get; } = [];
    public Square[][] CityObjects { get; set; } = [];
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

            var fine = GetFine(unit, CityObjects[y][x].Obj);
            if (count + fine <= unit.MovementRange)
            {
                count += fine;
                if (CityObjects[y][x].Obj == TreeType & Animals.Count == 0)
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

    private double GetFine(IUnit unit, string type)
    {
        if (type == "*")
        {
            return 1;
        }

        switch (unit)
        {
            case InfantryUnit when type == SwampType:
                return 1.5;
            case InfantryUnit when type == HillType:
                return 2;
            case InfantryUnit when type == TreeType:
                return 1.2;
            case ArcherUnit when type == SwampType:
                return 1.8;
            case ArcherUnit when type == HillType:
                return 2.2;
            case ArcherUnit when type == TreeType:
                return 1;
            case HorseUnit when type == SwampType:
                return 2.2;
            case HorseUnit when type == HillType:
                return 1.2;
            case HorseUnit when type == TreeType:
                return 1.5;
            case Cat when type == SwampType:
                return 1.5;
            case Cat when type == HillType:
                return 1.5;
            case Cat when type == TreeType:
                return 1;
            default:
                return 1;
        }
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
                CityObjects[i][j] = new Square("*");
            }
        }

        PlaceObjects(SwampsNumber, SwampType);
        PlaceObjects(HillsNumber, HillType);
        PlaceObjects(TreesNumber, TreeType);
    }

    private void PlaceObjects(int number, string type)
    {
        for (var i = 0; i < number; i++)
        {
            var rnd = new Random();
            var rnd1 = rnd.Next(1, Cols);
            var rnd2 = rnd.Next(1, Rows);
            if (CityObjects[rnd1][rnd2].Obj != "*")
            {
                rnd1 = rnd.Next(1, Cols);
                rnd2 = rnd.Next(1, Rows);
            }

            PlaceObject(rnd1, rnd2, type);
        }
    }

    public void PlaceObject(int x, int y, string type)
    {
        CityObjects[y][x].Obj = type;
    }

    private static Square[][] GenerateMatrix(int cols, int rows)
    {
        Square[][] result = new Square[rows][];
        for (var i = 0; i < rows; ++i)
            result[i] = new Square[cols];
        return result;
    }
}