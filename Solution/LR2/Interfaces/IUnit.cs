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
    void Move(IUnit unit, string direction, City city);
    void DoAttack(IUnit victim);
    bool IsAlive();
    bool CheckAvailability(IUnit unit);
}