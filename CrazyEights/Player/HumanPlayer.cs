using CrazyEights.Game;
using CrazyEights.Cards;
using CrazyEights.Domain;

namespace CrazyEights.Player;

public class HumanPlayer : PlayerBase
{
    public HumanPlayer(string name) : base(name) { }

    public override void TakeTurn(TurnContext context)
    {
        Console.WriteLine();
        Console.WriteLine(Name.ToUpper() + ", it is your turn.");
        Console.WriteLine("Top discard: " + context.TopDiscard.Display());
        Console.WriteLine("Suit to match: " + context.SuitToMatch);
        Console.WriteLine();

        PrintHand();
        Console.WriteLine();
        
        List<ICard> playable = FindPlayable(context.TopDiscard, context.SuitToMatch);

        if (playable.Count > 0)
        {
            PlayableList(context, playable);
            return;
        }
        
        Console.WriteLine(Name + ", you have no playable cards. Drawing one card...");
        ICard? drawnCard = context.DrawCard();

        if (drawnCard is null)
        {
            Console.WriteLine("The deck is empty. Turn ends.");
            return;
        }

        DrawCard(drawnCard);
        Console.WriteLine(Name + ", you drew: " +  drawnCard.Display());

        if (drawnCard.IsPlayable(context.TopDiscard, context.SuitToMatch))
        {
            Console.Write("Play the drawn card? Y/N: ");
            string? choice = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(choice) && choice.Trim().StartsWith("y", StringComparison.OrdinalIgnoreCase))
            {
                PlayCard(context, drawnCard);
                return;
            }
        }
        
        Console.WriteLine(Name + ", your turn is complete");
    }

    private void PrintHand()
    {
        var hand = PeekHand();
        for (int i = 0; i < hand.Count; i++)
        {
            Console.WriteLine("  [" + (i + 1) + "] " + hand[i].Display());
        }
    }

    private void PlayableList(TurnContext context, List<ICard> playable)
    {
        Console.WriteLine(Name + ", your playable cards are: ");
        for (int i = 0; i < playable.Count; i++)
        {
            string wc = "";

            if (playable[i].IsWildcard)
            {
                wc = "(Wildcard!)";
            }
            
            Console.WriteLine("[" + (i + 1) + "]  " +  playable[i].Display() + " " +wc);
        }

        int selection = ReadChoice(1, playable.Count, "Choose a card number to play: ");
        ICard selected = playable[selection - 1];
        PlayCard(context, selected);
    }

    private void PlayCard(TurnContext context, ICard card)
    {
        RemoveFromHand(card);
        
        context.PlayCard(card);
        
        Console.WriteLine();
        Console.WriteLine(Name + ", you played " + card.Display());

        if (card.IsWildcard)
        {
            Suit choice = SuitChoice(context.SuitToMatch);
            context.DeclareSuit(choice);
            Console.WriteLine(Name + ", you changed the suit to " + choice);
        }
    }

    private static int ReadChoice(int min, int max, string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int value) && value >= min && value <= max)
                return value;
            
            Console.WriteLine("Enter a number between " + min + " and " + max);
        }
    }

    private Suit SuitChoice(Suit currentSuit)
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine(Name + ", you played a wildcard! Choose a suit:");
            Console.WriteLine("[S] Spades ♠");
            Console.WriteLine("[H] Hearts ♥");
            Console.WriteLine("[C] Clubs ♣");
            Console.WriteLine("[D] Diamonds ♦");
            Console.WriteLine(" The current suit is " + currentSuit);
            Console.Write("Enter the letter of your chosen suit: ");

            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
                continue;
            
            char c = char.ToUpper(input.Trim()[0]);
            switch (c)
            {
                case 'S':
                    return Suit.Spades;
                case 'H':
                    return Suit.Hearts;
                case 'C':
                    return Suit.Clubs;
                case 'D':
                    return Suit.Diamonds;
                default:
                    Console.WriteLine("Please enter S, H, C, or D");
                    break;
            }
        }
    }
}