using CrazyEights.Domain;

namespace CrazyEights.Cards;

public class StandardCard : ICard 
{
    public Rank Rank { get; }
    public Suit Suit { get; }

    public bool IsWildcard => Rank == Rank.Eight;

    public StandardCard(Rank rank, Suit suit)
    {
        Rank = rank;
        Suit = suit;
    }

    public bool IsPlayable(ICard topDiscard, Suit suitToMatch)
    {
        if (IsWildcard) return true;
        return Suit == suitToMatch || Rank == topDiscard.Rank;
    }

    public string Display() => $"{Rank} of {Suit}";

    public override string ToString() => Display();
    
}