using ValtechBaseLine.Model.Interfaces;

namespace ValtechBaseLine.RepositoryContract.Common
{
    public interface IContactUs
    {
        IBaseModel GetHeaderDetails(string datasource);
    }
}
