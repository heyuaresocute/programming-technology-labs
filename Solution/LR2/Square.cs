using LR2.Animals;
using LR2.Interfaces;
using LR2.Units;

namespace LR2;

public class Square(string obj, double fineForInfantry, double fineForHorse, double fineForArcher, double fineForCat)
{
    public string Obj { get; set; } = obj;
    public double FineForInfantry { get; } = fineForInfantry;
    public double FineForHorse { get; } = fineForHorse;
    public double FineForArcher { get; } = fineForArcher;
    public double FineForCat { get; } = fineForCat;
    
    public double GetFine(IUnit unit)
    {
        switch (unit)
        {
            case InfantryUnit:
                return FineForInfantry;
            case ArcherUnit:
                return FineForArcher;
            case HorseUnit:
                return FineForHorse;
            case Cat:
                return FineForCat;
            default:
                return 1;
        }
    }                                        
}