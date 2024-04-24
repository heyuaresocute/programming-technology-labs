using LR2.Interfaces;

namespace LR2.Buildings;

public class Blacksmith: IImprovableBuilding
{
    public string Name { get; } = "b";
    public int WoodToCreate { get; } = 10;
    public int WoodToImprove { get; } = 5;
    public int StoneToCreate { get; } = 16;
    public int StoneToImprove { get; } = 8;
    public int Level { get; set; }
    public void Create(Player player, City city)
    {
        bool flag = true;
        foreach (var building in city.CityBuildings.OfType<Blacksmith>())
        {
            Console.WriteLine("You can have only 1 blacksmith");
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
            foreach (var unit in player.Units)
            {
                unit.AttackDamage += 1;
            }
        }
    }

    public void Output()
    {
        Console.WriteLine($"{Name}: level {Level}");
    }

    public void Improve(Player player, City city)
    {
        player.Wood -= WoodToImprove;
        player.Stone -= StoneToImprove;
        foreach (var unit in player.Units)
        {
            unit.AttackDamage += 1;
        }
        Level += 1;
        Console.WriteLine($"Now Blacksmith level is {Level}");
    }
}