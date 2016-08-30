//---------------------------------------------------------------------------
// <copyright file="ForgotPassword.cs" campany="Valtech">
//  Copyright © 2016 by Valtech_. All rights are reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited. 
// </copyright>
// <author>IN\abhay.dhar</author>
// <time>4/5/2016 6:31:33 PM</time>
//---------------------------------------------------------------------------

namespace ValtechBaseLine.Repository.Components
{
    using Glass.Mapper.Sc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using ValtechBaseLine.Model.Components;
    using ValtechBaseLine.RepositoryContract.Components;

    /// <summary>
    /// ForgotPassword
    /// </summary>
    public class ForgotPasswordRepository:IForgotPasswordContract
    {
        private readonly SitecoreContext _sitecoreContext;

         /// <summary>
        /// Default Constructor
        /// Instantiates the Sitecore service
        /// </summary>
        public ForgotPasswordRepository()
        {
            _sitecoreContext = new SitecoreContext();
            
        }

        IForgotPasswordModel GetForgotPassword(string datasource)
        {
            return this._sitecoreContext.GetItem<IForgotPasswordModel>(new Guid(datasource));
        }

        IForgotPasswordModel IForgotPasswordContract.GetForgotPassword(string datasource)
        {
            return this._sitecoreContext.GetItem<IForgotPasswordModel>(new Guid(datasource));
        }
    }
}