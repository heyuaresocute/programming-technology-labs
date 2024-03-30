namespace LR2.Interfaces;

public interface IAnimal
{
    string Name{ get; }
    int Id { get; }
    int Health { get; set; }
    int AttackDanger { get; }
    int AttackRange { get; }
    int MovementRange { get; }
    int Y { get; }
    int X { get; }
    void Move(IUnit unit, string direction, City city);
    void DoAttack(IUnit victim);
    bool IsAlive();
    bool CheckAvailability(IUnit unit);
}