using CrazyEights.Cards;

namespace CrazyEights.CardDeck;

public class DiscardPile
{
    private readonly Stack<ICard> _pile = new Stack<ICard>();

    public void Push(ICard card)
    {
        _pile.Push(card); 
    }

    public ICard Top => _pile.Peek();
}