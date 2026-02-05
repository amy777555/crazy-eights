using CrazyEights.Cards;
using CrazyEights.Domain;
using CrazyEights.Game;

namespace CrazyEights.Player;

public abstract class PlayerBase : IPlayer
{
    private readonly List<ICard> _hand = new();

    public string Name { get; }
    public int CardCount => _hand.Count;

    protected PlayerBase(string name)
    {
        Name = name;
    }

    public void DrawCard(ICard card)
    {
        _hand.Add(card);
    }

    protected ICard PlayCardAtIndex(int index)
    {
        ICard card = _hand[index];
        _hand.RemoveAt(index);
        return card;
    }

    public IReadOnlyList<ICard> PeekHand()
    {
        return _hand.AsReadOnly();
    }

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

    protected void RemoveFromHand(ICard card)
    {
        _hand.Remove(card);
    }

    public abstract void TakeTurn(TurnContext context);
}