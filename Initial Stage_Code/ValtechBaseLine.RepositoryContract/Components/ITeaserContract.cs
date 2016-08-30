using ValtechBaseLine.Model.Components;

namespace ValtechBaseLine.RepositoryContract.Components
{
    public interface ITeaserContract
    {
        IImageTeaserModel GetTeaser(string datasource);
    }
}
