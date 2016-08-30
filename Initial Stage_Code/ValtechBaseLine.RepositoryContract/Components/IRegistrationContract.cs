using ValtechBaseLine.Model.Components;

namespace ValtechBaseLine.RepositoryContract.Components
{
    public interface IRegistrationContract
    {
        RegistrationModel GetRegistrationForm(string dataSource);
        bool SubmitRegistrationForm(RegistrationModel model, out string duplicateEmail);
    }
}
