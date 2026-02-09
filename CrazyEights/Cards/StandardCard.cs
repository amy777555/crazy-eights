using CrazyEights.Domain;

namespace CrazyEights.Cards;

// This class represents a playing card in Crazy Eights
public class StandardCard : ICard 
{
    // Gets rank of card (king, queen, etc)
    public Rank Rank { get; }
    
    // Gets suit of card (clubs, diamonds, etc)
    public Suit Suit { get; }

    // Returns status of card as a wildcard
    // Cards with a rank of eight are wildcards
    public bool IsWildcard => Rank == Rank.Eight;

    // Creates a card with a given rank and suit
    public StandardCard(Rank rank, Suit suit)
    {
        Rank = rank;
        Suit = suit;
    }

    // Determines if a card can be played
    // A card is playable if it is an eight,
    // ...its suit matches the current suit to match,
    // ...and its rank matches the top discard
    public bool IsPlayable(ICard topDiscard, Suit suitToMatch)
    {
        if (IsWildcard) return true;
        return Suit == suitToMatch || Rank == topDiscard.Rank;
    }
    
    // Returns a short string representation of the card for UI
    public string Display() => $"{Rank} of {Suit}";

    public override string ToString() => Display();
    
}