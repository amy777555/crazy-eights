using CrazyEights.Cards;
using CrazyEights.Domain;

namespace CrazyEights.Game;

// This class represents the results of a player's turn
// Immutable
public class TurnAction
{
    private TurnAction(ICard? cardToPlay, bool drewCard, Suit? declaredSuit)
    {
        CardToPlay = cardToPlay;
        DrewCard = drewCard;
        DeclaredSuit = declaredSuit;
    }
    
    // The played card
    // Can be null
    public ICard? CardToPlay { get; }
    
    // True if the player drew a card
    public bool DrewCard { get; }  
    
    // The suit chosen after a wildcard is played
    // Can be null
    public Suit? DeclaredSuit { get; }
    
    // A player drew a card and ended a turn without playing a card
    public static TurnAction EndOfTurnAfterDraw() => new(cardToPlay: null, drewCard: true, declaredSuit: null);
    
    // A player played a card from their hand
    public static TurnAction Play(ICard card, Suit? declaredSuit = null) =>
        new(cardToPlay: card, drewCard: false, declaredSuit: declaredSuit);
    
    // A player drew a card and played it immediately
    public static TurnAction DrawAndPlay(ICard card, Suit? declaredSuit = null) =>
        new(cardToPlay: card, drewCard: true, declaredSuit: declaredSuit);
}