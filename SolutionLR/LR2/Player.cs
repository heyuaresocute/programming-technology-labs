namespace LR2;

public class Player(int cash)
{
    public List<IUnit> Units { get; set; } = [];
    public int Cash { get; set; } = cash;

    public void Move(IUnit unit, string direction, City city)
    {
        var y = unit.YСoordinate;
        var x = unit.XСoordinate;
        var wayRange = city.GetWayRange(direction, unit);
        switch (direction)
        {
            case "u":
                y -= wayRange;
                break;
            case "d":
                y += wayRange;
                break;
            case "r":
                x += wayRange;
                break;
            case "l":
                x -= wayRange;
                break;
        }
        if (city.CityObjects[y][x].Obj != "*")
        {
            switch (direction)
            {
                case "u":
                    y += 1;
                    break;
                case "d":
                    y -= 1;
                    break;
                case "r":
                    x -= 1;
                    break;
                case "l":
                    x += 1;
                    break;
            }
        }
        city.PlaceObject(unit.XСoordinate, unit.YСoordinate, "*");
        unit.XСoordinate = x;
        unit.YСoordinate = y;
        city.PlaceObject(unit.XСoordinate, unit.YСoordinate, $"{unit.Id}");
    }
    public void SelectUnits(UnitsFactory factory)
    {
        var count = 0;
        while (count < 3)
        {
            var unitId = Convert.ToInt32(Console.ReadLine());
            var unit = Selecter(factory, unitId, 9, 9 - count, count + 1);
            Cash -= unit.Cost;
            if (Cash >= 0)
            {
                Units.Add(unit);
                count += 1;
                Console.WriteLine($"{unit.Name} selected, your cash now is {Cash}");
            }
            else
            {
                Cash += unit.Cost;
                Console.WriteLine($"You can't select {unit.Name}, because your cash now is {Cash}");
            }
        }
    }
    
    public void OpponentSelectUnits(UnitsFactory factory)
    {
        var count = 0;
        Random rnd = new Random();
        int unitId;
        while (count < 3)
        {
            unitId = rnd.Next(1, 9);
            var unit = Selecter(factory, unitId, 0, count, count + 7);
            Cash -= unit.Cost;
            if (Cash >= 0)
            {
                Units.Add(unit);
                count += 1;
            }
            else
            {
                Cash += unit.Cost;
            }
        }
    }

    public void OutputUnits()
    {
        for (int i = 0; i < Units.Count; i++)
        {
            Console.WriteLine($"{Units[i].Id}. {Units[i].Name}, health - {Units[i].Health}, attack - {Units[i].AttackNumber}, attack range - {Units[i].AttackRange}, defence - {Units[i].Defence}, move - {Units[i].MovementRange}");
        }
        
    }
    
    public void PlaceUnits(City city) // 
    {
        for (int i = 0; i < 3; i++)
        {
            city.PlaceObject(Units[i].XСoordinate, Units[i].YСoordinate, $"{Units[i].Id}");
        }
    }

    private IUnit Selecter(UnitsFactory factory, int unitId, int x, int y, int id)
    {
        switch (unitId)
        {
            default:
                return factory.CreateAxeman(x, y, id);
            case 1: 
                return factory.CreateSwordsman(x, y, id);
            case 2:
                return factory.CreateSpearman(x, y, id);
            case 3:
                return factory.CreateAxeman(x, y, id);
            case 4:
                return factory.CreateLongBow(x, y, id);
            case 5:
                return factory.CreateShortBow(x, y, id);
            case 6:
                return factory.CreateCrossBow(x, y, id);
            case 7:
                return factory.CreateKnight(x, y, id);
            case 8:
                return factory.CreateСuirassier(x, y, id);
            case 9:
                return factory.CreateHorseBow(x, y, id);
        }
    }
}