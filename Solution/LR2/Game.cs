using LR2.Buildings;
using LR2.Interfaces;

namespace LR2;

public class Game
{
    private const int ContinueGame = 2;

    public static void Start(City city)
    {
        var player = city.Players[0];
        var opponent = city.Players[1];
        var win = Win(player, opponent);
        string outMsg;
        while (win == ContinueGame)
        {
            Console.Clear();
            city.OutputCity();
            Console.WriteLine("Your units: ");
            player.OutputUnits();
            Console.WriteLine("Opponent's units: ");
            opponent.OutputUnits();
            win = Win(player, opponent);
            if (win == ContinueGame)
            {
                PlayersStep(city);
            }

            win = Win(player, opponent);
            if (win == ContinueGame)
            {
                OpponentsStep(city);
            }

            win = Win(player, opponent);
            if (win == ContinueGame)
            {
                if (city.Animals.Count != 0)
                {
                    foreach (var animal in city.Animals)
                    {
                        animal.TakeAStep(city);
                    }
                }
            }

            win = Win(player, opponent);
            city.Players[0] = player;
            city.Players[1] = opponent;
        }

        outMsg = win == 1 ? "Congratulations!" : "Game over :(";
        Console.WriteLine(outMsg);
    }

    public static int Win(Player player, Player opponent)
    {
        return player.Units.Count == 0 ? 0 : opponent.Units.Count == 0 ? 1 : ContinueGame;
    }

    private static void PlayersStep(City city)
    {
        var player = city.Players[0];
        var opponent = city.Players[1];
        IUnit[]? animal;
        foreach (var building in city.CityBuildings)
        {
            if (building is Market)
            {
                var market = (Market)building;
                market.AskToChangeMaterials(player);
            }

            if (building is Handicraft)
            {
                var handicraft = (Handicraft)building;
                handicraft.GiveMoney(player);
            }
        }

        Console.WriteLine("1 - build something, 2 - update building, 3 - skip");
        var a = Convert.ToInt16(Console.ReadLine());
        switch (a)
        {
            case 1:
                AddBuilding(city);
                break;
            case 2:
                ImproveBuilding(city);
                break;
            case 3:
                break;
        }

        city.OutputCity();
        Console.WriteLine("Choose your unit: ");
        var unit = AskForUnit(player.Units);
        var action = AskForAction(city);
        switch (action)
        {
            case 1:
                var direction = AskForDirection();
                unit.Move(direction, city);
                break;
            case 2:
                Console.WriteLine("Choose your opponent's unit: ");
                var opponentsUnit = AskForUnit(opponent.Units);
                AttackUnit(city, unit, player, opponent, opponentsUnit);
                break;
            case 3:
                break;
            case 4:
                animal = player.GetAnimal(city);
                var animal1 = (IAnimal)animal![1];
                animal1.Eat(player);
                break;
        }
    }

    public static void AttackUnit(City city, IUnit unit, Player player, Player opponent, IUnit opponentsUnit)
    {
        var health = opponentsUnit.Health;
        unit.DoAttack(opponentsUnit);
        if (!opponentsUnit.IsAlive())
        {
            Console.WriteLine($"You killed your opponent's {opponentsUnit.Name}");
            Console.WriteLine($"You owned {opponentsUnit.Wood} wood and {opponentsUnit.Stone} stone");
            player.Wood += opponentsUnit.Wood;
            player.Stone += opponentsUnit.Stone;
            opponent.RemoveUnit(opponentsUnit, city);
        }
        else
        {
            if (opponentsUnit.Health < health)
            {
                Console.WriteLine("You owned 1 wood and 1 stone");
                player.Wood += 1;
                player.Stone += 1;
            }
        }
    }

    public static void OpponentsStep(City city)
    {
        var player = city.Players[0];
        var opponent = city.Players[1];
        var victim = opponent.GetVictim(player); // сначала его, потом чужой
        IUnit[]? animal = null;
        if (city.Animals.Count != 0)
        {
            animal = opponent.GetAnimal(city); // сначала юнит, потом животное
        }

        var isAnimalFeeded = false;
        if (animal != null)
        {
            var animal1 = (IAnimal)animal[1];
            if (animal1.Owner == null)
            {
                Console.WriteLine($"Your opponent feeds {animal1.Name}!");
                animal1.Eat(opponent);
                isAnimalFeeded = true;
            }
        }

        if (victim != null & !isAnimalFeeded)
        {
            Console.WriteLine($"Your opponent attacks your {victim?[1].Name} by his {victim?[0].Name}!");
            victim?[0].DoAttack(victim[1]);
            if (!victim![1].IsAlive())
            {
                player.RemoveUnit(victim[1], city);
                Console.WriteLine($"You lose your {victim[1].Name}");
            }
        }
        else if (!isAnimalFeeded)
        {
            Random rnd = new Random();
            var unitId = rnd.Next(0, opponent.Units.Count);
            var x = opponent.Units[unitId].X;
            var y = opponent.Units[unitId].Y;
            var directionAsInt = rnd.Next(0, 3);
            var directionAsString = "";
            switch (directionAsInt)
            {
                case 0:
                    directionAsString = "u";
                    break;
                case 1:
                    directionAsString = "d";
                    break;
                case 2:
                    directionAsString = "l";
                    break;
                case 3:
                    directionAsString = "r";
                    break;
            }

            opponent.Units[unitId].Move(directionAsString, city);
            if (opponent.Units[unitId].X == x & opponent.Units[unitId].Y == y)
            {
                OpponentsStep(city);
            }
            else
            {
                Console.WriteLine($"Your opponent moves his {opponent.Units[unitId].Name}!");
            }
        }

        city.Players[0] = player;
        city.Players[1] = opponent;
    }

