using LR2.Factories;
using LR2.Interfaces;

namespace LR2;

public class Player(int cash, int wood, int stone, string type)
{
    public string Type { get; } = type;
    public List<IUnit> Units { get; set; } = [];
    public int Cash { get; set; } = cash;

    public int Wood { get; set; } = wood;

    public int Stone { get; set; } = stone;
    
    public void SelectUnits(UnitsFactory factory, City city)
    {
        var count = 0;
        int unitId = 1;
        IUnit unit;
        if (Type == "You")
        {
            Console.WriteLine("Select your units (select 3):");
        }
        while (count < 3)
        {
            switch (Type)
            {
                default:
                {
                    unit = Selecter(factory, unitId, 0, count, $"{count + 7}");
                    break;
                }
                case "Opponent":
                {
                    Random rnd = new Random();
                    unitId = rnd.Next(1, 9);
                    unit = Selecter(factory, unitId, 0, count, $"{count + 7}");
                    break;
                }
                case "You":
                    unitId = Convert.ToInt32(Console.ReadLine());
                    unit = Selecter(factory, unitId, city.Cols - 1, city.Rows - 1 - count, $"{count + 1}");
                    break;
            }
            Cash -= unit.Cost;
            if (Cash >= 0)
            {
                Units.Add(unit);
                count += 1;
                if (Type == "You")
                {
                    Console.WriteLine($"{unit.Name} selected, your cash now is {Cash}");
                }
            }
            else
            {
                Cash += unit.Cost;
                city.PlaceObject(unit.X, unit.Y, new Square("*", 1, 1, 1, 1));
                if (Type == "You")
                {
                    Console.WriteLine($"You can't select {unit.Name}, because your cash now is {Cash}");
                }
            }
            
        }
    }

    public void OutputUnits()
    {
        foreach (var unit in Units)
        {
            Console.WriteLine($"{unit.ShortName}. {unit.Name}, health - {unit.Health}, attack - {unit.AttackDamage}, attack range - {unit.AttackRange}, defence - {unit.Defence}, movement range - {unit.MovementRange}, bleeds - {unit.Bleed}");
        }
    }

    private static IUnit Selecter(UnitsFactory factory, int unitId, int x, int y, string id)
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
                return factory.CreateBrassiere(x, y, id);
            case 9:
                return factory.CreateHorseBow(x, y, id);
            case 10:
                return factory.CreatePaladine(x, y, id);
        }
    }

    public void RemoveUnit(IUnit unit, City city)
    {
        Units.Remove(unit);
        city.PlaceObject(unit.X, unit.Y, new Square("*", 1, 1, 1, 1));
    }

    public IUnit[]? GetVictim(Player opponent)
    {
        return (from unit in Units from victim in opponent.Units.Where(unit.CheckAvailability) select (IUnit[]?) [unit, victim]).FirstOrDefault();
    }

    public IUnit[]? GetAnimal(City city)
    {
        return (from unit in Units from animal in city.Animals.Where(animal => animal.CheckAvailabilityOfFeeder(unit)) select (IUnit[]?) [unit, animal]).FirstOrDefault();
    }
}