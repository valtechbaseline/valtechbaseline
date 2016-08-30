using ValtechBaseLine.Model.Components;

namespace ValtechBaseLine.RepositoryContract.Components
{
    public interface IGalleryContract
    {
        IGalleryModel GetGallary(string datasource);
    }
}
