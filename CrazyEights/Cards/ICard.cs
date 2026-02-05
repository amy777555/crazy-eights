using CrazyEights.Domain;

namespace CrazyEights.Cards;

public interface ICard 
{
    Rank Rank { get; }
    Suit Suit { get; }

    bool IsWildcard { get; }

    bool IsPlayable(ICard topDiscard, Suit suitToMatch);

    string Display();
}