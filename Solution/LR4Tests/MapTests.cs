using LR2.Factories;
using LR2.MapProperties;
using Newtonsoft.Json;

namespace LR4Tests;

public class MapTests
{
    [Test]
    public void CorrectMap()
    {
        //Arrange
        var player = new Player(100, 1, 1, "You");
        List<Map> maps = GetMaps();
        var map = maps[0];
        var city = new City(100, map);
        city.GenerateCity();
        var result = true;
        //Act
        foreach (var obstacle in map.Obstacles)
        {
            if (city.CityObjects[obstacle.Y][obstacle.X].Obj != obstacle.Designation)
            {
                result = false;
            }
        }
        //Assert
        if (result)
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail();
        }
    }
    
    private static List<Map> GetMaps()
    {
        string json =
            File.ReadAllText("/Users/heyuaresocute/projects/programming-technology-labs/Solution/LR3/jsons/maps.json");
        var maps = JsonConvert.DeserializeObject<List<Map>>(json);
        return maps!;
    }
}