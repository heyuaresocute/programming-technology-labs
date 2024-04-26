namespace LR2.Interfaces;

public interface IImprovableBuilding: IBuilding
{
    int WoodToImprove { get; }
    int StoneToImprove { get; }
    void Improve(Player player, City city);
}