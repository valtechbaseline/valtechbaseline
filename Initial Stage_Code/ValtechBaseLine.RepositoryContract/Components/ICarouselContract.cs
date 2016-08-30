using ValtechBaseLine.Model.Components;


namespace ValtechBaseLine.RepositoryContract.Components
{
    public interface ICarouselContract
    {
        CarouselModel CarouselDetails(string datasource);
    }
}
