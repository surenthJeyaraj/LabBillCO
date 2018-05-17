using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repository
{
    class CrmRepository
    {
        private string _connectionString;
        public CrmRepository(string connectionstring)
        {
            _connectionString = connectionstring;
        }
        public string GetRequest(string TransactionID)

        {
            string result = string.Empty; 


            return result;


        }
    }
}
