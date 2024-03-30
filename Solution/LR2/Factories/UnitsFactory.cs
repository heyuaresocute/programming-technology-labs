using LR2.Units;

namespace LR2.Factories;

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
    
    public HorseUnit CreateBrassiere(int x, int y, int id)
    {
        return new HorseUnit("Brassiere", 50, 2, 1, 7, 5, 23, x , y, id);
    }
    
    public HorseUnit CreatePaladine(int x, int y, int id)
    {
        return new HorseUnit("Kotik", 50, 20, 20, 7, 20, 23, x , y, id);
    }
    
    //TODO Проходя мимо леса (находясь на соседней клетке) с каким-то шансом из леса выбежит котик. Спавнится в округе 1 клетка от леса. (Если 2 леса рядом - спавнить рядом). Все союзные юниты получают +1 ко всем характеристикам. Добавить действие - кормить кота. Кормим деньгами. После трех кормешек кот становится боевым юнитом. С характеристиками начинает кусаться. Делает рваную рану - периодиоческий урон. -2hp в течение трех ходов.
    //TODO Кот проходит деревья без штрафа. Все остальное - проблема.
}
