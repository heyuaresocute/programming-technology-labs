using LR2.Units;

namespace LR4Tests;

public class DefenceTests
{
    private List<InfantryUnit> Arrange()
    {
        var unit1 = new InfantryUnit("1", 10, 1, 1, 10, 1, 1, 1, 1, "1");
        var unit2 = new InfantryUnit("1", 10, 1, 1, 10, 1, 1, 1, 1, "1");
        return [unit1, unit2];
    }
    [Test]
    public void Defence()
    {
        //Arrange
        var units = Arrange();
        units[0].X = 0;
        units[0].Y = 0;
        units[1].X = 1;
        units[1].Y = 0;
        Player player = new Player(1, 1, 1, "You");
        player.Units.Add(units[0]);
        Player opponent = new Player(1, 1, 1, "Opponent");
        opponent.Units.Add(units[1]);
        var defence = units[1].Defence;
        var health = units[1].Health;
        //Act
        units[0].DoAttack(units[1]);
        //Assert
        if (units[1].Defence == defence - 1 & units[1].Health == health)
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail();
        }
    }
    
    [Test]
    public void Health()
    {
        //Arrange
        var units = Arrange();
        units[0].X = 0;
        units[0].Y = 0;
        units[1].X = 1;
        units[1].Y = 0;
        units[0].AttackDamage = 11;
        Player player = new Player(1, 1, 1, "You");
        player.Units.Add(units[0]);
        Player opponent = new Player(1, 1, 1, "Opponent");
        opponent.Units.Add(units[1]);
        var health = units[1].Health;
        //Act
        units[0].DoAttack(units[1]);
        //Assert
        if (units[1].Defence == 0 & units[1].Health == health - 1)
        {
            Assert.Pass();
        }
        else
        {
            Console.WriteLine(units[1].Defence);
            Assert.Fail();
        }
    }
}