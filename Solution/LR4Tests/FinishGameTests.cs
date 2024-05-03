using LR2.Units;

namespace LR4Tests;

public class FinishGameTests
{
    private List<Player> Arrange()
    {
        Player player = new Player(1, 1, 1, "You");
        Player opponent = new Player(1, 1, 1, "Opponent");
        var playerUnit = new InfantryUnit("1", 1, 1, 1, 1, 1, 1, 1, 1, "1");
        var opponentUnit = new InfantryUnit("1", 1, 1, 1, 1, 1, 1, 1, 1, "1");
        player.Units.Add(playerUnit);
        opponent.Units.Add(opponentUnit);
        return [player, opponent];
    }
    [Test]
    public void ContinueGameTest()
    {
        //Arrange
        var players = Arrange();
        var player = players[0];
        var opponent = players[1];
        int exceptedResult = 2;
        //Act
        var result = Game.Win(player, opponent);
        //Assert
        if (result == exceptedResult)
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail($"{result}");
        }
    }
    
    [Test]
    public void PlayerWinTest()
    {
        //Arrange
        var players = Arrange();
        var player = players[0];
        var opponent = players[1];
        var opponentUnit = opponent.Units[0];
        int exceptedResult = 1;
        //Act
        opponent.Units.Remove(opponentUnit);
        var result = Game.Win(player, opponent);
        //Assert
        if (result == exceptedResult)
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail($"{result}");
        }
    }
    
    [Test]
    public void OpponentWinTest()
    {
        //Arrange
        var players = Arrange();
        var player = players[0];
        var opponent = players[1];
        var playerUnit = player.Units[0];
        int exceptedResult = 0;
        //Act
        player.Units.Remove(playerUnit);
        var result = Game.Win(player, opponent);
        //Assert
        if (result == exceptedResult)
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail($"{result}");
        }
    }
}