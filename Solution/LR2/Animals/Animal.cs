using LR2.Interfaces;

namespace LR2.Animals;

public class Cat(string name, int id, int health, int attackNumber, int attackRange, int movementRange, int x, int y, int bleedingTime, int bleedingDamage): IAnimal
{
    public string Name { get; } = name;
    public int Id { get; } = id;
    public int Health { get; set; } = health;
    public int AttackDanger { get; } = attackNumber;
    public int AttackRange { get; } = attackRange;
    public int BleedingTime { get; } = bleedingTime;
    public int BleedingDamage { get; } = bleedingDamage;
    public int MovementRange { get; } = movementRange;
    public int Y { get; } = y;
    public int X { get; } = x;
    
    public void Move(IUnit unit, string direction, City city)
    {
        throw new NotImplementedException();
    }

    public void DoAttack(IUnit victim)
    {
        throw new NotImplementedException();
    }

    public bool IsAlive()
    {
        throw new NotImplementedException();
    }

    public bool CheckAvailability(IUnit unit)
    {
        throw new NotImplementedException();
    }
}