using LR2.Animals;
using LR2.Interfaces;
using LR2.MapProperties;
using LR2.Units;

namespace LR4Tests;

public class FineTests
{
    private bool Assert1(List<double> result, List<double> expectedResult)
    {
        var count = 0;
        var flag = true;
        foreach (var fine in result)
        {
            if (Math.Abs(fine - expectedResult[count]) > 0.01)
            {
                flag = false;
            }
            count += 1;
        }

        return flag;
    }
    [Test]
    public void InfantryTest()
    {
        //Arrange
        var infantryUnit = new InfantryUnit("1", 1, 1, 1, 1, 1, 1, 1, 1, "1");
        var obstacles = City.GetObstacles();
        var expectedResult = new List<Double>{};
        var result = new List<Double>{};
        foreach (var obstacle in obstacles)
        {
            expectedResult.Add(obstacle.InfantryFine);
        }
        //Act
        foreach (var obstacle in obstacles)
        {
            var square = new Square("1", obstacle.InfantryFine, obstacle.HorseFine, obstacle.ArcherFine, obstacle.CatFine);
            result.Add(square.GetFine(infantryUnit));
        }
        //Assert
        var flag = Assert1(result, expectedResult);
        if (flag)
        {
            Assert.Pass();
        }
        else
        {
            foreach (var fine in expectedResult)
            {
                Console.WriteLine(fine);
            }
            foreach (var fine in result)
            {
                Console.WriteLine(fine);
            }
            Assert.Fail();
        }
    }
    
    [Test]
    public void ArcherTest()
    {
        //Arrange
        var archerUnit = new ArcherUnit("1", 1, 1, 1, 1, 1, 1, 1, 1, "1");
        var obstacles = City.GetObstacles();
        var expectedResult = new List<Double>{};
        var result = new List<Double>{};
        foreach (var obstacle in obstacles)
        {
            expectedResult.Add(obstacle.ArcherFine);
        }
        //Act
        foreach (var obstacle in obstacles)
        {
            var square = new Square("1", obstacle.InfantryFine, obstacle.HorseFine, obstacle.ArcherFine, obstacle.CatFine);
            result.Add(square.GetFine(archerUnit));
        }
        //Assert
        var flag = Assert1(result, expectedResult);
        if (flag)
        {
            Assert.Pass();
        }
        else
        {
            foreach (var fine in expectedResult)
            {
                Console.WriteLine(fine);
            }
            foreach (var fine in result)
            {
                Console.WriteLine(fine);
            }
            Assert.Fail();
        }
    }
    [Test]
    public void HorseTest()
    {
        //Arrange
        var horseUnit = new HorseUnit("1", 1, 1, 1, 1, 1, 1, 1, 1, "1");
        var obstacles = City.GetObstacles();
        var expectedResult = new List<Double>{};
        var result = new List<Double>{};
        foreach (var obstacle in obstacles)
        {
            expectedResult.Add(obstacle.HorseFine);
        }
        //Act
        foreach (var obstacle in obstacles)
        {
            var square = new Square("1", obstacle.InfantryFine, obstacle.HorseFine, obstacle.ArcherFine, obstacle.CatFine);
            result.Add(square.GetFine(horseUnit));
        }
        //Assert
        var flag = Assert1(result, expectedResult);
        if (flag)
        {
            Assert.Pass();
        }
        else
        {
            foreach (var fine in expectedResult)
            {
                Console.WriteLine(fine);
            }
            foreach (var fine in result)
            {
                Console.WriteLine(fine);
            }
            Assert.Fail();
        }
    }
    [Test]
    public void CatTest()
    {
        //Arrange
        var catUnit = new Cat("1", 1, 1, 1, 1, 1, 1, 1, 1);
        var obstacles = City.GetObstacles();
        var expectedResult = new List<Double>{};
        var result = new List<Double>{};
        foreach (var obstacle in obstacles)
        {
            expectedResult.Add(obstacle.CatFine);
        }
        //Act
        foreach (var obstacle in obstacles)
        {
            var square = new Square("1", obstacle.InfantryFine, obstacle.HorseFine, obstacle.ArcherFine, obstacle.CatFine);
            result.Add(square.GetFine(catUnit));
        }
        //Assert
        var flag = Assert1(result, expectedResult);
        if (flag)
        {
            Assert.Pass();
        }
        else
        {
            foreach (var fine in expectedResult)
            {
                Console.WriteLine(fine);
            }
            foreach (var fine in result)
            {
                Console.WriteLine(fine);
            }
            Assert.Fail();
        }
    }
}