using LR3.Classes;
using LR3.Interfaces;
using Newtonsoft.Json;

namespace LR3
{
    internal static class Lab
    {
        private const string PathToJsons =
            "/Users/heyuaresocute/projects/programming-technology-labs/Solution/LR3/jsons/";
        private static void Main()
        {
            List<ObstacleType> obstacles = GetObstacles();
            List<Map> maps = GetMaps();
            Console.WriteLine("Hello, this is the card redactor for Bauman's Gate!");
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("1 - Create new map, 2 - Change map, 3 - Delete map, 4 - Add new obstacle, 5 - delete obstacle, 6 - Output maps, 7 - Output obstacles, 0 - stop");
            var x = Convert.ToInt32(Console.ReadLine());
            while (x != 0)
            {
                switch (x)
                {
                    case 1:
                        AddObject(maps, obstacles);
                        break;
                    case 2:
                        ChangeMap(maps, obstacles);
                        break;
                    case 3:
                        var mapsAsIData = new List<IData>(maps);
                        DeleteObject(mapsAsIData, "maps.json");
                        break;
                    case 4:
                        AddObject(obstacles);
                        break;
                    case 5:
                        var obstaclesAsIData = new List<IData>(obstacles);
                        DeleteObject(obstaclesAsIData, "obstacles.json");
                        break;
                    case 6:
                        var mapsAsIData2 = new List<IData>(maps);
                        OutputList(mapsAsIData2);
                        break;
                    case 7:
                        var obstaclesAsIData2 = new List<IData>(obstacles);
                        OutputList(obstaclesAsIData2);
                        break;
                }
                Console.WriteLine("1 - Create new map, 2 - Change map, 3 - Delete map, 4 - Add new obstacle, 5 - delete obstacle, 6 - Output maps, 7 - Output obstacles, 0 - stop");
                x = Convert.ToInt32(Console.ReadLine());
            }
        }

        private static void ChangeMap(List<Map> maps, List<ObstacleType> obstacleTypes)
        {
            var mapsAsIData = new List<IData>(maps);
            Console.WriteLine("Now you have this maps:");
            OutputList(mapsAsIData);
            Console.WriteLine("You can change map. Please write the designation:");
            var designation = Console.ReadLine();
            Map mapToChange = new Map("", 0, 0);
            foreach (var map in maps)
            {
                if (map.Designation == designation)
                {
                    mapToChange = map;
                }
            }
            mapToChange.Output();
            maps.Remove(mapToChange);
            Console.WriteLine("1 - change existing object, 2 - make new object, 0 - stop");
            var a = Convert.ToInt16(Console.ReadLine());
            while (a != 0)
            {
                switch (a)
                {
                    case 1:
                        Console.WriteLine("1 - change obstacle, 0 - stop");
                        var b = Convert.ToInt16(Console.ReadLine());
                        while (b != 0)
                        {
                            mapToChange = ChangeObstacle(mapToChange);
                            mapToChange.Output();
                            Console.WriteLine("1 - change obstacle, 0 - stop");
                            b = Convert.ToInt16(Console.ReadLine());
                        }
                        break;
                    case 2:
                        Console.WriteLine("1 - add obstacle, 0 - stop");
                        var c = Convert.ToInt16(Console.ReadLine());
                        while (c != 0)
                        {
                            mapToChange = AddObstacle(obstacleTypes, mapToChange);
                            mapToChange.Output();
                            Console.WriteLine("1 - add obstacle, 0 - stop");
                            c = Convert.ToInt16(Console.ReadLine());
                        }
                        break;
                }
                Console.WriteLine("1 - change existing object, 2 - make new object, 0 - stop");
                a = Convert.ToInt16(Console.ReadLine());
            }
            maps.Add(mapToChange);
            PutInFile("maps.json", maps);
        }

        private static Map ChangeObstacle(Map map)
        {
            map.Output();
            Console.WriteLine("Please, choose one obstacle . Select coordinates (X, Y): ");
            Console.WriteLine("X: ");
            var x = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Y: ");
            var y = Convert.ToInt16(Console.ReadLine());
            Obstacle obstacleToChange = new Obstacle("", 0, 0);
            foreach (var obstacle in map.Obstacles)
            {
                if (obstacle.X == x & obstacle.Y == y)
                {
                    obstacleToChange = obstacle;
                }
            }
            map.Obstacles.Remove(obstacleToChange);
            Console.WriteLine("Select new coordinates (X, Y): ");
            Console.WriteLine("X: ");
            x = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Y: ");
            y = Convert.ToInt16(Console.ReadLine());
            obstacleToChange.X = x;
            obstacleToChange.Y = y;
            map.Obstacles.Add(obstacleToChange);
            return map;
        }

        private static List<Map> GetMaps()
        {
            var maps = new List<Map>{};
            try
            {
                string json = File.ReadAllText(GetPathToFile("maps.json")); 
                maps = JsonConvert.DeserializeObject<List<Map>>(json)!;
                if (maps.Count == 0)
                {
                    AddDefaultMap(maps);
                }
            }
            catch (Exception e)
            {
                AddDefaultMap(maps);
                Console.WriteLine(e);
            }

            return maps!;
        }