    private static string AskForDirection()
    {
        Console.WriteLine("Please, choose the direction: ");
        Console.WriteLine("u - up");
        Console.WriteLine("d - down");
        Console.WriteLine("r - right");
        Console.WriteLine("l - left");
        var answer = Convert.ToString(Console.ReadLine());
        return answer is "u" or "d" or "r" or "l" ? answer : AskForDirection();
    }

    private static int AskForAction(City city)
    {
        var player = city.Players[0];
        var opponent = city.Players[1];
        int answer;
        var victim = player.GetVictim(opponent);
        IUnit[]? animal = null;
        var isAnimalHere = false;
        if (city.Animals.Count != 0)
        {
            animal = player.GetAnimal(city); // сначала юнит, потом животное
            if (animal != null)
            {
                isAnimalHere = true;
            }
        }

        if (isAnimalHere & victim != null & player.Cash > 0)
        {
            Console.WriteLine($"Choose the action: 1 - move, 2 - attack, 3 - skip, 4 - feed {animal?[1].Name}");
            answer = Convert.ToInt32(Console.ReadLine());
            if (answer == 1 || answer == 2 || answer == 3 || answer == 4)
            {
                return answer;
            }
        }

        if (!isAnimalHere & victim != null)
        {
            Console.WriteLine($"Choose the action: 1 - move, 2 - attack, 3 - skip");
            answer = Convert.ToInt32(Console.ReadLine());
            if (answer == 1 || answer == 2 || answer == 3)
            {
                return answer;
            }
        }

        if (isAnimalHere & victim == null & player.Cash > 0)
        {
            Console.WriteLine($"Choose the action: 1 - move, 3 - skip, 4 - feed {animal?[1].Name}");
            answer = Convert.ToInt32(Console.ReadLine());
            if (answer == 1 || answer == 3 || answer == 4)
            {
                return answer;
            }
        }

        if (!isAnimalHere & victim == null)
        {
            Console.WriteLine("Choose the action: 1 - move, 3 - skip");
            answer = Convert.ToInt32(Console.ReadLine());
            if (answer == 1 || answer == 3)
            {
                return answer;
            }
        }

        city.Players[0] = player;
        city.Players[1] = opponent;
        return AskForAction(city);
    }


    private static IUnit AskForUnit(List<IUnit> units)
    {
        foreach (var unit in units)
        {
            Console.WriteLine($"{unit.ShortName}. {unit.Name}");
        }

        var selected = Console.ReadLine();
        foreach (var unit in units.Where(unit => unit.ShortName == selected))
        {
            return unit;
        }

        return AskForUnit(units);
    }

    private static void ImproveBuilding(City city)
    {
        var player = city.Players[0];
        Console.WriteLine("Choose the building: ");
        List<IImprovableBuilding> buildings = [];
        foreach (var building in city.CityBuildings)
        {
            if (building is IImprovableBuilding)
            {
                buildings.Add((IImprovableBuilding)building);
            }
        }

        if (buildings.Count == 0)
        {
            Console.WriteLine("You can't improve anything");
        }
        else
        {
            foreach (var building in buildings)
            {
                building.Output();
            }

            var a = Console.ReadLine();
            if (a == null)
            {
                ImproveBuilding(city);
            }
            else
            {
                var flag = false;
                foreach (var building1 in buildings)
                {
                    if (building1.Designation == a)
                    {
                        flag = true;
                        var buildingAsImprovable = (IImprovableBuilding)building1;
                        if (player.Stone < buildingAsImprovable.StoneToImprove ||
                            player.Wood < buildingAsImprovable.WoodToImprove)
                        {
                            Console.WriteLine("You cant improve this");
                            AddBuilding(city);
                        }
                        else
                        {
                            buildingAsImprovable.Improve(player, city);
                        }
                    }
                }

                if (!flag)
                {
                    ImproveBuilding(city);
                }
            }
        }
    }

    private static void AddBuilding(City city)
    {
        var player = city.Players[0];
        Console.WriteLine("Choose the building: ");
        var buildingsCollection = GetBuildingsCollection();
        OutputList(buildingsCollection);
        var a = Console.ReadLine();
        if (a == null)
        {
            AddBuilding(city);
        }
        else
        {
            IBuilding? building = FindBuilding(a);
            if (building == null)
            {
                AddBuilding(city);
            }
            else
            {
                if (player.Stone < building.StoneToCreate || player.Wood < building.WoodToCreate)
                {
                    Console.WriteLine("You cant build this");
                    AddBuilding(city);
                }
                else
                {
                    building.Create(player, city);
                }
            }
        }
    }

    private static IBuilding? FindBuilding(string s)
    {
        List<IData> buildingsCollection = GetBuildingsCollection();
        foreach (var building in buildingsCollection.Where(building => building.Designation == s))
        {
            return (IBuilding)building;
        }

        return null;
    }

    private static List<IData> GetBuildingsCollection()
    {
        List<IData> buildingsCollection = [];
        buildingsCollection.Add(new Hospital());
        buildingsCollection.Add(new Blacksmith());
        buildingsCollection.Add(new Arsenal());
        buildingsCollection.Add(new Tavern());
        buildingsCollection.Add(new Market());
        buildingsCollection.Add(new Academy());
        buildingsCollection.Add(new Handicraft());
        buildingsCollection.Add(new Alchemist());
        return buildingsCollection;
    }

    public static void OutputList(List<IData> list)
    {
        foreach (var obj in list)
        {
            obj.Output();
        }
    }
}