using CrazyEights.Cards;
using CrazyEights.Domain;
using CrazyEights.Game;

namespace CrazyEights.Player;

public class CpuPlayer : PlayerBase
{
    private readonly Random _rng;

    public CpuPlayer(string name) : base(name)
    {
        _rng = new Random();
    }

    public override void TakeTurn(TurnContext context)
    {
        Console.WriteLine();
        Console.WriteLine("IT IS THE " + Name.ToUpper() + "'S TURN");
        Console.WriteLine();
        Console.WriteLine("Top discard: " + context.TopDiscard.Display());
        Console.WriteLine("Suit to match: " + context.SuitToMatch);
        Console.WriteLine();
        
        List<ICard> playable = FindPlayable(context.TopDiscard, context.SuitToMatch);

        if (playable.Count > 0)
        {
            PlaySelectedCard(context, ChooseCard(playable), drewCard: false);
            return;
        }
        
        Console.WriteLine(Name + " has no playable cards. Drawing one card...");
        ICard? drawn = context.DrawCard();

        if (drawn is null)
        {
            Console.WriteLine("The deck is empty. Turn ends.");
            return;
        }

        DrawCard(drawn);
        Console.WriteLine(Name + " drew: " +  drawn.Display());

        if (drawn.IsPlayable(context.TopDiscard, context.SuitToMatch))
        {
            PlaySelectedCard(context, drawn, drewCard: true);
            return;
        }
        
        Console.WriteLine(Name + "'s turn ends.");
    }

    private ICard ChooseCard(List<ICard> playable)
    {
        int index = _rng.Next(playable.Count);
        return playable[index];
    }

    private void PlaySelectedCard(TurnContext context, ICard card, bool drewCard)
    {
        RemoveFromHand(card);
        context.PlayCard(card);
        
        if (drewCard)
            Console.WriteLine(Name + " played the drawn card: " + card.Display());
        else
            Console.WriteLine(Name + " played " + card.Display());

        if (card.IsWildcard)
        {
            Suit choice = ChooseSuit();
            context.DeclareSuit(choice);
            Console.WriteLine(Name + " declared suit: " + choice);
        }
    }

    private Suit ChooseSuit()
    {
        Suit[] suits = { Suit.Spades, Suit.Hearts, Suit.Clubs, Suit.Diamonds };
        return suits[_rng.Next(suits.Length)];
    }
}