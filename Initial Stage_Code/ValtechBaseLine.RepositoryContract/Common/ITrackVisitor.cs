//---------------------------------------------------------------------------
// <copyright file="ITrackVisitor.cs" campany="Valtech">
//  Copyright © 2016 by Valtech_. All rights are reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited. 
// </copyright>
// <author>IN\jitender.monga</author>
// <time>4/5/2016 6:18:01 PM</time>
//---------------------------------------------------------------------------

using ValtechBaseLine.Model.Common;
using ValtechBaseLine.Model.Components;

namespace ValtechBaseLine.RepositoryContract.Common
{
    public interface ITrackVisitor
    {
        TrackUserModelCollection GetBrowserCookieValue();
    }
}
