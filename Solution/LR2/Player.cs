using LR2.Factories;
using LR2.Interfaces;

namespace LR2;

public class Player(int cash)
{
    public List<IUnit> Units { get; } = [];
    public int Cash { get; set; } = cash;
    public void SelectUnits(UnitsFactory factory, City city)
    {
        var count = 0;
        while (count < 3)
        {
            var unitId = Convert.ToInt32(Console.ReadLine());
            var unit = Selecter(factory, unitId, city.Cols - 1, city.Rows - 1 - count, count + 1);
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
    
    public void PlaceUnits(City city)
    {
        for (int i = 0; i < 3; i++)
        {
            city.PlaceObject(Units[i].X, Units[i].Y, $"{Units[i].Id}");
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
        city.PlaceObject(unit.X, unit.Y, "*");
    }

    public IUnit[]? GetVictim(Player opponent)
    {
        foreach (var unit in Units)
        {
            foreach (var victim in opponent.Units)
            {
                if (unit.CheckAvailability(victim))
                {
                    return [unit, victim];
                }
            }
        }

        return null;
    }
}