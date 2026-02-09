using CrazyEights.Cards;
using CrazyEights.Game;

namespace CrazyEights.Player;

// This interface represents a player in Crazy Eights
public interface IPlayer
{
    // Gets the players name
    string Name { get; }
    
    // Gets the number of cards in a player's hand
    int CardCount { get; }
    
    // Adds a card to the player's hand
    void DrawCard(ICard card);
    
    // Limits changes in the game to within the game context
    void TakeTurn(TurnContext context);
}