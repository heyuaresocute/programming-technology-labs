using LR2.Interfaces;
using LR2.Units;

namespace LR2;

public class City(int cols, int rows, int swampsNumber, int hillsNumber, int treesNumber)
{
    public int SwampsNumber { get; } = swampsNumber;
    public int HillsNumber { get; } = hillsNumber;
    public int TreesNumber { get; } = treesNumber;
    public int Cols { get; } = cols;
    public int Rows { get; } = rows;
    public Square[][] CityObjects { get; set; } = [];

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
            if (count+fine <= unit.MovementRange)
            {
                count += fine;
            }
        }
        wayrange = Math.Floor(count);
        return Convert.ToInt32(wayrange);
    }

    private static double GetFine(IUnit unit, string type)
    {
        if (type == "*")
        {
            return 1;
        }

        if (unit is InfantryUnit)
        {
            switch (type)
            {
                case "S":
                    return 1.5;
                case "H":
                    return 2;
                case "T":
                    return 1.2;
                    
            }
        }
        if (unit is ArcherUnit)
        {
            switch (type)
            {
                case "S":
                    return 1.8;
                case "H":
                    return 2.2;
                case "T":
                    return 1;
                    
            }
        }
        if (unit is HorseUnit)
        {
            switch (type)
            {
                case "S":
                    return 2.2;
                case "H":
                    return 1.2;
                case "T":
                    return 1.5;
                    
            }
        }

        return 1;
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
        CityObjects = GenerateMatrix(Cols, Rows);
        FillTheCity();
    }

    public void OutputCity()
    {
        //Console.Clear();
        Console.Write("|    | ");
        for (int i = 1; i < Cols+1; i++)
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
                Console.Write($"| {i+1}  |");
            }
            else
            {
                Console.Write($"| {i+1} |");
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
        for (int i = 0; i < Cols; i++)
        {
            for (int j = 0; j < Rows; j++)
            {
                CityObjects[i][j] = new Square("*");
            }
        }
        PlaceObjects(SwampsNumber, "S");
        PlaceObjects(HillsNumber, "H");
        PlaceObjects(TreesNumber, "T");
    }

    private void PlaceObjects(int number, string type)
    {
        for (var i = 0; i < number; i++)
        {
            Random rnd = new Random();
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