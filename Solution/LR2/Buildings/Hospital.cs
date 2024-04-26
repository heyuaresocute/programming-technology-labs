using LR2.Interfaces;

namespace LR2.Buildings;

public class Hospital: IImprovableBuilding
{
    public string Designation { get; set; } = "h";
    public string Name { get; } = "Hospital";
    public int StoneToImprove { get; } = 5;
    public int Level { get; set; } = 0;
    public int WoodToCreate { get; } = 10;
    public int WoodToImprove { get; } = 5;
    public int StoneToCreate { get; } = 10;
    public void Create(Player player, City city)
    {
        bool flag = true;
        foreach (var building in city.CityBuildings.OfType<Hospital>())
        {
            Console.WriteLine("You can have only 1 hospital");
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
            city.PlaceObject(x, y, new Square(Designation, 1, 1, 1, 1));
            foreach (var unit in player.Units)
            {
                unit.Health += 1;
            }

            Level += 1;
        }
    }

    public void Output()
    {
        if (Level > 0)
        {
            Console.WriteLine($"{Name}: level {Level}, wood - {WoodToImprove}, stone - {StoneToImprove} - {Designation}");
        }
        else
        {
            Console.WriteLine($"{Name}: wood - {WoodToCreate}, stone - {StoneToCreate} - {Designation}");
        }
    }

    public void Improve(Player player, City city)
    {
        player.Wood -= WoodToImprove;
        player.Stone -= StoneToImprove;
        foreach (var unit in player.Units)
        {
            unit.Health += 1;
        }
        Level += 1;
        Console.WriteLine($"Now Hospital level is {Level}");
    }
}