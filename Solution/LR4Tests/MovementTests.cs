using LR2.Interfaces;
using LR2.MapProperties;
using LR2.Units;

namespace LR4Tests;

public class MovementTests
{
    [Test]
    public void CollisionTest()
    {
        //Arrange
        City city = new City(100, new Map("1", 10, 10));
        city.CityObjects = City.GenerateMatrix(10, 10);
        city.CityObjects[5][5] = new Square("T", 1, 1,1,1);
        var unit1 = new InfantryUnit("1", 1, 1, 1, 1, 1, 1, 4, 5, "1");
        city.PlaceObject(unit1.X, unit1.Y, new Square(unit1.ShortName, 1, 1, 1, 1));
        int[] expectedResult = { 5, 4 };
        //Act
        unit1.Move("u", city);
        int[] result = { unit1.X, unit1.Y };
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
}