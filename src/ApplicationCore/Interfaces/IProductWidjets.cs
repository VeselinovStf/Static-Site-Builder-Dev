namespace ApplicationCore.Interfaces
{
    public interface IProductWidjets
    {
        bool Hot { get; set; }
        bool LatestProduct { get; set; }
        bool LatestNewCollectionProduct { get; set; }
        bool DealOfTheDayProduct { get; set; }
        bool ProductOfTheDay { get; set; }
        bool PickedForYou { get; set; }
        string Category { get; set; }
    }
}