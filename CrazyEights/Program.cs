using CrazyEights.CardDeck;
using CrazyEights.Game;
using CrazyEights.Player;

namespace CrazyEights;

public static class Program
{
    public static void Main(string[] args)
    {
        Console.Write("Enter your name (or press enter for 'Player'): ");
        string? nameInput = Console.ReadLine();

        string name = "Player";
        if (!string.IsNullOrWhiteSpace(nameInput))
            name = nameInput.Trim();
        
        IPlayer human = new HumanPlayer(name);
        IPlayer cpu = new CpuPlayer("CPU");

        Deck deck = Deck.Shuffle();
        
        CrazyEightsGame game = new CrazyEightsGame(deck, human, cpu);
        
        game.Run();
    }
}