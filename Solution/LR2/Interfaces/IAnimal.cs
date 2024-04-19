namespace LR2.Interfaces;

public interface IAnimal : IUnit
{
    Player? Owner { get; }
    void TakeAStep(City city);
    void Eat(Player player);
    bool CheckAvailabilityOfFeeder(IUnit feeder);
}