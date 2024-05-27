using LR3.Classes;
using LR3.Interfaces;
using Newtonsoft.Json;

namespace LR3
{
    internal static class Lab
    {
        private static void Main()
        {
            List<ObstacleType> obstacles = GetObstacles();
            List<Map> maps = GetMaps();
            Redactor.Start(maps, obstacles);
        }
        
        private static List<Map> GetMaps()
        {
            var maps = new List<Map>{};
            try
            {
                string json = File.ReadAllText(Redactor.GetPathToFile("maps.json")); 
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
        
        private static List<ObstacleType> GetObstacles()
        {
            var obstacles = new List<ObstacleType>{};
            try
            {
                string json = File.ReadAllText(Redactor.GetPathToFile("obstacles.json")); 
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
            File.WriteAllText(Redactor.GetPathToFile("obstacles.json"), json);
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
            File.WriteAllText(Redactor.GetPathToFile("maps.json"), json);
        }
    }
}

//TODO новое здание - алхимическая лаборатория. Ее можно построить только после захвата кота. В лаборатории за некоторую сумму можно попробовать мутировать кота. Пользователь вводит желаемые статы для кота. Сумма всех статов - константа. Перераспределение статов. Так как алхимик учился в МГТУ каждый раз кот получает некоторый случайный эффект - полет (перемещение без ограничений), хромота - можно ходить только в одном направлении, кот становится огромным - занимает 4 клетки, занимает 1hp. но бить можно по любой из 4.