using CrazyEights.Cards;
using CrazyEights.Domain;

namespace CrazyEights.Game;

// Provides the information a player needs to complete their turn
public class TurnContext
{
    // The current top card on the discard pile
    public ICard TopDiscard { get; }
    
    // The active suit
    public Suit SuitToMatch { get; }
    
    // The current round number
    public int RoundNumber { get; }

    private readonly Func<ICard?> _drawCard;
    private readonly Action<ICard> _playToDiscard;
    private readonly Action<Suit> _setSuit;

    // Creates a turn context with information about the games current state
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

    // Draws a card from the deck
    public ICard? DrawCard() => _drawCard();

    // Plays a card to the discard pile
    public void PlayCard(ICard card)
    {
        _playToDiscard(card);
    }

    // Declares the suit after a wildcard is played
    public void DeclareSuit(Suit suit)
    {
        _setSuit(suit);
    }
}