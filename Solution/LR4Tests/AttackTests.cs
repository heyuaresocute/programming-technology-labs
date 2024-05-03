using LR2.Units;

namespace LR4Tests;

public class AttackTests
{
    private List<InfantryUnit> Arrange()
    {
        var unit1 = new InfantryUnit("1", 1, 1, 1, 1, 1, 1, 1, 1, "1");
        var unit2 = new InfantryUnit("1", 1, 1, 1, 1, 1, 1, 1, 1, "1");
        return [unit1, unit2];
    }
    [Test]
    public void Attack()
    {
        //Arrange
        var units = Arrange();
        units[0].X = 0;
        units[0].Y = 0;
        units[1].X = 9;
        units[1].Y = 9;
        var summ = units[1].Health + units[1].Defence;
        var result = true;
        //Act
        units[0].DoAttack(units[1]);
        if (units[1].Health + units[1].Defence == summ)
        {
            result = false;
        }
        //Assert
        if (result == false)
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail();
        }
    }
}