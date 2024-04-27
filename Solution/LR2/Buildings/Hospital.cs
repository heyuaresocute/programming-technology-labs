using LR2.Interfaces;
using Newtonsoft.Json;

namespace LR2.Buildings;

public class Hospital: IImprovableBuilding
{
    public string Designation { get; set; } = "h";
    public string Name { get; } = "Hospital";
    public int StoneToImprove { get; } = 5;
    public int Y { get; set; }
    public int Level { get; set; } = 0;
    public int WoodToCreate { get; } = 10;
    public int WoodToImprove { get; } = 5;
    public int StoneToCreate { get; } = 10;
    public int X { get; set; }

    public void Create(Player player, City city)
    {
        bool flag = true;
        foreach (var building in city.CityBuildings.OfType<Hospital>())
        {
            Console.WriteLine("You can have only 1 hospital");
            flag = false;
        }

        if (flag)
        {
            Console.WriteLine("Choose the coordinates X Y: ");
            X = Convert.ToInt32(Console.ReadLine());
            Y = Convert.ToInt32(Console.ReadLine());
            player.Stone -= StoneToCreate;
            player.Wood -= WoodToCreate;
            city.CityBuildings.Add(this);
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
            city.PlaceObject(X, Y, new Square(Designation, 1, 1, 1, 1));
            foreach (var unit in player.Units)
            {
                unit.Health += Level;
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
        player.Wood -= WoodToImprove;
        player.Stone -= StoneToImprove;
        Level += 1;
        Dictionary<string, int>? buildings = GetBuildings();
        if (buildings != null & buildings!.ContainsKey(Designation))
        {
            buildings[Designation] = Level;
            string json = JsonConvert.SerializeObject(buildings);
            File.WriteAllText("/Users/heyuaresocute/projects/programming-technology-labs/Solution/LR3/jsons/buildings.json", json);
        }
        foreach (var unit in player.Units)
        {
            unit.Health += 1;
        }
        Console.WriteLine($"Now Hospital level is {Level}");
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