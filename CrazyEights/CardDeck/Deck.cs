using CrazyEights.Cards;
using CrazyEights.Domain;

namespace CrazyEights.CardDeck;

// This class represents a deck of cards that can only be altered by drawing
// Cards are stored as a stack
public class Deck
{
    private readonly Stack<ICard> _cards;

    private Deck(IEnumerable<ICard> cards)
    {
        // Creates a deck of cards from an existing sequence
        _cards = new Stack<ICard>(cards);
    }
    
    public int Count => _cards.Count;

    // Removes and returns the top card from the deck
    public ICard Draw()
    {
        if (_cards.Count == 0)
            throw new InvalidOperationException("Deck is empty.");

        return _cards.Pop();
    }

    // Builds a standard deck of cards
    // uses Fisher-Yates shuffle algorithm to shuffle the deck
    public static Deck Shuffle()
    {
        Random rng = new Random();
        var cards = new List<ICard>(52);

        foreach (Suit suit in Enum.GetValues<Suit>())
            foreach (Rank rank in Enum.GetValues<Rank>())
                cards.Add(new StandardCard(rank, suit));

        for (int i = cards.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }

        return new Deck(cards);
    }
}