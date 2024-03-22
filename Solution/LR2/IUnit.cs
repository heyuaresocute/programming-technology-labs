namespace LR2;

public interface IUnit
{
    string Name{ get; }
    int Id { get; }
    int Health { get; set; }
    int AttackNumber { get; }
    int AttackRange { get; }
    int Defence { get; set; }
    int MovementRange { get; }
    int YСoordinate { get; set; }
    int XСoordinate { get; set; }
    int Cost { get; }
    void DoAttack();
    int IsAlive();
}

public class UnitsFactory
{
    public InfantryUnit CreateSwordsman(int x, int y, int id)
    {
        return new InfantryUnit("Swordsman",50, 5, 1, 8, 3, 10, x , y, id);
    }

    public InfantryUnit CreateSpearman(int x, int y, int id)
    {
        return new InfantryUnit("Spearman",35, 3, 1, 4, 6, 15, x , y, id);
    }

    public InfantryUnit CreateAxeman(int x, int y, int id)
    {
        return new InfantryUnit("Axeman",45, 9, 1, 3, 4, 20, x , y, id);
    }
    
    public ArcherUnit CreateLongBow(int x, int y, int id)
    {
        return new ArcherUnit("Longbow",30, 6, 5, 8, 2, 15, x , y, id);
    }
    
    public ArcherUnit CreateShortBow(int x, int y, int id)
    {
        return new ArcherUnit("Shortbow", 25, 3, 3, 4, 4, 19, x , y, id);
    }
    
    public ArcherUnit CreateCrossBow(int x, int y, int id)
    {
        return new ArcherUnit("Crossbow", 40, 7, 6, 3, 2, 23, x , y, id);
    }
    
    public ArcherUnit CreateHorseBow(int x, int y, int id)
    {
        return new ArcherUnit("HorseBow", 25, 3, 3, 2, 5, 25, x , y, id);
    }
    
    public HorseUnit CreateKnight(int x, int y, int id)
    {
        return new HorseUnit("Knight", 30, 5, 1, 3, 6, 20, x , y, id);
    }
    
    public HorseUnit CreateСuirassier(int x, int y, int id)
    {
        return new HorseUnit("Сuirassier", 50, 2, 1, 7, 5, 23, x , y, id);
    }
    
    
}
