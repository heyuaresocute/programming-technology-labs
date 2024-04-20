using System.Diagnostics.SymbolStore;
using Newtonsoft.Json;

namespace LR3
{
    internal static class Lab
    {
        private const string Path =
            "/Users/heyuaresocute/projects/programming-technology-labs/Solution/LR3/jsons/obstacles.json";
        private static void Main()
        {
            Dictionary<string, double[]>? obstaclesDictionary;
            try
            {
                string json = File.ReadAllText(Path); 
                obstaclesDictionary = JsonConvert.DeserializeObject<Dictionary<string, double[]>>(json);
            }
            catch (Exception e)
            {
                obstaclesDictionary = new Dictionary<string, double[]>()
                {
                    ["S"] = [3, 1.5, 1.8, 2.2, 1.5], // count, Inf, Arch, Horse, Cat
                    ["H"] = [1, 2, 2.2, 1.2, 2],
                    ["T"] = [4, 1.2, 1, 1.5, 1]
                };
                string json = JsonConvert.SerializeObject(obstaclesDictionary);
                File.WriteAllText(Path, json);
                Console.WriteLine(e);
            }
            Console.WriteLine("Hello, this is the card redactor for Bauman's Gate!");
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("1 - Create new map, 2 - Change map, 3 - Delete map, 4 - Add new obstacle, 5 - delete obstacle");
            var x = Convert.ToInt32(Console.ReadLine());
            switch (x)
            {
                case 4:
                    AddObstacle(obstaclesDictionary);
                    break;
            }
        }

        private static void AddObstacle(Dictionary<string, double[]>? obstaclesDictionary)
        {
            Console.WriteLine("Now you have this obstacles:");
            OutputDictionary(obstaclesDictionary);
            Console.WriteLine("You can make a new one. Please write the designation:");
            var designation = Console.ReadLine();
            Console.WriteLine("Please write the InfantryFine:");
            var infantryFine = Console.ReadLine();
            Console.WriteLine("Please write the ArcherFine:");
            var archerFine = Console.ReadLine();
            Console.WriteLine("Please write the HorseFine:");
            var horseFine = Console.ReadLine();
            Console.WriteLine("Please write the CatFine:");
            var catFine = Console.ReadLine();
        }

        private static void OutputDictionary(Dictionary<string, double[]>? dictionary)
        {
            foreach (var obstacle in dictionary)
            {
                Console.WriteLine($"Symbol: {obstacle.Key}, InfantryFine: {obstacle.Value[1]}, ArcherFine: {obstacle.Value[2]}, HorseFine: {obstacle.Value[3]}, CatFine: {obstacle.Value[4]}");
            }
        }
    }
}