using LR2.Interfaces;

namespace LR2.Animals;

public class Cat(
    string name,
    int health,
    int attackNumber,
    int attackRange,
    int movementRange,
    int x,
    int y,
    int bleedingTime,
    int bleedingDamage) : IAnimal
{
    public string Name { get; private set; } = name;
    public string ShortName { get; set; } = Convert.ToString(name[0]);
    public int Health { get; set; } = health;
    public int AttackDamage { get; set; } = attackNumber;
    public int AttackRange { get; set; } = attackRange;
    public int Defence { get; set; } = 0;
    public int Cost { get; set; } = 3;
    public int Bleed { get; set; } = 0;
    private int BleedingTime { get; } = bleedingTime;
    private int BleedingDamage { get; } = bleedingDamage;
    public int MovementRange { get; set; } = movementRange;
    public int Y { get; set; } = y;
    public int X { get; set; } = x;
    public Player? Owner { get; set; }

    public void TakeAStep(City city)
    {
        MakeUnitsBleed(city);
        if (Owner != null)
        {
            var opponent = Owner == city.Players[0] ? city.Players[1] : city.Players[0];
            var victim = GetVictim(opponent);
            if (victim != null)
            {
                Console.WriteLine($"{Owner.Type} cat attacks {opponent.Type} {victim.Name}. Meow");
                DoAttack(victim);
                if (victim.IsAlive()) return;
                opponent.RemoveUnit(victim, city);
                Console.WriteLine($"{Owner.Type} cat killed {opponent.Type} {victim.Name}");
            }
            else
            {
                Move("", city);
            }
        }
        else
        {
            var a = FindFeeder(city);
            if (a)
            {
                Console.WriteLine($"{Name} smelled the food. {Name} decided to wait");
            }
            else
            {
                Move( "", city);
            }
        }
    }

    private void MakeUnitsBleed(City city)
    {
        foreach (var player in city.Players)
        {
            foreach (var unit in player.Units.ToList())
            {
                if (unit.Bleed > 0 & unit.Bleed < BleedingTime)
                {
                    unit.Health -= BleedingDamage;
                    unit.Bleed += 1;
                }
                else if (unit.Bleed == BleedingTime)
                {
                    unit.Health -= BleedingDamage;
                    unit.Bleed = 0;
                }
                if (!unit.IsAlive())
                {
                    player.RemoveUnit(unit, city);
                    Console.WriteLine($"{player.Type}'s {unit.Name} died because of bleeding");
                }
            }
        }
    }

    private IUnit? GetVictim(Player opponent)
    {
        foreach (var victim in opponent.Units)
        {
            if (CheckAvailability(victim))
            {
                return victim;
            }
        }

        return null;
    }
    
    private bool FindFeeder(City city)
    {
        var player = city.Players[0];
        var opponent = city.Players[1];
        foreach (var feeder in player.Units)
        {
            if (CheckAvailabilityOfFeeder(feeder))
            {
                return true;
            }
        }
        foreach (var feeder in opponent.Units)
        {
            if (CheckAvailabilityOfFeeder(feeder))
            {
                return true;
            }
        }
        return false;
    }

    public void Eat(Player player)
    {
        player.Cash -= 1;
        Cost -= 1;
        Console.WriteLine($"{player.Type} cash now is {player.Cash}.");
        switch (Cost)
        {
            case > 0:
                Console.WriteLine($"{player.Type} need to feed {Name} {Cost} times");
                break;
            case 0:
                Owner = player;
                Console.WriteLine($"Now {player.Type} are the owner of {Name}");
                if (player.Type == "You")
                {
                    Console.WriteLine("Please, choose the name of the cat: ");
                    SetName();
                }
                UpdateOwnersUnits(player);
                Cost = 6;
                break;
        }
    }

    private void UpdateOwnersUnits(Player player)
    {
        foreach (var unit in player.Units)
        {
            unit.Health += 1;
            unit.Defence += 1;
            unit.AttackDamage += 1;
            unit.AttackRange += 1;
            unit.MovementRange += 1;
        }
    }

    private void SetName()
    {
        var name = Convert.ToString(Console.ReadLine());
        if (name != null)
        {
            Name = name;
            ShortName = Convert.ToString(name[0]);
            if (ShortName == City.TreeType || ShortName == City.SwampType || ShortName == City.HillType)
            {
                ShortName = "C";
            }
        }
        else
        {
             SetName();       
        }
    }

    public void Move(string direction, City city)
    {
        var rnd = new Random();
        var x = X;
        var y = Y;
        var directionAsInt = rnd.Next(0, 3);
        var directionAsString = directionAsInt switch
        {
            0 => "u",
            1 => "d",
            2 => "l",
            3 => "r",
            _ => ""
        };
        var wayRange = city.GetWayRange(directionAsString, this);
        switch (directionAsString)
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
            switch (directionAsString)
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

        if (X == x & Y == y)
        {
            Move( "", city);
        }
        else
        {
            city.PlaceObject(X, Y, "*");
            X = x;
            Y = y;
            city.PlaceObject(X, Y, "C");
            Console.WriteLine(Owner != null ? $"{Owner.Type} {Name} has moved" : $"{Name} has moved");
        }
    }

    public void DoAttack(IUnit victim)
    {
        victim.Bleed = 1;
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

    public bool CheckAvailability(IUnit victim)
    {
        return Math.Abs(X - victim.X) <= AttackRange & Y == victim.Y ||
                   Math.Abs(Y - victim.Y) <= AttackRange & X == victim.X;
    }

    public bool CheckAvailabilityOfFeeder(IUnit feeder)
    {
        return Math.Abs(X - feeder.X) <= 1 & Y == feeder.Y ||
               Math.Abs(Y - feeder.Y) <= 1 & X == feeder.X;
    }

    public bool IsAlive()
    {
        return Health > 0;
    }
}