using CrazyEights.Cards;
using CrazyEights.Domain;
using CrazyEights.Game;

namespace CrazyEights.Player;

// Base class for implementing a player in Crazy Eights
// Provides helper methods
public abstract class PlayerBase : IPlayer
{
    private readonly List<ICard> _hand = new();

    public string Name { get; }
    public int CardCount => _hand.Count;

    protected PlayerBase(string name)
    {
        Name = name;
    }

    // Adds a card to a player's hand
    public void DrawCard(ICard card)
    {
        _hand.Add(card);
    }

    // Removes and returns a card at a specified index
    protected ICard PlayCardAtIndex(int index)
    {
        ICard card = _hand[index];
        _hand.RemoveAt(index);
        return card;
    }

    // Returns a read-only view of the player's hand
    protected IReadOnlyList<ICard> PeekHand()
    {
        return _hand.AsReadOnly();
    }

    // Returns all the cards that are playable on the current top card in the discard pile
    protected List<ICard> FindPlayable(ICard topDiscard, Suit suitToMatch)
    {
        var playable = new List<ICard>();
        foreach (var card in _hand)
        {
            if (card.IsPlayable(topDiscard, suitToMatch))
                playable.Add(card);
        }
        
        return playable;
    }

    // Removes a card from the player's hand
    protected void RemoveFromHand(ICard card)
    {
        _hand.Remove(card);
    }

    // Executes a players turn within the current context of the game
    public abstract void TakeTurn(TurnContext context);
}