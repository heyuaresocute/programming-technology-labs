using LR2.Factories;
using LR2.MapProperties;
using LR2.Units;
using Newtonsoft.Json;

namespace LR4Tests;

public class ChooseUnitsTests
{
    [Test]
    public void PlayerDontHaveEnoughCash()
    {
        //Arrange
        var player = new Player(1, 1, 1, "You");
        List<Map> maps = GetMaps();
        var map = maps[0];
        var city = new City(100, map);
        city.GenerateCity();
        //Act
        var result = player.CheckCash(city, [1, 2, 3]);
        //Assert
        if (!result)
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail();
        }
    }
    
    [Test]
    public void PlayerHaveEnoughCash()
    {
        //Arrange
        var player = new Player(100, 1, 1, "You");
        List<Map> maps = GetMaps();
        var map = maps[0];
        var city = new City(100, map);
        city.GenerateCity();
        //Act
        var result = player.CheckCash(city, [1, 2, 3]);
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
    
    [Test]
    public void CorrectUnitsChosen()
    {
        //Arrange
        var player = new Player(100, 1, 1, "You");
        List<Map> maps = GetMaps();
        var map = maps[0];
        var city = new City(100, map);
        city.GenerateCity();
        var factory = new UnitsFactory(city);
        //Act
        var firstUnit = player.Selecter(factory,1, city.Cols - 1, city.Rows - 1, "1");
        var secondUnit = player.Selecter(factory,2, city.Cols - 1, city.Rows - 2, "2");
        var thirdUnit = player.Selecter(factory,3, city.Cols - 1, city.Rows - 3, "3");
        //Assert
        if (firstUnit.Name == "Swordsman" & secondUnit.Name == "Spearman" & thirdUnit.Name == "Axeman")
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