using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Model;

namespace Domain.Interfaces
{
    public interface IRequestFileRepository
    {
        string GetRequest(string TransactionID);
        string GetTimelyFilling(string TransactionID, string claimRecId, string StrecID);
        string Get997LevelXMLContent(string TransactionID); 
    }
}
