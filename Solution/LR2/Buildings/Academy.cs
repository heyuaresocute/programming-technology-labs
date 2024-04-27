using LR2.Factories;
using LR2.Interfaces;
using Newtonsoft.Json;

namespace LR2.Buildings;

public class Academy: IBuilding
{
    public string Designation { get; set; } = "c";
    public string Name { get; } = "Academy";
    public int WoodToCreate { get; } = 10;
    public int WoodToImprove { get; } = 5;
    public int StoneToCreate { get; } = 16;
    public int X { get; set; }
    public int Y { get; set; }
    public int StoneToImprove { get; } = 8;
    public int Level { get; set; }
    private const string PathToJsons =
        "/Users/heyuaresocute/projects/programming-technology-labs/Solution/LR3/jsons/";
    public void Create(Player player, City city)
    {
        bool flag = true;
        foreach (var building in city.CityBuildings.OfType<Academy>())
        {
            Console.WriteLine("You can have only 1 academy");
            flag = false;
        }

        if (flag)
        {
            AskForUnit(player, city);
            Console.WriteLine("Choose the coordinates X Y: ");
            X = Convert.ToInt32(Console.ReadLine());
            Y = Convert.ToInt32(Console.ReadLine());
            player.Stone -= StoneToCreate;
            player.Wood -= WoodToCreate;
            city.CityBuildings.Add(this);
            city.PlaceObject(X, Y, new Square(Designation, 1, 1, 1, 1));
        }
    }

    private void AskForUnit(Player player, City city)
    {
        Console.WriteLine("Now you can make a new unit. 1 - create, 2 - skip");
        var x = Convert.ToInt16(Console.ReadLine());
        if (x != 2)
        {
            Console.WriteLine("Choose the type of unit: i - infantry, h - horse, a - archer ");
            var type = Console.ReadLine();
            Console.WriteLine("Choose the name: ");
            var name = Console.ReadLine();
            Console.WriteLine("Choose the health: ");
            var health = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Choose the attack damage: ");
            var attackDamage = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Choose the attack range: ");
            var attackRange = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Choose the defence: ");
            var defence = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Choose the movement range: ");
            var movementRange = Convert.ToInt16(Console.ReadLine());
            CreateNewUnit(player, name!, health, attackDamage, attackRange, defence, movementRange, type, city);
        }
    }

    private void CreateNewUnit(Player player, string name, short health, short attackDamage, short attackRange, short defence, short movementRange, string? type, City city)
    {
        var cost = FindCost(health, attackDamage, attackRange, defence, movementRange, type);
        Console.WriteLine($"Cost is {cost}. ");
        if (player.Cash < cost)
        {
            Console.WriteLine("You don't have enough money");
            AskForUnit(player, city);
        }
        else
        {
            Console.WriteLine("Do you want to create this unit? 1 - yes, 2 - no");
            var x = Convert.ToInt16(Console.ReadLine());
            switch (x)
            {
                case 1:
                    var factory = new UnitsFactory(city);
                    Dictionary<string, string?> dictionary = new()
                    {
                        { "x", Convert.ToString(city.Cols - 4) },
                        { "y", Convert.ToString(city.Rows - 4) },
                        { "name", name },
                        { "health", Convert.ToString(health) },
                        { "attackDamage", Convert.ToString(attackDamage) },
                        { "attackRange", Convert.ToString(attackRange) },
                        { "defence", Convert.ToString(defence) },
                        { "movementRange", Convert.ToString(movementRange) },
                        { "type", type }
                    };
                    if (type == "i")
                    {
                        var unit = factory.CreateNewInfantry(city.Cols - 4, city.Rows - 4, name, health, attackDamage, attackRange, defence, movementRange);
                        player.Units.Add(unit);
                        dictionary["type"] = type;
                        PutInFile("units.json", dictionary);
                    }

                    if (type == "h")
                    {
                        dictionary[type] = type;
                        var unit = factory.CreateNewHorse(2, 2, name, health, attackDamage, attackRange, defence, movementRange);
                        player.Units.Add(unit);
                        PutInFile("units.json", dictionary);
                    }

                    if (type == "a")
                    {
                        dictionary[type] = type;
                        var unit = factory.CreateNewArcher(2, 2, name, health, attackDamage, attackRange, defence, movementRange);
                        player.Units.Add(unit);
                        PutInFile("units.json", dictionary);
                    }
                    break;
                case 2:
                    AskForUnit(player, city);
                    break;
            }
        }
    }

    private int FindCost(short health, short attackDamage, short attackRange, short defence, short movementRange, string? type)
    {
        var cost = health / 2 + attackDamage + attackRange / 2 + defence / 3 + movementRange / 2;
        switch (type)
        {
            case "a":
                cost += 5;
                break;
            case "h":
                cost += 2;
                break;
        }
        return Convert.ToInt32(cost);
    }

    public void Output()
    {
        if (Level > 0)
        {
            Console.WriteLine($"{Name}: level {Level} - {Designation}");
        }
        else
        {
            Console.WriteLine($"{Name}: wood - {WoodToCreate}, stone - {StoneToCreate} - {Designation}");
        }
    }
    
    private static string GetPathToFile(string filename)
    {
        return PathToJsons + filename;
    }

    private static void PutInFile<T>(string filename, T data)
    {
        string json = JsonConvert.SerializeObject(data);
        File.WriteAllText(GetPathToFile(filename), json);
    }
}