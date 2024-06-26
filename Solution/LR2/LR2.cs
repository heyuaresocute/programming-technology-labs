﻿using LR2.Factories;
using LR2.Interfaces;
using LR2.MapProperties;
using Newtonsoft.Json;
using NLog;

namespace LR2
{
    public class Lab
    {
        private const string PathToJsons =
            "/Users/heyuaresocute/projects/programming-technology-labs/Solution/LR3/jsons/";

        private static void Main()
        {
            ConfigureLog();
            var startcash = 69;
            var catChanse = 100;
            var wood = 30;
            var stone = 30;
            Console.WriteLine("Now you have this maps:");
            List<Map> maps = GetMaps();
            var mapsAsIData = new List<IData>(maps);
            OutputList(mapsAsIData);
            Console.WriteLine("Choose the map. Please write the designation:");
            var designation = Console.ReadLine();
            Map map = new Map("", 0, 0);
            foreach (var map1 in maps)
            {
                if (map1.Designation == designation)
                {
                    map = map1;
                }
            }
            map.Output();
            
            var city = new City(catChanse, map);
            city.GenerateCity();
            var player = new Player(startcash, wood, stone, "You");
            var opponent = new Player(startcash, wood, stone, "Opponent");
            city.Players.Add(player);
            city.Players.Add(opponent);
            Console.WriteLine($"Your start cash is {startcash}. Choose your units (select 3): ");
            player.PlaceUnits(city);
            opponent.PlaceUnits(city);
            Game.Start(city);
        }

        private static void ConfigureLog()
        {
            var config = new NLog.Config.LoggingConfiguration();
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "cat.log" };
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);
            LogManager.Configuration = config;
        }

        private static List<Map> GetMaps()
        {
            string json = File.ReadAllText(GetPathToFile("maps.json"));
            var maps = JsonConvert.DeserializeObject<List<Map>>(json);
            return maps!;
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