using LR2.Interfaces;

namespace LR2.Buildings;

public class Arsenal: IImprovableBuilding
{
    public string Designation { get; set; } = "a";
    public string Name { get; } = "Arsenal";
    public int WoodToCreate { get; } = 3;
    public int WoodToImprove { get; } = 2;
    public int StoneToCreate { get; } = 16;
    public int StoneToImprove { get; } = 8;
    public int Level { get; set; }
    public void Create(Player player, City city)
    {
        bool flag = true;
        foreach (var building in city.CityBuildings.OfType<Arsenal>())
        {
            Console.WriteLine("You can have only 1 arsenal");
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
                unit.Defence += 1;
            }

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
        foreach (var unit in player.Units)
        {
            unit.Defence += 1;
        }
        Level += 1;
        Console.WriteLine($"Now Arsenal level is {Level}");
    }
}