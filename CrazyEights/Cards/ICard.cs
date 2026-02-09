using CrazyEights.Domain;

namespace CrazyEights.Cards;

// This interface represents a playing card used in Crazy Eights
// Defines the characteristics of a playing card
public interface ICard 
{
    // Gets rank of card (king, queen, etc)
    Rank Rank { get; }
    
    // Gets suit of card (clubs, diamonds, etc)
    Suit Suit { get; }

    // Gets status of card as a wildcard
    bool IsWildcard { get; }

    // Determines whether a card can be played on top of ...
    // the current top card in the discard pile
    bool IsPlayable(ICard topDiscard, Suit suitToMatch);

    // Returns a short string representation of the card for UI
    string Display();
}