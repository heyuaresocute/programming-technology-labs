using LR2.Animals;
using LR2.Units;

namespace LR2.Factories;

public class UnitsFactory(City city)
{
    public InfantryUnit CreateSwordsman(int x, int y, string id)
    {
        var unit = new InfantryUnit("Swordsman",50, 5, 1, 8, 3, 10, x , y, id);
        city.PlaceObject(unit.X, unit.Y, new Square(unit.ShortName, 1, 1, 1, 1));
        return unit;
    }

    public InfantryUnit CreateSpearman(int x, int y, string id)
    {
        var unit = new InfantryUnit("Spearman",35, 3, 1, 4, 6, 15, x , y, id);
        city.PlaceObject(unit.X, unit.Y, new Square(unit.ShortName, 1, 1, 1, 1));
        return unit;
    }

    public InfantryUnit CreateAxeman(int x, int y, string id)
    {
        var unit = new InfantryUnit("Axeman",45, 9, 1, 3, 4, 20, x , y, id);
        city.PlaceObject(unit.X, unit.Y, new Square(unit.ShortName, 1, 1, 1, 1));
        return unit;
    }

    public InfantryUnit CreateNewInfantry(int x, int y, string name, short health, short attackDamage, short attackRange,
        short defence, short movementRange, int cost)
    {
        var unit = new InfantryUnit(name, health, attackDamage, attackRange, defence, movementRange, cost, x, y, "4");
        city.PlaceObject(unit.X, unit.Y, new Square(unit.ShortName, 1, 1, 1, 1));
        return unit;
    }
    
    public ArcherUnit CreateLongBow(int x, int y, string id)
    {
        var unit = new ArcherUnit("Longbow",30, 6, 5, 8, 2, 15, x , y, id);
        city.PlaceObject(unit.X, unit.Y, new Square(unit.ShortName, 1, 1, 1, 1));
        return unit;
    }
    
    public ArcherUnit CreateShortBow(int x, int y, string id)
    {
        var unit = new ArcherUnit("Shortbow", 25, 3, 3, 4, 4, 19, x , y, id);
        city.PlaceObject(unit.X, unit.Y, new Square(unit.ShortName, 1, 1, 1, 1));
        return unit;
    }
    
    public ArcherUnit CreateCrossBow(int x, int y, string id)
    {
        var unit = new ArcherUnit("Crossbow", 40, 7, 6, 3, 2, 23, x , y, id);
        city.PlaceObject(unit.X, unit.Y, new Square(unit.ShortName, 1, 1, 1, 1));
        return unit;
    }
    
    public ArcherUnit CreateHorseBow(int x, int y, string id)
    {
        var unit = new ArcherUnit("HorseBow", 25, 3, 3, 2, 5, 25, x , y, id);
        city.PlaceObject(unit.X, unit.Y, new Square(unit.ShortName, 1, 1, 1, 1));
        return unit;
    }
    
    public ArcherUnit CreateNewArcher(int x, int y, string name, short health, short attackDamage, short attackRange,
        short defence, short movementRange, int cost)
    {
        var unit = new ArcherUnit(name, health, attackDamage, attackRange, defence, movementRange, cost, x, y, "4");
        city.PlaceObject(unit.X, unit.Y, new Square(unit.ShortName, 1, 1, 1, 1));
        return unit;
    }
    
    public HorseUnit CreateKnight(int x, int y, string id)
    {
        var unit = new HorseUnit("Knight", 30, 5, 1, 3, 6, 20, x , y, id);
        city.PlaceObject(unit.X, unit.Y, new Square(unit.ShortName, 1, 1, 1, 1));
        return unit;
    }
    
    public HorseUnit CreateBrassiere(int x, int y, string id)
    {
        var unit = new HorseUnit("Brassiere", 50, 2, 1, 7, 5, 23, x , y, id);
        city.PlaceObject(unit.X, unit.Y, new Square(unit.ShortName, 1, 1, 1, 1));
        return unit;
    }
    
    public HorseUnit CreatePaladine(int x, int y, string id)
    {
        var unit = new HorseUnit("Paladine", 50, 20, 20, 7, 20, 23, x , y, id);
        city.PlaceObject(unit.X, unit.Y, new Square(unit.ShortName, 1, 1, 1, 1));
        return unit;
    }
    
    public HorseUnit CreateNewHorse(int x, int y, string name, short health, short attackDamage, short attackRange,
        short defence, short movementRange, int cost)
    {
        var unit = new HorseUnit(name, health, attackDamage, attackRange, defence, movementRange, cost, x, y, "4");
        city.PlaceObject(unit.X, unit.Y, new Square(unit.ShortName, 1, 1, 1, 1));
        return unit;
    }
    
    public Cat CreateCat(int x, int y)
    {
        var cat = new Cat("Cat", 10, 3, 1, 1,x, y, 3, 2);
        city.Animals.Add(cat);
        Console.WriteLine("Cat has appeared!");
        city.PlaceObject(cat.X, cat.Y, new Square(cat.ShortName, 1, 1, 1, 1));
        return cat;
    }
    
    //TODO Проходя мимо леса (находясь на соседней клетке) с каким-то шансом из леса выбежит котик. Спавнится в округе 1 клетка от леса. (Если 2 леса рядом - спавнить рядом). Все союзные юниты получают +1 ко всем характеристикам. Добавить действие - кормить кота. Кормим деньгами. После трех кормешек кот становится боевым юнитом. С характеристиками начинает кусаться. Делает рваную рану - периодиоческий урон. -2hp в течение трех ходов.
    //TODO Кот проходит деревья без штрафа. Все остальное - проблема.
}
