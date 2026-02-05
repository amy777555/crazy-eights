using CrazyEights.Cards;
using CrazyEights.Domain;

namespace CrazyEights.Game;

public class TurnContext
{
    public ICard TopDiscard { get; }
    public Suit SuitToMatch { get; }
    public int RoundNumber { get; }

    private readonly Func<ICard?> _drawCard;
    private readonly Action<ICard> _playToDiscard;
    private readonly Action<Suit> _setSuit;

    public TurnContext(
        ICard topDiscard,
        Suit suitToMatch,
        int roundNumber,
        Func<ICard?> drawCard,
        Action<ICard> playToDiscard,
        Action<Suit> setSuit)
    {
        TopDiscard = topDiscard;
        SuitToMatch = suitToMatch;
        RoundNumber = roundNumber;
        
        _drawCard = drawCard;
        _playToDiscard = playToDiscard;
        _setSuit = setSuit;
    }

    public ICard? DrawCard() => _drawCard();

    public void PlayCard(ICard card)
    {
        _playToDiscard(card);
    }

    public void DeclareSuit(Suit suit)
    {
        _setSuit(suit);
    }
}