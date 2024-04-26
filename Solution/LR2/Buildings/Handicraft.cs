using LR2.Interfaces;

namespace LR2.Buildings;

public class Handicraft: IImprovableBuilding
{
    public string Designation { get; set; } = "r";
    public string Name { get; } = "Handicraft";
    public int WoodToCreate { get; } = 10;
    public int WoodToImprove { get; } = 5;
    public int StoneToCreate { get; } = 16;
    public int StoneToImprove { get; } = 8;
    public int Level { get; set; }

    public const int Cash = 10;
    public void Create(Player player, City city)
    {
        bool flag = true;
        var count = 0;
        foreach (var building in city.CityBuildings.OfType<Handicraft>())
        {
            count += 1;
        }

        if (count >= 4)
        {
            Console.WriteLine("You can have only 4 hadicrafts");
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

            Level += 1;
        }
    }

    public void Output()
    {
        Console.WriteLine(Level > 0
            ? $"{Name}: level {Level}, wood - {WoodToImprove}, stone - {StoneToImprove} - {Designation}"
            : $"{Name}: wood - {WoodToCreate}, stone - {StoneToCreate} - {Designation}");
    }

    public void Improve(Player player, City city)
    {
        player.Wood -= WoodToImprove;
        player.Stone -= StoneToImprove;
        Level += 1;
        Console.WriteLine($"Now your handicraft has {Level} crafters");
    }

    public void GiveMoney(Player player)
    {
        player.Cash += Cash * Level;
    }
}