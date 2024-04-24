namespace LR2.Interfaces;

public interface IImprovableBuilding: IBuilding
{
    void Improve(Player player, City city);
}