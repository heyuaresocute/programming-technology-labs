using LR2.Interfaces;
using Newtonsoft.Json;

namespace LR2.Buildings;

public class Market: IBuilding
{
    public string Designation { get; set; } = "m";
    public string Name { get; } = "Market";
    public int WoodToCreate { get; } = 5;
    public int StoneToCreate { get; } = 5;
    public int X { get; set; }
    public int Y { get; set; }
    public int Level { get; set; } = 0;
    public void Create(Player player, City city)
    {
        bool flag = true;
        foreach (var building in city.CityBuildings.OfType<Market>())
        {
            Console.WriteLine("You can have only 1 market");
            flag = false;
        }
        if (flag)
        {
            Console.WriteLine("Choose the coordinates X Y: ");
            var x = Convert.ToInt32(Console.ReadLine());
            var y = Convert.ToInt32(Console.ReadLine());
            player.Stone -= StoneToCreate;
            player.Wood -= WoodToCreate;
            city.CityBuildings.Add(this);
            city.PlaceObject(X, Y, new Square(Designation, 1, 1, 1, 1));
            city.CityBuildings.Add(this);
            city.PlaceObject(X, Y, new Square(Designation, 1, 1, 1, 1));
            city.OutputCity();
            AskToChangeMaterials(player);
            Level += 1;
        }
    }

    public void AskToChangeMaterials(Player player)
    {
        var course = FindCourse();
        Console.WriteLine($"Course of market: 1 wood to {course[0]} stone/ 1 stone to {course[0]} wood, 1 wood to {course[1]} money, 1 stone to {course[2]} money");
        Console.WriteLine("1 - change materials, 2 - skip");
        var a = Convert.ToInt16(Console.ReadLine());
        if (a == 1)
        {
            Console.WriteLine("1 - wood to stone, 2 - stone to wood, 3 - wood to money, 4 - stone to money: ");
            var b = Convert.ToInt16(Console.ReadLine());
            SwitchMaterials(player, b, course[0], course[1], course[2]);
        }
    }

    private static int[] FindCourse()
    {
        Random rnd = new Random();
        var woodToStone = rnd.Next(1, 5);
        var woodToMoney = rnd.Next(1, 5);
        var stoneToMoney = rnd.Next(1, 5);
        return [woodToStone, woodToMoney, stoneToMoney];
    }

    public void Output()
    {
        Console.WriteLine(Level > 0
            ? $"{Name}: level {Level} - {Designation}"
            : $"{Name}: wood - {WoodToCreate}, stone - {StoneToCreate} - {Designation}");
    }

    public void SwitchMaterials(Player player, int type, int woodToStone, int woodToMoney, int stoneToMoney)
    {
        Console.WriteLine("How many material do you want to change? ");
        var number = Convert.ToInt16(Console.ReadLine());
        switch (type)
        {
            case 1: // wood to stone
                
                if (player.Wood <= number)
                {
                    Console.WriteLine("You don't have enough wood");
                    SwitchMaterials(player, type, woodToStone, woodToMoney, stoneToMoney);
                }
                player.Wood -= number;
                player.Stone += woodToStone * number;
                break;
            case 2: // stone to wood
                if (player.Stone <= number)
                {
                    Console.WriteLine("You don't have enough stone");
                    SwitchMaterials(player, type, woodToStone, woodToMoney, stoneToMoney);
                }
                player.Stone -= number;
                player.Wood += woodToStone * number;
                break;
            case 3:
                if (player.Wood <= number)
                {
                    Console.WriteLine("You don't have enough wood");
                    SwitchMaterials(player, type, woodToStone, woodToMoney, stoneToMoney);
                }
                player.Wood -= number;
                player.Cash += woodToMoney * number;
                break;
            case 4:
                if (player.Stone <= number)
                {
                    Console.WriteLine("You don't have enough stone");
                    SwitchMaterials(player, type, woodToStone, woodToMoney, stoneToMoney);
                }
                player.Stone -= number;
                player.Cash += stoneToMoney * number;
                break;
        }
    }
}