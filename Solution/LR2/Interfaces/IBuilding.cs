namespace LR2.Interfaces;

public interface IBuilding: IData
{
    new string Designation { get;  }
    string Name { get;  }
    int WoodToCreate { get; }
    int StoneToCreate { get; }
    int Level {get; set; }
    void Create(Player player, City city);
    new void Output();
}