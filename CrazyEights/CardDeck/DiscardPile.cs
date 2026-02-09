using CrazyEights.Cards;

namespace CrazyEights.CardDeck;

// This class represents the games discard pile
// Cards are added to the stack, and only the top card is visible
public class DiscardPile
{
    private readonly Stack<ICard> _pile = new Stack<ICard>();

    // Places a card on top of the discard pile
    public void Push(ICard card)
    {
        _pile.Push(card); 
    }

    // Gets the current top card 
    public ICard Top => _pile.Peek();
}