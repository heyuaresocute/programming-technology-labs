namespace LR2;

public class HorseUnit(string name, int health, int attackNumber, int attackRange, int defence, int movementRange, int cost, int y, int x, int id): IUnit
{
    public string Name { get; } = name;
    public int Id { get; } = id;
    public int Health { get; set; } = health;
    public int AttackNumber { get; } = attackNumber;
    public int AttackRange { get; } = attackRange;
    public int Defence { get; set; } = defence;
    public int MovementRange { get; } = movementRange;
    public int YСoordinate { get; set; } = y;
    public int XСoordinate { get; set; } = x;
    public int Cost { get; } = cost;

    public void DoAttack()
    {
        
    }
    
    public int IsAlive()
    {
        if (Health == 0)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }
}