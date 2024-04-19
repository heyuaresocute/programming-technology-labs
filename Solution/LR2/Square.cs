namespace LR2;

public class Square(string obj)
{
    public string Obj { get; set; } = obj; // * - обычная клетка
                                            // S - Болото
                                            // H - Холм
                                            // T – Дерево
                                            // 1, 2, 3 – Юниты игрока
                                            // a, b, c – Юниты противника
}