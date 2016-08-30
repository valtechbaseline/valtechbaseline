
using System;
using Sitecore.Data;
using ValtechBaseLine.Model.Common;

namespace ValtechBaseLine.RepositoryContract.Common
{
    public interface IAnalyticsContract
    {
        void TriggerGoal(string goalId, string message);
        ValidationResultModel AddingList(string emailAddress, string firstName, string lastName, string listId, out string contactId);
        void SendSubscriptionMail(ID messageItemId, string contactId);
        void RemovingContactsFromList(string listId, string contactId, string messageId);
    }
}
