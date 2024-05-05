using LR2.MapProperties;
using LR2.Units;
using Newtonsoft.Json;

namespace LR4Tests;

public class BotTests
{
    private List<InfantryUnit> Arrange()
    {
        var unit1 = new InfantryUnit("1", 100, 1, 1, 0, 1, 1, 1, 1, "1");
        var unit2 = new InfantryUnit("1", 100, 1, 1, 0, 1, 1, 1, 1, "1");
        return [unit1, unit2];
    }
    [Test]
    public void Attack()
    {
        //Arrange
        List<Map> maps = GetMaps();
        var map = maps[0];
        var city = new City(100, map);
        city.GenerateCity();
        var units = Arrange();
        units[0].X = 0;
        units[0].Y = 0;
        units[1].X = 1;
        units[1].Y = 0;
        Player player = new Player(1, 1, 1, "You");
        player.Units.Add(units[0]);
        Player opponent = new Player(1, 1, 1, "Opponent");
        opponent.Units.Add(units[1]);
        city.Players.Add(player);
        city.Players.Add(opponent);
        var health = units[0].Health;
        //Act
        Game.OpponentsStep(city);
        //Assert
        if (player.Units[0].Health < health )
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail();
        }
    }
    
    [Test]
    public void Move()
    {
        //Arrange
        List<Map> maps = GetMaps();
        var map = maps[0];
        var city = new City(100, map);
        city.GenerateCity();
        var units = Arrange();
        units[0].X = 0;
        units[0].Y = 0;
        units[1].X = 9;
        units[1].Y = 9;
        Player player = new Player(1, 1, 1, "You");
        player.Units.Add(units[0]);
        Player opponent = new Player(1, 1, 1, "Opponent");
        opponent.Units.Add(units[1]);
        city.Players.Add(player);
        city.Players.Add(opponent);
        var health = units[0].Health;
        //Act
        Game.OpponentsStep(city);
        //Assert
        if (player.Units[0].Health == health )
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