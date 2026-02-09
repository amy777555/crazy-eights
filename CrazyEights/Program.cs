using CrazyEights.CardDeck;
using CrazyEights.Game;
using CrazyEights.Player;

namespace CrazyEights;

// Executes a game of crazy eights
// Gathers necessary user input
public static class Program
{
    public static void Main(string[] args)
    {
        Console.Write("Enter your name (or press enter for 'Player'): ");
        string? nameInput = Console.ReadLine();

        string name = "Player";
        if (!string.IsNullOrWhiteSpace(nameInput))
            name = nameInput.Trim();
        
        // Creates to players
        IPlayer human = new HumanPlayer(name);
        IPlayer cpu = new CpuPlayer("CPU");

        // Creates a shuffled deck of cards
        Deck deck = Deck.Shuffle();
        
        // Creates a new game using two players and a deck of cards
        CrazyEightsGame game = new CrazyEightsGame(deck, human, cpu);
        
        game.Run();
    }
}