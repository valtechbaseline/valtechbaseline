using ValtechBaseLine.Model.Components;

namespace ValtechBaseLine.RepositoryContract.Common
{
    public interface IHeaderContract
    {
        IHeaderModel GetHeaderDetails(string datasource);
    }
}
