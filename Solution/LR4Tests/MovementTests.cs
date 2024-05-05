using LR2.Factories;
using LR2.Interfaces;
using LR2.MapProperties;
using LR2.Units;
using Newtonsoft.Json;

namespace LR4Tests;

public class MovementTests
{
    [Test]
    public void CollisionTest()
    {
        //Arrange
        var startcash = 69;
        var catChanse = 100;
        var wood = 30;
        var stone = 30;
        Game game = new Game();
        List<Map> maps = GetMaps();
        var map = maps[0];
        var city = new City(catChanse, map);
        city.GenerateCity();
        var unitsFactory = new UnitsFactory(city);
        var unit = unitsFactory.CreateSwordsman(9, 9, "1");
        int[] expectedResult = { 9, 7 };
        //Act
        unit.Move("u", city);
        int[] result = { unit.X, unit.Y };
        //Assert
        bool flag = result[0] == expectedResult[0];
        if (result[1] != expectedResult[1])
        {
            flag = false;
        }

        if (flag)
        {
            Assert.Pass();
        }
        else
        {
            Console.WriteLine($"{result[0]}, {result[1]}");
            Assert.Fail();
        }
    }
    [Test]
    public void WallTest()
    {
        //Arrange
        var startcash = 69;
        var catChanse = 100;
        var wood = 30;
        var stone = 30;
        Game game = new Game();
        List<Map> maps = GetMaps();
        var map = maps[0];
        var city = new City(catChanse, map);
        city.GenerateCity();
        var unitsFactory = new UnitsFactory(city);
        var unit = unitsFactory.CreateSwordsman(9, 9, "1");
        int[] expectedResult = { 9, 9 };
        //Act
        unit.Move("r", city);
        int[] result = { unit.X, unit.Y };
        //Assert
        bool flag = result[0] == expectedResult[0];
        if (result[1] != expectedResult[1])
        {
            flag = false;
        }

        if (flag)
        {
            Assert.Pass();
        }
        else
        {
            Console.WriteLine($"{result[0]}, {result[1]}");
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