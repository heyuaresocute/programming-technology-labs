namespace LR2.Interfaces;

public interface IBuilding
{
    string Name { get;  }
    int WoodToCreate { get; }
    int WoodToImprove { get; }
    int StoneToCreate { get; }
    int StoneToImprove { get; }
    int Level {get; set; }
    void Create(Player player, City city);
    void Output();
}