        private static void AddDefaultMap(List<Map> maps)
        {
            var rnd = new Random();
            List<ObstacleType> obstacles = GetObstacles();
            var map = new Map("Default", 10, 10);
            foreach (var obstacle in obstacles)
            {
                for (int i = 0; i < obstacle.StandartCount; i++)
                {
                    var x = rnd.Next(0, map.Cols);
                    var y = rnd.Next(0, map.Rows);
                    map.AddObstacle(obstacle.Designation, x, y);
                }
            }
            maps.Add(map);
            var json = JsonConvert.SerializeObject(maps);
            File.WriteAllText(GetPathToFile("maps.json"), json);
        }

        private static List<ObstacleType> GetObstacles()
        {
            var obstacles = new List<ObstacleType>{};
            try
            {
                string json = File.ReadAllText(GetPathToFile("obstacles.json")); 
                obstacles = JsonConvert.DeserializeObject<List<ObstacleType>>(json)!;
                if (obstacles.Count == 0)
                {
                    AddDefaultObstacles(obstacles);
                }
            }
            catch (Exception e)
            {
                AddDefaultObstacles(obstacles);
                Console.WriteLine(e);
            }

            return obstacles!;
        }

        private static void AddDefaultObstacles(List<ObstacleType> obstacles)
        {
            ObstacleType swamp = new ObstacleType("S", 1.5, 1.8, 2.2, 1.5, 3);
            ObstacleType hill = new ObstacleType("H", 2, 2.2, 1.2, 2, 3);
            ObstacleType tree = new ObstacleType("T", 1.2, 1, 1.5, 1, 3);
            obstacles.Add(swamp);
            obstacles.Add(hill);
            obstacles.Add(tree);
            string json = JsonConvert.SerializeObject(obstacles);
            File.WriteAllText(GetPathToFile("obstacles.json"), json);
        }

        private static void DeleteObject(List<IData> data, string filename)
        {
            Console.WriteLine("Now you have:");
            OutputList(data);
            Console.WriteLine("You can delete one. Please write its designation:");
            var designation = Console.ReadLine();
            data.RemoveAll(obstacle => obstacle.Designation == designation);
            PutInFile(filename, data);
        }

        private static void AddObject(List<ObstacleType> obstacles)
        {
            var obstaclesAsIData = new List<IData>(obstacles);
            Console.WriteLine("Now you have this obstacles:");
            OutputList(obstaclesAsIData);
            Console.WriteLine("You can make a new one. Please write the designation:");
            var designation = Console.ReadLine();
            Console.WriteLine("Please write the InfantryFine:");
            var infantryFine = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Please write the ArcherFine:");
            var archerFine = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Please write the HorseFine:");
            var horseFine = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Please write the CatFine:");
            var catFine = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Please write the StandartCount:");
            var standartCount = Convert.ToInt16(Console.ReadLine());
            obstacles.Add(new ObstacleType(designation!, infantryFine, archerFine, horseFine, catFine, standartCount));
            PutInFile("obstacles.json", obstacles);
        }

        private static void AddObject(List<Map> maps, List<ObstacleType> obstacleTypes)
        {
            var mapsAsIData = new List<IData>(maps);
            Console.WriteLine("Now you have this maps:");
            OutputList(mapsAsIData);
            Console.WriteLine("You can make a new one. Please write the designation:");
            var designation = Console.ReadLine();
            Console.WriteLine("Please write the Cols number:");
            var cols = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Please write the Rows number:");
            var rows = Convert.ToInt16(Console.ReadLine());
            var map = new Map(designation!, cols, rows);
            Console.WriteLine("Now you should add the obstacles. Press 1 - if you want to add obstacle, 0 - stop");
            var a = Convert.ToInt16(Console.ReadLine());
            while (a != 0)
            {
                map = AddObstacle(obstacleTypes, map);
                Console.WriteLine("Press 1 - if you want to add obstacle, 0 - stop");
                a = Convert.ToInt16(Console.ReadLine());
            }
            maps.Add(map);
            PutInFile("maps.json", maps);
        }

        private static Map AddObstacle(List<ObstacleType> obstacleTypes, Map map)
        {
            Console.WriteLine("Choose one:");
            var obstaclesAsIData = new List<IData>(obstacleTypes);
            OutputList(obstaclesAsIData);
            Console.WriteLine("Now write its designation: ");
            var designation = Console.ReadLine();
            Console.WriteLine("X: ");
            var x = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Y: ");
            var y = Convert.ToInt16(Console.ReadLine());
            map.Obstacles.Add(new Obstacle(designation!, x, y));
            return map;
        }

        private static void PutInFile<T>(string filename, T data)
        {
            string json = JsonConvert.SerializeObject(data);
            File.WriteAllText(GetPathToFile(filename), json);
        }

        private static string GetPathToFile(string filename)
        {
            return PathToJsons + filename;
        }


        private static void OutputList(List<IData> list)
        {
            foreach (var obj in list)
            {
                obj.Output();
            }
        }
    }
}