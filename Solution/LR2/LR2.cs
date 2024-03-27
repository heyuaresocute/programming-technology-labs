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

            var city = new City(cols, rows, swampsNumber, hillsNumber, treesNumber);
            var player = new Player(startcash);
            var opponent = new Player(startcash);
            var factory = new UnitsFactory();
            Console.WriteLine($"Your start cash is {startcash}. Choose your units (select 3): ");
            player.SelectUnits(factory, city);
            opponent.OpponentSelectUnits(factory);
            Start(player, opponent, city);
        }

        private static void Start(Player player, Player opponent, City city)
        {
            city.GenerateCity();
            player.PlaceUnits(city);
            opponent.PlaceUnits(city);
            var win = Win(player, opponent);
            var unit = player.Units[0];
            while (win == ContinueGame)
            {
                city.OutputCity();
                Console.WriteLine("Your units: ");
                player.OutputUnits();
                Console.WriteLine("Opponent's units: ");
                opponent.OutputUnits();
                var action = AskForAction();
                switch (action)
                {
                    case 1:
                        Console.WriteLine("Choose your unit: ");
                        unit = AskForUnit(player.Units);
                        var direction = AskForDirection();
                        unit.Move(unit, direction, city);
                        break;
                    case 2:
                        Console.WriteLine("Choose your unit: ");
                        unit = AskForUnit(player.Units);
                        Console.WriteLine("Choose your opponent's unit: ");
                        var opponentsUnit = AskForUnit(opponent.Units);
                        unit.DoAttack(opponentsUnit);
                        if (!opponentsUnit.IsAlive())
                        {
                            opponent.RemoveUnit(opponentsUnit, city);
                        }
                        break;
                    case 3:
                        break;
                }

                OpponentsStep(opponent, player, city);

                win = Win(player, opponent);
            }

            var outMsg = win == 1 ? "Congratulations" : "Game over :(";
            Console.WriteLine(outMsg);
        }

        private static void OpponentsStep(Player opponent, Player player, City city)
        {
            var victim = opponent.GetVictim(player); // сначала его, потом чужой
            if (victim != null)
            {
                Console.WriteLine($"Your opponent attacks your {victim[1].Name} by his {victim[0].Name}!");
                victim[0].DoAttack(victim[1]);
                if (!victim[1].IsAlive())
                {
                    opponent.RemoveUnit(victim[1], city);
                }
            }
            else
            {
                Random rnd = new Random();
                var unitId = rnd.Next(0, 2);
                var x = opponent.Units[unitId].XСoordinate;
                var y = opponent.Units[unitId].YСoordinate;
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
                opponent.Units[unitId].Move(opponent.Units[unitId], directionAsString, city);
                if (opponent.Units[unitId].XСoordinate == x & opponent.Units[unitId].YСoordinate == y)
                {
                    OpponentsStep(opponent, player, city);
                }
                else
                {
                    Console.WriteLine($"Your opponent moves his {opponent.Units[unitId].Name}!");
                }
            }
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
            if (answer == "u" || answer == "d" || answer == "r" || answer == "l")
            {
                return answer;
            }

            return AskForDirection();
        }

        private static int AskForAction()
        {
            Console.WriteLine("Choose the action: 1 - move, 2 - attack, 3 - skip");
            var answer = Convert.ToInt32(Console.ReadLine());
            if (answer == 1 || answer == 2 || answer == 3)
            {
                return answer;
            }

            return AskForAction();
        }

        private static IUnit AskForUnit(List<IUnit> units)
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"{units[i].Id}. {units[i].Name}");
            }

            var selected = Convert.ToInt32(Console.ReadLine());
            switch (selected)
            {
                case 1:
                    return units[0];
                case 2:
                    return units[1];
                case 3:
                    return units[2];
                case 7:
                    return units[0];
                case 8:
                    return units[1];
                case 9:
                    return units[2];
            }

            return AskForUnit(units);
        }
    }
}