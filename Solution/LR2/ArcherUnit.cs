namespace LR2;

public class ArcherUnit(string name, int health, int attackNumber, int attackRange, int defence, int movementRange, int cost, int y, int x, int id): IUnit
{
    public string Name { get; } = name;
    public int Id { get; } = id;
    public int Health { get; set; } = health;
    public int AttackNumber { get; } = attackNumber;
    public int AttackRange { get; } = attackRange;
    public int Defence { get; set; } = defence;
    public int MovementRange { get; } = movementRange;
    public int YСoordinate { get; set; } = y;
    public int XСoordinate { get; set; } = x;
    public int Cost { get; } = cost;
    public void Move(IUnit unit, string direction, City city)
    {
        var y = YСoordinate;
        var x = XСoordinate;
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
        if (city.CityObjects[y][x].Obj != "*" & city.CityObjects[y][x].Obj != $"{Id}")
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
        city.PlaceObject(XСoordinate, YСoordinate, "*");
        XСoordinate = x;
        YСoordinate = y;
        city.PlaceObject(XСoordinate, YСoordinate, $"{Id}");
    }
    public void DoAttack(IUnit unit)
    {
        if (CheckAvailability(unit))
        {
            if (unit.Defence > 0)
            {
                if (unit.Defence < AttackNumber)
                {
                    unit.Health -= AttackNumber - unit.Defence;
                    unit.Defence = 0;
                }
                else
                {
                    unit.Defence -= AttackNumber;
                }
            }
            else
            {
                unit.Health -= AttackNumber;
            }
        }
        else
        {
            Console.WriteLine("You can't attack this unit");
        }
    }
    
    private bool CheckAvailability(IUnit victim)
    {
        return XСoordinate - victim.XСoordinate <= AttackRange || YСoordinate - victim.YСoordinate <= AttackRange;
    }
    
    public bool IsAlive()
    {
        return Health > 0;
    }
}