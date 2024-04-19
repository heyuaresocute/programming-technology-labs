using LR2.Factories;
using LR2.Interfaces;

namespace LR2
{
    internal static class Lab
    {
        private const int ContinueGame = 2;

        private static void Main()
        {
            var swampsNumber = 0;
            var hillsNumber = 0;
            var treesNumber = 0;
            var startcash = 0;
            var catChance = 20;

            Console.WriteLine("Before you start, please, enter the size of your city (two numbers - columns and rows)");
            var cols = Convert.ToInt32(Console.ReadLine());
            var rows = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Also, please, choose the difficulty (1 - easy, 2 - normal, 3 - hard)");
            var x = Convert.ToInt32(Console.ReadLine());
            switch (x)
            {
                case 1:
                    swampsNumber = 3;
                    hillsNumber = 1;
                    treesNumber = 4;
                    startcash = 69;
                    break;
                case 2:
                    swampsNumber = 5;
                    hillsNumber = 4;
                    treesNumber = 6;
                    startcash = 55;
                    break;
                case 3:
                    swampsNumber = 6;
                    hillsNumber = 5;
                    treesNumber = 7;
                    startcash = 48;
                    break;
            }

            var city = new City(cols, rows, swampsNumber, hillsNumber, treesNumber, catChance);
            city.GenerateCity();
            var player = new Player(startcash, "You");
            var opponent = new Player(startcash, "Opponent");
            city.Players.Add(player);
            city.Players.Add(opponent);
            var unitsFactory = new UnitsFactory(city);
            Console.WriteLine($"Your start cash is {startcash}. Choose your units (select 3): ");
            player.SelectUnits(unitsFactory, city);
            opponent.SelectUnits(unitsFactory, city);
            Start(city);
        }

        private static void Start(City city)
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

        private static void PlayersStep(City city)
        {
            var player = city.Players[0];
            var opponent = city.Players[1];
            IUnit[]? animal = null;
            if (city.Animals.Count != 0)
            {
                animal = opponent.GetAnimal(city); // сначала юнит, потом животное
            }
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
                    unit.DoAttack(opponentsUnit);
                    if (!opponentsUnit.IsAlive())
                    {
                        Console.WriteLine($"You killed your opponent's {opponentsUnit.Name}");
                        opponent.RemoveUnit(opponentsUnit, city);
                    }
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

        private static void OpponentsStep(City city)
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

        private static int Win(Player player, Player opponent)
        {
            return player.Units.Count == 0 ? 0 : opponent.Units.Count == 0 ? 1 : ContinueGame;
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
            if (!isAnimalHere & victim != null )
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
    }
}