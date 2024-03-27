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
            player.SelectUnits(factory);
            opponent.OpponentSelectUnits(factory);
            Start(player, opponent, city);
        }

        private static void Start(Player player, Player opponent, City city)
        {
            
            city.GenerateCity();
            player.PlaceUnits(city);
            opponent.PlaceUnits(city);
            var win = Win(player, opponent);
            while (win == 2)
            {
                city.OutputCity();
                Console.WriteLine("Your units: ");
                player.OutputUnits();
                Console.WriteLine("Opponent's units: ");
                opponent.OutputUnits();
                var action = AskForAction();
                var unit = AskForUnit(player.Units);
                switch (action)
                {
                    case 1:
                        var direction = AskForDirection();
                        player.Move(unit, direction, city);
                        break;
                }

                win = Win(player, opponent);
            }

            var outMsg = win==1? "Congratulations":"Game over :(";
            Console.WriteLine(outMsg);
            

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
            if (answer == "u" || answer == "d" || answer == "r" || answer == "l"  )
            {
                return answer;
            }
            return AskForDirection();
        }
        
        private static int AskForAction()
        {
            Console.WriteLine("Choose the action: 1 - move, 2 - attack");
            var answer = Convert.ToInt32(Console.ReadLine());
            if (answer == 1 || answer == 2  )
            {
                return answer;
            }
            return AskForAction();
        }

        private static IUnit AskForUnit(List<IUnit> units)
        {
            Console.WriteLine("Choose your unit: ");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"{units[i].Id}. {units[i].Name}");
            }

            var selected = Convert.ToInt32(Console.ReadLine());
            if (selected == 1 || selected == 2 || selected == 3)
            {
                switch (selected)
                {
                    case 1:
                        return units[0];
                    case 2:
                        return units[1];
                    case 3:
                        return units[2];
                }
            }
            return AskForUnit(units);
        }
    }
}