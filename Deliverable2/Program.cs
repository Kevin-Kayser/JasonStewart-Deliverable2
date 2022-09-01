class MainClass
{
    public static void Main(string[] args)
    {
        // Initialize and say hello
        var diner = new RoadsideDiner();
        diner.GreetGuests();

        // setup their stay
        while (diner.ShouldAskForGuestCount)
        {
            diner.SetNumberOfGuests();
        }

        // Only continue when we have guests
        if (diner.HasValidGuestCount)
        {
            diner.AskGuestsForDrinkSelection();
            diner.PrintReceipt();
        }

        // It's time to leave, bye!
        diner.WaveGoodbye();
    }
}

