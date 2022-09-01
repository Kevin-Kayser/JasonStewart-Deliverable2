public class RoadsideDiner
{
    private const int _maxGuests = 6;
    private readonly IPricingEngine _pricingEngine;

    public RoadsideDiner()
    {
        _pricingEngine = new PricingEngine();
    }

    public IList<Guest> Guests { get; set; } = new List<Guest>();
    public int NumberOfGuests => Guests.Count;

    private int TimesAskedForGuestCount { get; set; }
    public bool KickGuestsOutForTooManyPeople => TimesAskedForGuestCount > 1 && Guests.Count > _maxGuests;


    public void WaveGoodbye()
    {
        if (KickGuestsOutForTooManyPeople)
        {
            "Sorry You Are Leaving, Please Stop By Again. \n".ToConsole();

        }

        "Have a nice day!".ToConsole();
    }

    public void SetNumberOfGuests()
    {
        var consoleResponse = "";

        if (TimesAskedForGuestCount == 0)
        {
            consoleResponse = "How many guests are in your party?".AskQuestionGetResponse();
        } else if (TimesAskedForGuestCount > 0)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            $"I'm sorry, your party of {Guests.Count} exceeds our current limit of 6 guests per party"
                .ToConsole();
            ConsoleHelpers.AddPadding();
            Console.ForegroundColor = oldColor;

            //Give User Chance To Adjust Group Size || Leave//
            var groupResponse =
                "Would you like to adjust your party size or find a different restaurant? \nPlease enter Yes or y to adjust your party size or press any other key to leave"
                    .AskQuestionGetResponse();

            //Handle User Capitalization Variance//
            groupResponse = groupResponse.ToLower();

            if (groupResponse is "yes" or "y")
            {
                Console.Clear();
                consoleResponse = "Please enter a new party size that is 6 or less guests".AskQuestionGetResponse();
            }
            else
            {
                TimesAskedForGuestCount++;
                return;
            }
        }


        if (int.TryParse(consoleResponse, out var numberOfGuests))
        {
            UpdateNumberOfGuests(numberOfGuests);
        }
        ConsoleHelpers.AddPadding();
        TimesAskedForGuestCount++;
    }

    public void GreetGuests()
    {

        Console.ForegroundColor = ConsoleColor.Blue;
        "Welcome to Greasy Jean's All You Can Eat Food Trough!".ToConsole();
        "This all you can eat experience only costs $9.99 per guest!".ToConsole();
        "Here at Greasy Jean's water is always free or upgrade to a 32oz softdrink for just $2.00!".ToConsole();
        ConsoleHelpers.AddPadding();

        //Warn Guests About Group Size Limit//
        Console.ForegroundColor = ConsoleColor.Red;
        "                     ****Please note**** \nDue to current staffing shortages we must limit party size to 6 guests or less".ToConsole();
        ConsoleHelpers.AddPadding();

        Console.ForegroundColor = ConsoleColor.White;
    }

    public void UpdateNumberOfGuests(int newNumberOfGuests)
    {
        Guests?.Clear();
        for (var i = 1; i <= newNumberOfGuests; i++)
        {
            var newGuest = new Guest()
            {
                GuestNumber = i
            };

            Guests?.Add(newGuest);
        }
    }

    public bool HasValidGuestCount => NumberOfGuests is > 0 and <= _maxGuests && !KickGuestsOutForTooManyPeople;

    public bool ShouldAskForGuestCount => !HasValidGuestCount && !KickGuestsOutForTooManyPeople;

    public void PrintReceipt()
    {

        BillSummary summary = _pricingEngine.CalculateCost(Guests);

        // Present Total Bill
        Console.WriteLine("Thank you for dining at Greasy Jean's!");
        ConsoleHelpers.AddPadding();
        Console.WriteLine("Your total bill today is: $" + $"{summary.TotalBill:0.00}");
        ConsoleHelpers.AddPadding();


        // Write Detailed Receipt With Item Quantities && Line Item Totals
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("Item            | Qty    | Cost Per   | Line Total");
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine(
            $"{"Buffet",-15} | {summary.NumberOfGuests,-6} | {"$" + summary.BuffetCostPerPerson,-10} | {"$" + $"{summary.TotalBuffetCost:0.00}",-10}");

        var sodaTotal = Guests.Where(x => x.DrinkSelection == Drink.Soda).Count();
        var waterTotal = Guests.Where(x => x.DrinkSelection == Drink.Water).Count();
        //Do Not Show Line Items On Receipt If Zero Ordered//
        if (sodaTotal > 0)
        {
            $"{"Softdrink",-15} | {sodaTotal,-6} | {"$" + $"{summary.SoftDrinkCostPerDrink:0.00}",-10} | {"$" + $"{summary.TotalSoftDrinkCost:0.00}",-10}".ToConsole();
        }

        if (waterTotal > 0)
        {
            Console.WriteLine($"{"Water",-15} | {waterTotal,-6} | {"FREE!",-10} | {"$0.00",-10}");
        }

        //Finish Building Receipt//
        Console.WriteLine("--------------------------------------------------");
        ConsoleHelpers.AddPadding();

        Console.WriteLine($"Total Bill ------------------------------${summary.TotalBill}");
        ConsoleHelpers.AddPadding();

        Console.WriteLine("------------------Add Gratuity--------------------");
        Console.WriteLine("     5%    |    10%     |    15%     |    20%     ");
        Console.WriteLine(
            $"{$"  ${Math.Round((summary.TotalBuffetCost * 1.05), 2):0.00}",-10} | {$" ${Math.Round((summary.TotalBuffetCost * 1.10), 2):0.00}",-10} | {" $" + $"{Math.Round((summary.TotalBuffetCost * 1.15), 2):0.00}",-10} | {" $" + $"{Math.Round((summary.TotalBuffetCost * 1.20), 2):0.00}",-10}");
        Console.WriteLine("--------------------------------------------------");

        //Thank Guests//
        Console.ForegroundColor = ConsoleColor.White;
        ConsoleHelpers.AddPadding();

    }

    public void AskGuestsForDrinkSelection()
    {
        foreach (var guest in Guests)
        {
            guest.SelectDrink();
        }
    }
}