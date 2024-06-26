namespace LR2.Interfaces;

public interface IUnit
{
    string Name{ get; }
    string ShortName { get; set; }
    int Health { get; set; }
    int AttackDamage { get; set; }
    int AttackRange { get; set; }
    int Defence { get; set; }
    int MovementRange { get; set; }
    int Y { get; }
    int X { get; }
    int Cost { get; set; } 
    int Bleed { get; set; }
    int Stone { get; set; }
    int Wood { get; set; }
    void Move(string direction, City city);
    void DoAttack(IUnit victim);
    bool IsAlive();
    bool CheckAvailability(IUnit unit);
}