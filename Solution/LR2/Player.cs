using LR2.Factories;
using LR2.Interfaces;
using LR2.Units;
using Newtonsoft.Json;
using Random = System.Random;

namespace LR2;

public class Player(int cash, int wood, int stone, string type)
{
    public string Type { get; } = type;
    public List<IUnit> Units { get; set; } = [];
    public int Cash { get; set; } = cash;

    public int Wood { get; set; } = wood;

    public int Stone { get; set; } = stone;
    
    private const string PathToJsons =
        "/Users/heyuaresocute/projects/programming-technology-labs/Solution/LR3/jsons/";
    
    public void PlaceUnits(City city)
    {
        var factory = new UnitsFactory(city);
        if (Type == "You")
        {
            Console.WriteLine("Write indexes of units (like 1 2 3)");
            var indexes = Console.ReadLine();
            if (indexes != null & indexes!.Length == 5)
            {
                var indexesArr = Array.ConvertAll(indexes.Trim().Split(' '),Convert.ToInt32);
                var answer = CheckCash(city, indexesArr);
                if (answer)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        var unit = Selecter(factory, indexesArr[i], city.Cols - 1, city.Rows - 1 - i, $"{i + 1}");
                        Units.Add(unit);
                        Cash -= unit.Cost;
                    }
                }
                else
                {
                    PlaceUnits(city);
                }
            }
            else
            {
                PlaceUnits(city);
            }
        }
        if (Type == "Opponent")
        {
            Random rnd = new Random();
            int[] indexesArr = {1, 2, 3};
            for (int i = 0; i < 3; i++)
            {
                indexesArr[i] = rnd.Next(1, 9);
            }
            var answer = CheckCash(city, indexesArr);
            if (answer)
            {
                for (int i = 0; i < 3; i++)
                {
                    var unit = Selecter(factory,indexesArr[i], 0, i, $"{i + 7}");
                    Units.Add(unit);
                    Cash -= unit.Cost;
                }
                
            }
            else
            {
                PlaceUnits(city);
            }
        }
        Dictionary<string, string?>? units = GetUnits();
        if (units != null & Type == "You")
        {
            if (units?["type"] == "i")
            {
                var unit1 = factory.CreateNewInfantry(city.Rows - 1, city.Cols - 4, units["name"]!, Convert.ToInt16(units["health"]), Convert.ToInt16(units["attackDamage"]), Convert.ToInt16(units["attackRange"]), Convert.ToInt16(units["defence"]), Convert.ToInt16(units["movementRange"]));
                city.PlaceObject(unit1.X, unit1.Y, new Square(unit1.ShortName, 1, 1, 1, 1));
                Units.Add(unit1);
                Console.WriteLine($"{unit1.Name} added");
            }
            if (units?["type"] == "a")
            {
                var unit1 = factory.CreateNewArcher(city.Rows - 1, city.Cols - 4, units["name"]!, Convert.ToInt16(units["health"]), Convert.ToInt16(units["attackDamage"]), Convert.ToInt16(units["attackRange"]), Convert.ToInt16(units["defence"]), Convert.ToInt16(units["movementRange"]));
                city.PlaceObject(unit1.X, unit1.Y, new Square(unit1.ShortName, 1, 1, 1, 1));
                Units.Add(unit1);
                Console.WriteLine($"{unit1.Name} added");
            }
            if (units?["type"] == "h")
            {
                var unit1 = factory.CreateNewHorse(city.Rows - 1, city.Cols - 4, units["name"]!, Convert.ToInt16(units["health"]), Convert.ToInt16(units["attackDamage"]), Convert.ToInt16(units["attackRange"]), Convert.ToInt16(units["defence"]), Convert.ToInt16(units["movementRange"]));
                city.PlaceObject(unit1.X, unit1.Y, new Square(unit1.ShortName, 1, 1, 1, 1));
                Units.Add(unit1);
                Console.WriteLine($"{unit1.Name} added");
            }
        }
    }

    public bool CheckCash(City city, int[] indexes)
    {
        var factory = new UnitsFactory(city);
        var firstUnit = Selecter(factory,Convert.ToInt16(indexes[0]), city.Cols - 1, city.Rows - 1, "1");
        var secondUnit = Selecter(factory,Convert.ToInt16(indexes[1]), city.Cols - 1, city.Rows - 2, "2");
        var thirdUnit = Selecter(factory,Convert.ToInt16(indexes[2]), city.Cols - 1, city.Rows - 3, "3");
        return firstUnit.Cost + secondUnit.Cost + thirdUnit.Cost <= Cash;
    }

    public void OutputUnits()
    {
        foreach (var unit in Units)
        {
            Console.WriteLine($"{unit.ShortName}. {unit.Name}, health - {unit.Health}, attack - {unit.AttackDamage}, attack range - {unit.AttackRange}, defence - {unit.Defence}, movement range - {unit.MovementRange}, bleeds - {unit.Bleed}");
        }
    }

    public IUnit Selecter(UnitsFactory factory, int unitId, int x, int y, string id)
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
    
    private static Dictionary<string, string?>? GetUnits()
    {
        try
        {
            string json = File.ReadAllText(GetPathToFile("units.json"));
            var unit = JsonConvert.DeserializeObject<Dictionary<string, string?>>(json);
            return unit;
        }
        catch
        {
            return null;
        }
        
    }
    
    public static string GetPathToFile(string filename)
    {
        return PathToJsons + filename;
    }
}