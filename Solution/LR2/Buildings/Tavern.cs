using LR2.Interfaces;
using Newtonsoft.Json;

namespace LR2.Buildings;

public class Tavern: IImprovableBuilding
{
    public string Designation { get; set; } = "t";
    public string Name { get; } = "Tavern";
    public int WoodToCreate { get; } = 10;
    public int WoodToImprove { get; } = 5;
    public int StoneToCreate { get; } = 15;
    public int X { get; set; }
    public int Y { get; set; }
    public int StoneToImprove { get; } = 10;
    public int Level { get; set; } = 0;
    public void Create(Player player, City city)
    {
        bool flag = true;
        foreach (var building in city.CityBuildings.OfType<Tavern>())
        {
            Console.WriteLine("You can have only 1 tavern");
            flag = false;
        }

        if (flag)
        {
            Console.WriteLine("Choose the coordinates X Y: ");
            X = Convert.ToInt32(Console.ReadLine());
            Y = Convert.ToInt32(Console.ReadLine());
            player.Stone -= StoneToCreate;
            player.Wood -= WoodToCreate;
            Dictionary<string, int>? buildings = GetBuildings();
            if (buildings != null )
            {
                if (buildings.ContainsKey(Designation))
                { 
                    Level = buildings[Designation];
                }
                else
                {
                    Level = 1;
                    buildings.Add(Designation, Level);
                    string json = JsonConvert.SerializeObject(buildings);
                    File.WriteAllText("/Users/heyuaresocute/projects/programming-technology-labs/Solution/LR3/jsons/buildings.json", json);
                }
            }
            city.CityBuildings.Add(this);
            city.PlaceObject(X, Y, new Square(Designation, 1, 1, 1, 1));
            city.CityBuildings.Add(this);
            city.PlaceObject(X, Y, new Square(Designation, 1, 1, 1, 1));
            Console.WriteLine("1 - improve Movement Range, 2 - lower fines");
            var a = Convert.ToInt16(Console.ReadLine());
            switch (a)
            {
                case 1:
                    foreach (var unit in player.Units)
                    {
                        unit.MovementRange += Level;
                    }
                    break;
                case 2:
                    city.TavernBonus += 0.5 * Level;
                    break;
            }
        }
    }

    public void Output()
    {
        if (Level > 0)
        {
            Console.WriteLine($"{Name}: level {Level}, wood - {WoodToImprove}, stone - {StoneToImprove} - {Designation}");
        }
        else
        {
            Console.WriteLine($"{Name}: wood - {WoodToCreate}, stone - {StoneToCreate} - {Designation}");
        }
    }

    public void Improve(Player player, City city)
    {
        Console.WriteLine("1 - improve Movement Range, 2 - lower fines");
        var a = Convert.ToInt16(Console.ReadLine());
        switch (a)
        {
            case 1:
                foreach (var unit in player.Units)
                {
                    unit.MovementRange += 1;
                }
                break;
            case 2:
                city.TavernBonus += 0.5;
                break;
        }
        Level += 1;
        Dictionary<string, int>? buildings = GetBuildings();
        if (buildings != null & buildings!.ContainsKey(Designation))
        {
            buildings[Designation] = Level;
            string json = JsonConvert.SerializeObject(buildings);
            File.WriteAllText("/Users/heyuaresocute/projects/programming-technology-labs/Solution/LR3/jsons/buildings.json", json);
        }
    }
    
    public Dictionary<string, int>? GetBuildings()
    {
        try
        {
            string json = File.ReadAllText("/Users/heyuaresocute/projects/programming-technology-labs/Solution/LR3/jsons/buildings.json");
            var buildings = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
            return buildings;
        }
        catch
        {
            return null;
        }
    }
}