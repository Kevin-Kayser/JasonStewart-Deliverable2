
    public interface IPricingEngine
{
    BillSummary CalculateCost(IList<Guest> guests);
}