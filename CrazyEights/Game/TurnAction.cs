using CrazyEights.Cards;
using CrazyEights.Domain;

namespace CrazyEights.Game;

public class TurnAction
{
    private TurnAction(ICard? cardToPlay, bool drewCard, Suit? declaredSuit)
    {
        CardToPlay = cardToPlay;
        DrewCard = drewCard;
        DeclaredSuit = declaredSuit;
    }
    
    public ICard? CardToPlay { get; }
    public bool DrewCard { get; }  
    public Suit? DeclaredSuit { get; }
    
    public static TurnAction EndOfTurnAfterDraw() => new(cardToPlay: null, drewCard: true, declaredSuit: null);
    
    public static TurnAction Play(ICard card, Suit? declaredSuit = null) =>
        new(cardToPlay: card, drewCard: false, declaredSuit: declaredSuit);
    
    public static TurnAction DrawAndPlay(ICard card, Suit? declaredSuit = null) =>
        new(cardToPlay: card, drewCard: true, declaredSuit: declaredSuit);
}