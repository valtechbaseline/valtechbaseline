using ValtechBaseLine.Common;
using ValtechBaseLine.Model.Components;


namespace ValtechBaseLine.RepositoryContract.Components
{
    public interface IContactUsContract
    {
         ContactUsModel GetContactUsDetails(string datasource);
         EmailDTO MappingContactUs(ContactUsModel contactUsModel);
        
    }
}
