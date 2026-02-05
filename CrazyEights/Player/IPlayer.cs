using CrazyEights.Cards;
using CrazyEights.Game;

namespace CrazyEights.Player;

public interface IPlayer
{
    string Name { get; }
    int CardCount { get; }
    
    void DrawCard(ICard card);
    
    void TakeTurn(TurnContext context);
}