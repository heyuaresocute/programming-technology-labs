using LR2.Animals;
using LR2.Units;

namespace LR4Tests;

public class CatTests
{
    [Test]
    public void BleedTest()
    {
        
            //Arrange
            var cat = new Cat("Timon", 1, 1, 1, 1, 0, 0, 3, 1);
            var unit = new InfantryUnit("1", 100, 1, 1, 0, 1, 10, 1, 1, "1");
            //Act
            cat.DoAttack(unit);
            
            //Assert
            if (unit.Bleed > 0)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        
    }

    [Test]
    public void GetOwnerTest()
    {
        //Arrange
        var cat = new Cat("Timon", 1, 1, 1, 1, 0, 0, 3, 1);
        var player = new Player(100, 10, 10, "You");
        //Act
        cat.Eat(player);
        cat.Eat(player);
        cat.Eat(player);
        //Assert
        if (cat.Owner == player)
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail();
        }
    }
}