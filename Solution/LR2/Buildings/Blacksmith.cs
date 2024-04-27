using LR2.Interfaces;
using Newtonsoft.Json;

namespace LR2.Buildings;

public class Blacksmith: IImprovableBuilding
{
    public string Designation { get; set; } = "b";
    public string Name { get; } = "Blacksmith";
    public int WoodToCreate { get; } = 10;
    public int WoodToImprove { get; } = 5;
    public int StoneToCreate { get; } = 16;
    public int X { get; set; }
    public int Y { get; set; }
    public int StoneToImprove { get; } = 8;
    public int Level { get; set; }
    public void Create(Player player, City city)
    {
        bool flag = true;
        foreach (var building in city.CityBuildings.OfType<Blacksmith>())
        {
            Console.WriteLine("You can have only 1 blacksmith");
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
            foreach (var unit in player.Units)
            {
                unit.AttackDamage += Level;
            }
        }
    }

    public void Output()
    {
        Console.WriteLine(Level > 0
            ? $"{Name}: level {Level}, wood - {WoodToImprove}, stone - {StoneToImprove} - {Designation}"
            : $"{Name}: wood - {WoodToCreate}, stone - {StoneToCreate} - {Designation}");
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
            unit.AttackDamage += 1;
        }
        Console.WriteLine($"Now Blacksmith level is {Level}");
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