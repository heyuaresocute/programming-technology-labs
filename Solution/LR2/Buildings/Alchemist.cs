using LR2.Animals;
using LR2.Factories;
using LR2.Interfaces;
using Newtonsoft.Json;

namespace LR2.Buildings;

public class Alchemist: IImprovableBuilding
{
    public string Designation { get; set; } = "l";
    public string Name { get; } = "Alchemist";
    public int WoodToCreate { get; } = 10;
    public int WoodToImprove { get; } = 5;
    public int StoneToCreate { get; } = 16;
    public int X { get; set; }
    public int Y { get; set; }
    public int StoneToImprove { get; } = 8;
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
        Console.WriteLine("Do you want to mutate cat? 1 - yes, 2 - no");
            if(Console.ReadLine() == "1"){
                if(PlayerHasEnoughMoney(player)){
                    player.Cash -= 10;
                    MutateCat(player, city);            
                }
                else{
                    Console.WriteLine("You don't have enough money");
                }
            }
        Console.WriteLine($"Now Alchemist level is {Level}");
    }

    public int Level { get; set; }
    private const string PathToJsons =
        "/Users/heyuaresocute/projects/programming-technology-labs/Solution/LR3/jsons/";
    public void Create(Player player, City city)
    {
        bool flag = true;
        foreach (var building in city.CityBuildings.OfType<Alchemist>())
        {
            Console.WriteLine("You can have only 1 alchemist");
            flag = false;
        }

        if (city.Animals.Count == 0)
        {
            Console.WriteLine("You must have a cat to build this");
            flag = false;
        }
        else
        {
            flag = false;
            foreach (var cat in city.Animals.OfType<Cat>())
            {
                if (cat.Owner == player)
                {
                    flag = true;
                }
            }

            if (!flag)
            {
                Console.WriteLine("You must have a cat to build this");
            }
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
            Console.WriteLine("Do you want to mutate cat? 1 - yes, 2 - no");
            if(Console.ReadLine() == "1"){
                if(PlayerHasEnoughMoney(player)){
                    player.Cash -= 10;
                    MutateCat(player, city);            
                }
                else{
                    Console.WriteLine("You don't have enough money");
                }
            }
        }
    }

    private bool PlayerHasEnoughMoney(Player player)
    {
        return player.Cash - 10 > 0;
    }

    private void MutateCat(Player player, City city)
    {
        foreach (var cat in city.Animals.OfType<Cat>())
        {
            if (cat.Owner == player)
            {
                var statsSum = GetStatsSum(cat);
                int[] desiredStats = GetDesiredStats(statsSum);
                cat.Health = desiredStats[0];
                cat.Defence = desiredStats[1];
                cat.AttackDamage = desiredStats[2];
                cat.MovementRange = desiredStats[3];
                cat.AttackRange = desiredStats[4];
                
            }
        }
    }

    private int[] GetDesiredStats(int statsSumm)
    {
        while (true)
        {
            Console.WriteLine("Please, write the desired stats: Health, Defence, AttackDamage, MovementRange, AttackRange");
            Console.WriteLine($"Your cat stats summ now is: {statsSumm}");
            var indexesAsString = Console.ReadLine();
            if(indexesAsString != null){
                var desiredStatsArr = Array.ConvertAll(indexesAsString.Trim().Split(' '),Convert.ToInt32);
                if (desiredStatsArr.Sum() == statsSumm & desiredStatsArr.Length == 5){
                    return desiredStatsArr;
                }
                else {
                    Console.WriteLine("You need to choose stats with the same summ");
                }
            }
        }
            
    }

    private int GetStatsSum(Cat cat)
    {
        return cat.Health + cat.Defence + cat.AttackDamage + cat.MovementRange + cat.AttackRange;
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