using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper.Sc;
using ValtechBaseLine.CMS.Extension.MongoDB;
using ValtechBaseLine.Model.Components;
using ValtechBaseLine.RepositoryContract.Components;
using ValtechBaseLine.Model.Common;
using System.Web.Mvc;
using ValtechBaseLine.Common;
using ValtechBaseLine.Common.DTO;
using ValtechBaseLine.Data.EF;


namespace ValtechBaseLine.Repository.Components
{
    public class RegistrationRepository : IRegistrationContract
    {
        private readonly ValtechBaseLineEntities _dataBaseEntities;

        private readonly SitecoreContext _sitecoreContext;

        private readonly ValtechMembershipProvider _valtechMembershipProvider = new ValtechMembershipProvider();
        /// <summary>
        /// Default Constructor
        /// Instantiates the Sitecore service
        /// </summary>
        public RegistrationRepository()
        {
            _dataBaseEntities = new ValtechBaseLineEntities();
            _sitecoreContext = new SitecoreContext();

        }

        public RegistrationModel GetRegistrationForm(string dataSource)
        {
            var registrationModel = this._sitecoreContext.GetItem<RegistrationModel>(new Guid(dataSource));
            if (registrationModel != null)
            {
                registrationModel.SalutationOption = new List<SelectListItem>();
                List<ITaxonomy> salutationList = this._sitecoreContext.GetItem<ITaxonomy>(Constants.SalutationList).Children.ToList();
                salutationList.ForEach(x => registrationModel.SalutationOption.Add(new SelectListItem { Value = x.Id.ToString(), Text = x.Value }));
                return registrationModel;
            }

            return null;

        }

        public bool SubmitRegistrationForm(RegistrationModel model, out string duplicateEmail)
        {
            duplicateEmail = "";
            if (model != null && !string.IsNullOrWhiteSpace(model.EmailId) && !string.IsNullOrWhiteSpace((model.Password)))
            {
                var custId = _valtechMembershipProvider.CreateUser(model.EmailId, model.Password, String.Concat(model.FirstName,model.LastName));
                if (custId == Constants.DuplicateEmail)
                {
                    duplicateEmail = Constants.DuplicateEmail;
                    return false;
                }
                if (!string.IsNullOrWhiteSpace(custId))
                {
                    using (var context = _dataBaseEntities.GetContext())
                    {
                        var saveData = context.SaveUserInfo(new UserDTO()
                        {
                            ScCustId = custId,
                            ScSalutationId = model.Salutation,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            PhoneNumber = model.PhoneNumber
                        });
                        #region Saving Data in MongoDB
                        SavedContactRespositary xdb = new SavedContactRespositary();
                        xdb.XdbSaveContact(model);
                        #endregion
                        return saveData;
                    }
                }
            }
            return false;
        }
    }
}
