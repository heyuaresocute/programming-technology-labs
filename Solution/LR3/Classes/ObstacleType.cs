using LR3.Interfaces;

namespace LR3.Classes;

public class ObstacleType(string designation, double infantryFine, double archerFine, double horseFine, double catFine, int standartCount): IData
{
    public string Designation { get; set; } = designation;
    public double InfantryFine { get; set; } = infantryFine;
    public double ArcherFine { get; set; } = archerFine;
    public double HorseFine { get; set; } = horseFine;
    public double CatFine { get; set; } = catFine;
    public int StandartCount { get; set; } = standartCount;
    
    public void Output()
    {
        Console.WriteLine($"Designation: {Designation}, InfantryFine: {InfantryFine}, ArcherFine: {ArcherFine}, HorseFine: {HorseFine}, CatFine: {CatFine}, StandartCount: {StandartCount}");
    }
}