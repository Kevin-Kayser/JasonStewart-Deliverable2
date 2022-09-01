public class BillSummary
{
    public BillSummary(double buffetCostPerPerson, double softDrinkCostPerDrink)
    {
        BuffetCostPerPerson = buffetCostPerPerson;
        SoftDrinkCostPerDrink = softDrinkCostPerDrink;

    }
    public double BuffetCostPerPerson { get; private set; }
    public double SoftDrinkCostPerDrink { get; private set; }
    public int NumberOfGuests { get; set; }
    public double TotalBuffetCost => BuffetCostPerPerson * NumberOfGuests;

    public double TotalSoftDrinkCost => SoftDrinkCostPerDrink * NumberOfGuests;
    public double TotalBill => TotalBuffetCost + TotalSoftDrinkCost;

}