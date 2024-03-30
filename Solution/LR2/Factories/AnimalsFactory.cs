using LR2.Animals;

namespace LR2.Factories;

public class AnimalsFactory
{
    public Cat CreateCat(int x, int y, int id, string name)
    {
        return new Cat($"{name}",id, 10, 3, 1, 3,x, y, 3, 2);
    }
}