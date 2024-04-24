using LR2.Interfaces;

namespace LR2.Buildings;

public class Tavern: IImprovableBuilding
{
    public string Name { get; } = "t";
    public int WoodToCreate { get; } = 10;
    public int WoodToImprove { get; } = 5;
    public int StoneToCreate { get; } = 15;
    public int StoneToImprove { get; } = 10;
    public int Level { get; set; }
    public void Create(Player player, City city)
    {
        bool flag = true;
        foreach (var building in city.CityBuildings.OfType<Tavern>())
        {
            Console.WriteLine("You can have only 1 tavern");
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
            city.PlaceObject(x, y, new Square(Name, 1, 1, 1, 1));
            Console.WriteLine("1 - improve Movement Range, 2 - lower fines");
            var a = Convert.ToInt16(Console.ReadLine());
            switch (a)
            {
                case 1:
                    foreach (var unit in player.Units)
                    {
                        unit.MovementRange += 1;
                    }
                    break;
                case 2:
                    city.TavernBonus += 0.5;
                    break;
            }
        }
    }

    public void Output()
    {
        Console.WriteLine($"{Name}: level {Level}");
    }

    public void Improve(Player player, City city)
    {
        Console.WriteLine("1 - improve Movement Range, 2 - lower fines");
        var a = Convert.ToInt16(Console.ReadLine());
        switch (a)
        {
            case 1:
                foreach (var unit in player.Units)
                {
                    unit.MovementRange += 1;
                }
                break;
            case 2:
                city.TavernBonus += 0.5;
                break;
        }
        Level += 1;
        Console.WriteLine($"Now Arsenal level is {Level}");
    }
}