public class Guest
{
    private const int _maxTimesAskingForDrink = 2;

    public int GuestNumber { get; set; }
    public Drink? DrinkSelection { get; set; }
    private int TimesAskedForDrinkSelection { get; set; }
    private bool KeepAskingForDrinkSelection => DrinkSelection is null && TimesAskedForDrinkSelection < _maxTimesAskingForDrink;

    public void SelectDrink()
    {
        while (KeepAskingForDrinkSelection)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            if (TimesAskedForDrinkSelection == 0)
            {
                $"Alright Guest {GuestNumber}, would you like water or a softdrink? \n(You can type w or s for faster service)"
                    .ToConsole();
                ConsoleHelpers.AddPadding();
            }
            else if (TimesAskedForDrinkSelection == 1)
            {
                $"Ok lets try this again Guest {GuestNumber},\nwould you like water or a softdrink? (You can type w or s for faster service)".ToConsole();
                ConsoleHelpers.AddPadding();
            }

            var guestBeverageSelection = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;

            // select by enum if they used full name. Otherwise use short hand selection.
            if (Enum.TryParse(typeof(Drink), guestBeverageSelection, true, out var selectedDrink))
            {
                DrinkSelection = (Drink) selectedDrink;
            }
            else
            {

                //Allow Abbreviated User Inputs To Be Valid//
                switch (guestBeverageSelection?.ToLower())
                {
                    case "w":
                        DrinkSelection = Drink.Water;
                        break;
                    case "s":
                        DrinkSelection = Drink.Soda;
                        break;
                    case null:
                        DrinkSelection = Drink.None;
                        break;
                    default:
                        DrinkSelection = null;
                        break;

                }
            }

            TimesAskedForDrinkSelection++;

            if (DrinkSelection != null && DrinkSelection != Drink.None)
            {
                $"{DrinkSelection}, you got it!".ToConsole();
            }
            else if (DrinkSelection == null)
            {
                "Sorry we don't have that selection, would you like something else or nothing to drink?"
                    .ToConsole();
                var beverageResponse = "Enter Yes or y to choose a different drink or any key to pass"
                    .AskQuestionGetResponse();

                var wouldLikeToTryAgain = (beverageResponse?.ToLower() is "yes" or "y");
                if (!wouldLikeToTryAgain)
                {
                    $"Ok  Guest {GuestNumber} orders nothing... Moving on.".ToConsole();
                    DrinkSelection = Drink.None;
                    return;
                }
            }
        }
    }
}