namespace LR2;

public class HorseUnit(string name, int health, int attackNumber, int attackRange, int defence, int movementRange, int cost, int y, int x, int id): IUnit
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
    public void DoAttack(IUnit victim)
    {
        if (CheckAvailability(victim))
        {
            if (victim.Defence > 0)
            {
                if (victim.Defence < AttackNumber)
                {
                    victim.Health -= AttackNumber - victim.Defence;
                    victim.Defence = 0;
                }
                else
                {
                    victim.Defence -= AttackNumber;
                }
            }
            else
            {
                victim.Health -= AttackNumber;
            }
        }
        else
        {
            Console.WriteLine("You can't attack this unit");
        }
    }
    
    public bool CheckAvailability(IUnit victim)
    {
        return Math.Abs(XСoordinate - victim.XСoordinate) <= AttackRange || Math.Abs( YСoordinate - victim.YСoordinate) <= AttackRange;
    }
    
    public bool IsAlive()
    {
        return Health > 0;
    }
}