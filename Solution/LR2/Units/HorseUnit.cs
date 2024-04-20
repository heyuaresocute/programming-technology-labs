using LR2.Interfaces;

namespace LR2.Units;

public class HorseUnit(string name, int health, int attackNumber, int attackRange, int defence, int movementRange, int cost, int y, int x, string id): IUnit
{
    public string Name { get; } = name;
    public string ShortName { get; } = id;
    public int Health { get; set; } = health;
    public int AttackDamage { get; set; } = attackNumber;
    public int AttackRange { get; set; } = attackRange;
    public int Defence { get; set; } = defence;
    public int MovementRange { get; set; } = movementRange;
    public int Y { get; set; } = y;
    public int X { get; set; } = x;
    public int Cost { get; set; } = cost;
    public int Bleed { get; set; } = 0;

    public void Move(string direction, City city)
    {
        var y = Y;
        var x = X;
        var wayRange = city.GetWayRange(direction, this);
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
        while (city.CityObjects[y][x].Obj != "*" & city.CityObjects[y][x].Obj != $"{ShortName}")
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
        city.PlaceObject(X, Y, new Square("*", 1, 1, 1, 1));
        X = x;
        Y = y;
        city.PlaceObject(X, Y, new Square(ShortName, 1, 1, 1, 1));
    }
    public void DoAttack(IUnit victim)
    {
        if (CheckAvailability(victim))
        {
            if (victim.Defence > 0)
            {
                if (victim.Defence < AttackDamage)
                {
                    victim.Health -= AttackDamage - victim.Defence;
                    victim.Defence = 0;
                }
                else
                {
                    victim.Defence -= AttackDamage;
                }
            }
            else
            {
                victim.Health -= AttackDamage;
            }
        }
        else
        {
            Console.WriteLine("You can't attack this unit");
        }
    }

    public bool CheckAvailability(IUnit victim)
    {
        return Math.Abs(X - victim.X) <= AttackRange & Y == victim.Y ||
               Math.Abs(Y - victim.Y) <= AttackRange & X == victim.X;
    }
    
    public bool IsAlive()
    {
        return Health > 0;
    }
}