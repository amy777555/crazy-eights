using CrazyEights.Cards;
using CrazyEights.CardDeck;
using CrazyEights.Domain;
using CrazyEights.Player;

namespace CrazyEights.Game;

// This class coordinates a game of Crazy Eights with two players
// it deals cards, runs the game loop, tracks which suit is active, 
// ... and determines the winner

// Game Rules:
// - Players begin with 5 cards
// - The top discard and the active suit determine if a card is playable
// - when a non wildcard is played, the cards suit is now the active suit
// - when the deck is empty, the player with the fewest cards wins
public class CrazyEightsGame
{
    private readonly Deck _deck;
    private readonly DiscardPile _discardPile;
    private readonly List<IPlayer> _players;

    // Current active suit
    private Suit _suitToMatch;

    // Creates a new game with a deck and two players
    public CrazyEightsGame(Deck deck, IPlayer player1, IPlayer player2)
    {
        _deck = deck;
        _discardPile = new DiscardPile();
        _players = new List<IPlayer> {player1, player2};
    }

    // Runs the game until a winner is determined
    public void Run()
    {
        StartGame();

        int currentIndex = 0;
        int round = 1;

        while (true)
        {
            IPlayer currentPlayer = _players[currentIndex];

            TurnHeader(round);

            TurnContext context = new TurnContext(
                topDiscard: _discardPile.Top,
                suitToMatch: _suitToMatch,
                roundNumber: round,
                drawCard: () => _deck.Draw(),
                playToDiscard: card =>
                {
                    _discardPile.Push(card);

                    // Sets the suit to the played cards suit if not a wildcard
                    if (!card.IsWildcard)
                        _suitToMatch = card.Suit;
                },
                setSuit: suit => _suitToMatch = suit
            );

            currentPlayer.TakeTurn(context);

            if (currentPlayer.CardCount == 0)
            {
                WinByEmptyHand(currentPlayer);
                return;
            }
            
            if (_deck.Count == 0)
            {
                WinByFewestCards();
                return;
            }


            currentIndex = (currentIndex + 1) % _players.Count;
            round++;
        }
    }

    private void StartGame()
    {

        // Deals five cards to each player
        for (int i = 0; i < 5; i++)
        {
            foreach (IPlayer player in _players)
            {
                ICard card = _deck.Draw();
                player.DrawCard(card);
            }
        }

        // Sets the discard pile up with its first card
        // Sets the active suit to the top discard's suit
        ICard firstDiscard = _deck.Draw();
        _discardPile.Push(firstDiscard);
        _suitToMatch = firstDiscard.Suit;

        Console.WriteLine();
        Console.WriteLine("₊˚ ✧ ‿︵‿‿︵‿୨୧‿︵‿‿︵‿ ✧ ₊˚");
        Console.WriteLine("       Crazy Eights        ");
        Console.WriteLine("₊˚ ✧ ‿︵‿︵‿︵‿︵‿︵‿︵‿ ✧ ₊˚");
        Console.WriteLine();
    }

    private void TurnHeader(int round)
    {
        Console.WriteLine();
        Console.WriteLine("‿︵‿︵‿ Turn " + round + " ‿︵‿︵‿");
        Console.WriteLine("Top discard: " + _discardPile.Top.Display() + " | Suit to Match: " + _suitToMatch);
        Console.WriteLine("Deck remaining: " + _deck.Count + " cards");
        Console.WriteLine(_players[0].Name + ": " + _players[0].CardCount + " cards" + " | " +
                          _players[1].Name + ": " + _players[1].CardCount + " cards");
    }

    private void WinByEmptyHand(IPlayer winner)
    {
        Console.WriteLine();
        Console.WriteLine("‿︵‿︵‿ " + winner.Name + " wins! ‿︵‿︵‿");
    }

    private void WinByFewestCards()
    {
        int min = _players.Min(players => players.CardCount);
        List<IPlayer> winners = _players.Where(players => players.CardCount == min).ToList();

        Console.WriteLine();
        Console.WriteLine("Deck is empty.");

        if (winners.Count == 1)
        {
            Console.WriteLine("‿︵‿︵‿ " + winners[0].Name + " wins by fewest cards! ‿︵‿︵‿");
        }

        else
        {
            Console.WriteLine("‿︵‿︵‿ " + winners[0].Name + " and " + winners[1].Name + " tied! ‿︵‿︵‿");
        }
    }
}