using LR2.MapProperties;
using LR2.Units;
using Newtonsoft.Json;

namespace LR4Tests;

public class DeathTests
{
    private List<InfantryUnit> Arrange()
    {
        var unit1 = new InfantryUnit("1", 1, 1, 1, 0, 1, 1, 1, 1, "1");
        var unit2 = new InfantryUnit("1", 1, 1, 1, 0, 1, 1, 1, 1, "1");
        return [unit1, unit2];
    }
    
    private static List<Map> GetMaps()
    {
        string json =
            File.ReadAllText("/Users/heyuaresocute/projects/programming-technology-labs/Solution/LR3/jsons/maps.json");
        var maps = JsonConvert.DeserializeObject<List<Map>>(json);
        return maps!;
    }
    
    [Test]
    public void Attack()
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
        var units = Arrange();
        units[0].X = 0;
        units[0].Y = 0;
        units[1].X = 1;
        units[1].Y = 0;
        Player player = new Player(1, 1, 1, "You");
        player.Units.Add(units[0]);
        Player opponent = new Player(1, 1, 1, "Opponent");
        opponent.Units.Add(units[1]);
        //Act
        Game.AttackUnit(city, units[0], player, opponent, units[1]);
        //Assert
        if (opponent.Units.Count == 0)
        {
            Assert.Pass();
        }
        else
        {
            Console.WriteLine(opponent.Units.Count);
            Assert.Fail();
        }
    }
}