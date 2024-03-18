
    public class PricingEngine : IPricingEngine
    {
        public static double BuffetCostPerPerson { get; } = 9.99;
        public static double SoftDrinkCostPerDrink { get; } = 2.00;


        public BillSummary CalculateCost(IList<Guest> guests)
        {
            return new BillSummary(BuffetCostPerPerson, SoftDrinkCostPerDrink)
            {
                NumberOfGuests = guests.Count
            };
        }



    }

