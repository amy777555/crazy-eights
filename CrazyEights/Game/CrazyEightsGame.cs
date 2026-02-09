using CrazyEights.Cards;
using CrazyEights.CardDeck;
using CrazyEights.Domain;
using CrazyEights.Player;

namespace CrazyEights.Game;

public class CrazyEightsGame
{
    private readonly Deck _deck;
    private readonly DiscardPile _discardPile;
    private readonly List<IPlayer> _players;

    private Suit _suitToMatch;

    public CrazyEightsGame(Deck deck, IPlayer player1, IPlayer player2)
    {
        _deck = deck;
        _discardPile = new DiscardPile();
        _players = new List<IPlayer> {player1, player2};
    }

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

        for (int i = 0; i < 5; i++)
        {
            foreach (IPlayer player in _players)
            {
                ICard card = _deck.Draw();
                player.DrawCard(card);
            }
        }

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