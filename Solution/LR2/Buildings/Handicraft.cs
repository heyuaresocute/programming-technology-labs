using LR2.Interfaces;
using Newtonsoft.Json;

namespace LR2.Buildings;

public class Handicraft: IImprovableBuilding
{
    public string Designation { get; set; } = "r";
    public string Name { get; } = "Handicraft";
    public int WoodToCreate { get; } = 10;
    public int WoodToImprove { get; } = 5;
    public int StoneToCreate { get; } = 16;
    public int X { get; set; }
    public int Y { get; set; }
    public int StoneToImprove { get; } = 8;
    public int Level { get; set; }

    public const int Cash = 10;
    public void Create(Player player, City city)
    {
        bool flag = true;
        var count = 0;
        foreach (var building in city.CityBuildings.OfType<Handicraft>())
        {
            count += 1;
        }

        if (count >= 4)
        {
            Console.WriteLine("You can have only 4 hadicrafts");
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
        }
    }

    public void Output()
    {
        Console.WriteLine(Level > 0
            ? $"{Name}: crafters - {Level}, wood - {WoodToImprove}, stone - {StoneToImprove} - {Designation}"
            : $"{Name}: wood - {WoodToCreate}, stone - {StoneToCreate} - {Designation}");
    }

    public void Improve(Player player, City city)
    {
        player.Wood -= WoodToImprove;
        player.Stone -= StoneToImprove;
        Dictionary<string, int>? buildings = GetBuildings();
        Level += 1;
        if (buildings != null & buildings!.ContainsKey(Designation))
        {
            buildings[Designation] = Level;
            string json = JsonConvert.SerializeObject(buildings);
            File.WriteAllText("/Users/heyuaresocute/projects/programming-technology-labs/Solution/LR3/jsons/buildings.json", json);
        }
        Console.WriteLine($"Now your handicraft has {Level} crafters");
    }

    public void GiveMoney(Player player)
    {
        player.Cash += Cash * Level;
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